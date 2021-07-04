using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

using ejemplo;

public class ZombiFSM : MonoBehaviour
{

    private StateMachineEngine ZombiFSM_FSM;
    private State Parado;
    private State Buscando;
    private State Recargando;
    private State Avanzando;
    private State FijarVictima;
    private State Matado;
    private int r;
    private bool victimaEncontrada = false;
    private Vector3 destino;
    private NavMeshAgent Zombi;
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




    void Start()
    {
        ZombiFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        Zombi = GetComponent<NavMeshAgent>();
        CreateStateMachine();
    }

    private void CreateStateMachine()
    {
        //States
        Parado = ZombiFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = ZombiFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = ZombiFSM_FSM.CreateState("Avanzando", AvanzandoAction);
        Recargando = ZombiFSM_FSM.CreateState("Recargando", RecargandoAction);
        FijarVictima = ZombiFSM_FSM.CreateState("FijarVictima", FijarVictimaAction);
        Matado = ZombiFSM_FSM.CreateState("Matado", MatadoAction);

        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = ZombiFSM_FSM.CreatePerception<IsInStatePerception>(ZombiFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = ZombiFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = ZombiFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Zombi.transform.position) <= 10.0f && !estaPersiguiendo);
        EncuentraVictimaPerception = ZombiFSM_FSM.CreatePerception<ValuePerception>(() => estaPersiguiendo && !checkeado);
        VictimaFijadaPerception = ZombiFSM_FSM.CreatePerception<PushPerception>();
        VictimaMuertaPerception = ZombiFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Zombi.transform.position) <= 10.0f && estaPersiguiendo);
        VictimaEscapaPerception = ZombiFSM_FSM.CreatePerception<ValuePerception>(() => victimaRenderer.enabled == false); //Comprobar si el mesh renderer del objetivo está desactivado
        HaDescansadoPerception = ZombiFSM_FSM.CreatePerception<PushPerception>();
        RecargandoPerception = ZombiFSM_FSM.CreatePerception<TimerPerception>(15);

        // Transitions
        ZombiFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        ZombiFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        ZombiFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);
        ZombiFSM_FSM.CreateTransition("EncuentraVictima", Avanzando, EncuentraVictimaPerception, FijarVictima);
        ZombiFSM_FSM.CreateTransition("VictimaFijada", FijarVictima, VictimaFijadaPerception, Avanzando);
        ZombiFSM_FSM.CreateTransition("VictimaMuerta", Avanzando, VictimaMuertaPerception, Matado);
        ZombiFSM_FSM.CreateTransition("VictimaEscapa", Avanzando, VictimaEscapaPerception, Parado);
        ZombiFSM_FSM.CreateTransition("HaDescansado", Matado, HaDescansadoPerception, Recargando);
        ZombiFSM_FSM.CreateTransition("HaRecargado", Recargando, RecargandoPerception, Parado);



    }

    void Update()
    {
        ZombiFSM_FSM.Update();
        if (estaPersiguiendo)
        {
            destino = victima.transform.position;
            Zombi.SetDestination(destino);
        }
    }

    private void FijarVictimaAction()
    {
        Debug.Log("He fijado victima");
        destino = victima.transform.position;
        Zombi.SetDestination(destino);
        ZombiFSM_FSM.Fire("VictimaFijada");
        checkeado = true;
    }

    private void RecargandoAction()
    {
        Debug.Log("Estoy Recargando");
        Zombi.velocity.Set(0, 0, 0);

    }

    private void MatadoAction()
    {
        Debug.Log("Lo he matao");
        checkeado = false;
        Zombi.velocity.Set(0, 0, 0);

        estaPersiguiendo = false;
        Destroy(victima);
        ZombiFSM_FSM.Fire("HaDescansado");
    }

    private void AvanzandoAction()
    {
    }

    private void BuscandoAction()
    {
        Debug.Log("Estoy andando Random");
        r = Random.Range(0, 30);
        destino = positions[r];
        Zombi.SetDestination(destino);
        ZombiFSM_FSM.Fire("Buscando_Avanzando");
    }

    private void ParadoAction()
    {
        if (estaPersiguiendo) {
            Debug.Log("Se me ha escapado hijo de puta");
            checkeado = false;
            estaPersiguiendo = false;
        }
        Zombi.velocity.Set(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].thisCollider.tag == "zombievision")
        {
            if (collision.collider.tag == "aldeano" && ZombiFSM_FSM.GetCurrentState().Name == "Avanzando" && !estaPersiguiendo)
            {
                victima = collision.collider.gameObject;
                victimaModelo = victima.transform.Find("Aldeano/aldeano/default").gameObject;
                victimaRenderer = victimaModelo.GetComponent<MeshRenderer>();
                if (victimaRenderer.enabled)
                {
                    estaPersiguiendo = true;
                    ZombiFSM_FSM.Fire("EncuentraVictima");
                }
            }
        }
    }

    }
