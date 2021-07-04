using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

using ejemplo;

public class GuardiaFSM : MonoBehaviour
{

    private StateMachineEngine GuardiaFSM_FSM;
    private State Parado;
    private State Buscando;
    private State Recargando;
    private State Avanzando;
    private State FijarVictima;
    private State Matado;
    private int r;
    private bool victimaEncontrada = false;
    private Vector3 destino;
    private NavMeshAgent Guardia;
    private IsInStatePerception ParadoBuscandoPerception;
    private TimerPerception RecargandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private ValuePerception EstaEnDestinoPerception;
    private ValuePerception EncuentraVictimaPerception;
    private PushPerception VictimaFijadaPerception;
    private ValuePerception VictimaMuertaPerception;
    private ValuePerception VictimaEscapaPerception;
    private PushPerception HaDescansadoPerception;
    private Vector3[] positions = Spawn.GetSpawnpoints();
    private bool estaPersiguiendo = false;
    private bool checkeado = false;
    private bool checkeadoRenderer = false;
    private GameObject victima;
    private GameObject victimaModelo;
    private MeshRenderer victimaRenderer = new MeshRenderer();
    private int duelo;




    void Start()
    {
        GuardiaFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        Guardia = GetComponent<NavMeshAgent>();
        CreateStateMachine();
    }

    private void CreateStateMachine()
    {
        //States
        Parado = GuardiaFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = GuardiaFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = GuardiaFSM_FSM.CreateState("Avanzando", AvanzandoAction);
        Recargando = GuardiaFSM_FSM.CreateState("Recargando", RecargandoAction);
        FijarVictima = GuardiaFSM_FSM.CreateState("FijarVictima", FijarVictimaAction);
        Matado = GuardiaFSM_FSM.CreateState("Matado", MatadoAction);

        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = GuardiaFSM_FSM.CreatePerception<IsInStatePerception>(GuardiaFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = GuardiaFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Guardia.transform.position) <= 10.0f && !estaPersiguiendo);
        EncuentraVictimaPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => estaPersiguiendo && !checkeado);
        VictimaFijadaPerception = GuardiaFSM_FSM.CreatePerception<PushPerception>();
        VictimaMuertaPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Guardia.transform.position) <= 10.0f && estaPersiguiendo);
        VictimaEscapaPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => victimaRenderer.enabled == false); //Comprobar si el mesh renderer del objetivo está desactivado
        HaDescansadoPerception = GuardiaFSM_FSM.CreatePerception<PushPerception>();
        RecargandoPerception = GuardiaFSM_FSM.CreatePerception<TimerPerception>(5);

        // Transitions
        GuardiaFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        GuardiaFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        GuardiaFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);
        GuardiaFSM_FSM.CreateTransition("EncuentraVictima", Avanzando, EncuentraVictimaPerception, FijarVictima);
        GuardiaFSM_FSM.CreateTransition("VictimaFijada", FijarVictima, VictimaFijadaPerception, Avanzando);
        GuardiaFSM_FSM.CreateTransition("VictimaMuerta", Avanzando, VictimaMuertaPerception, Matado);
        GuardiaFSM_FSM.CreateTransition("VictimaEscapa", Avanzando, VictimaEscapaPerception, Parado);
        GuardiaFSM_FSM.CreateTransition("HaDescansado", Matado, HaDescansadoPerception, Recargando);
        GuardiaFSM_FSM.CreateTransition("HaRecargado", Recargando, RecargandoPerception, Parado);



    }

    void Update()
    {
        GuardiaFSM_FSM.Update();
        if (estaPersiguiendo)
        {
            destino = victima.transform.position;
            Guardia.SetDestination(destino);
        }
    }

    private void FijarVictimaAction()
    {
        Debug.Log("He fijado victima");
        destino = victima.transform.position;
        Guardia.SetDestination(destino);
        GuardiaFSM_FSM.Fire("VictimaFijada");
        checkeado = true;
    }

    private void RecargandoAction()
    {
        Debug.Log("Estoy Recargando");
        Guardia.velocity.Set(0, 0, 0);

    }

    private void MatadoAction()
    {
        duelo = Random.Range(0, 100);
        if (duelo <= 90)
        {
            Debug.Log("Lo he matao");
            checkeado = false;
            Guardia.velocity.Set(0, 0, 0);

            estaPersiguiendo = false;
            Destroy(victima);
            GuardiaFSM_FSM.Fire("HaDescansado");
        }
        else
        {
            Destroy(this);
        }
    }

    private void AvanzandoAction()
    {
    }

    private void BuscandoAction()
    {
        Debug.Log("Estoy andando Random");
        r = Random.Range(0, 30);
        destino = positions[r];
        Guardia.SetDestination(destino);
        GuardiaFSM_FSM.Fire("Buscando_Avanzando");
    }

    private void ParadoAction()
    {
        if (estaPersiguiendo)
        {
            Debug.Log("Se me ha escapado hijo de puta");
            checkeado = false;
            estaPersiguiendo = false;
        }
        Guardia.velocity.Set(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].thisCollider.tag == "guardiavision")
        {
            if (collision.collider.tag == "zombi" && GuardiaFSM_FSM.GetCurrentState().Name == "Avanzando" && !estaPersiguiendo)
            {
                victima = collision.collider.gameObject;
                victimaModelo = victima.transform.Find("Zombie/zombie/default").gameObject;
                victimaRenderer = victimaModelo.GetComponent<MeshRenderer>();
                if (victimaRenderer.enabled)
                {
                    estaPersiguiendo = true;
                    GuardiaFSM_FSM.Fire("EncuentraVictima");
                }
            }
        }
    }

}
