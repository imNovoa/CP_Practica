using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Random;

using ejemplo;

public class AldeanoFSM : MonoBehaviour {


    //public static Spawn s = new Spawn();

    #region variables

    private StateMachineEngine AldeanoFSM_FSM;

    private IsInStatePerception ParadoBuscandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private PushPerception DecideDormirBuscaCasaPerception;
    private PushPerception BuscaCasaAvanzaCasaPerception;
    private PushPerception DecideDormirBuscandoPerception;
    private PushPerception AvanzandoZombiePerception;
    private PushPerception AvanzandoBuscandoPerception;
    private ValuePerception EstaEnDestinoPerception;
    private ValuePerception DespertarPerception;
    private ValuePerception DormirPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;
    private State DecideDormir;
    private State BuscaCasa;
    private State AvanzaCasa;
    private State Dormir;
    private Vector3[] positions = Spawn.GetSpawnpoints();
    private NavMeshAgent Aldeano;
    private GameObject modelo;
    private MeshRenderer AldeanoRenderer;
    private Vector3 destino = new Vector3(0,0,0);
    private int r;
    private int porcentajeDormir;
    private int casaRandom;
    public int distrito;
    private bool checkDormir = false;
    private float horaDespertar = 8.0f;
    private Vector3[] puertas = Globales.generarPuertas();
    private bool estaAsustado = false;
    private bool posicionEsValida = false;
    private bool distritoCerradoCheck = false;

    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        AldeanoFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        Aldeano = GetComponent<NavMeshAgent>();
        modelo = transform.Find("Aldeano/aldeano/default").gameObject;
        AldeanoRenderer = modelo.GetComponent<MeshRenderer>();

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        
        
        // States
        Parado = AldeanoFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = AldeanoFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = AldeanoFSM_FSM.CreateState("Avanzando", AvanzandoAction);
        DecideDormir = AldeanoFSM_FSM.CreateState("DecideDormir", DecideDormirAction);
        BuscaCasa = AldeanoFSM_FSM.CreateState("BuscaCasa", BuscaCasaAction);
        AvanzaCasa = AldeanoFSM_FSM.CreateState("AvanzaCasa", AvanzaCasaAction);
        Dormir = AldeanoFSM_FSM.CreateState("Dormir", DormirAction);




        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = AldeanoFSM_FSM.CreatePerception<IsInStatePerception>(AldeanoFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = AldeanoFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Aldeano.transform.position) <= 10.0f);
        DecideDormirBuscaCasaPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        DecideDormirBuscandoPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        DespertarPerception = AldeanoFSM_FSM.CreatePerception<ValuePerception>(() => Globales.horaActual >= horaDespertar && Globales.horaActual <= horaDespertar + 2);
        DormirPerception = AldeanoFSM_FSM.CreatePerception<ValuePerception>(() => Globales.horaActual >= 21 && Globales.horaActual <= 21.2f && !checkDormir);
        BuscaCasaAvanzaCasaPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        AvanzandoZombiePerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        AvanzandoBuscandoPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();






        // ExitPerceptions

        // Transitions
        AldeanoFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        AldeanoFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        AldeanoFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);
        AldeanoFSM_FSM.CreateTransition("DecideDormir_BuscaCasa", DecideDormir, DecideDormirBuscaCasaPerception, BuscaCasa);
        AldeanoFSM_FSM.CreateTransition("BuscaCasa_AvanzaCasa", BuscaCasa, BuscaCasaAvanzaCasaPerception, AvanzaCasa);
        AldeanoFSM_FSM.CreateTransition("DecideDormir_Buscando", DecideDormir, DecideDormirBuscandoPerception, Buscando);
        AldeanoFSM_FSM.CreateTransition("EstaEnDestinoDormir", AvanzaCasa, EstaEnDestinoPerception, Dormir);
        AldeanoFSM_FSM.CreateTransition("Despertar", Dormir, DespertarPerception, Parado);
        AldeanoFSM_FSM.CreateTransition("Avanzando_DecideDormir", Avanzando, DormirPerception, DecideDormir);
        AldeanoFSM_FSM.CreateTransition("Avanzando_Zombi", Avanzando, AvanzandoZombiePerception, BuscaCasa);
        AldeanoFSM_FSM.CreateTransition("Avanzando_Buscando", Avanzando, AvanzandoBuscandoPerception, Buscando);



        // ExitTransitions

    }

    // Update is called once per frame
    private void Update()
    {
        AldeanoFSM_FSM.Update();
    }

    // Create your desired actions
    
    private void ParadoAction()
    {
        checkDormir = false;
        AldeanoRenderer.enabled = true;
        Aldeano.velocity.Set(0, 0, 0);
    }
    
    private void BuscandoAction()
    {
        r = Random.Range(0, 30);
        destino = positions[r];
        Aldeano.SetDestination(destino);
        AldeanoFSM_FSM.Fire("Buscando_Avanzando");
    }
    
    private void AvanzandoAction()
    {
        
    }

    private void DecideDormirAction()
    {
        checkDormir = true;
        porcentajeDormir = Random.Range(0, 100);
        if(porcentajeDormir <= 75)
        {
            AldeanoFSM_FSM.Fire("DecideDormir_BuscaCasa");
        }
        else
        {
            AldeanoFSM_FSM.Fire("DecideDormir_Buscando");
        }
    }

    private void BuscaCasaAction()
    {
        casaRandom = Random.Range(distrito * 10, distrito * 10 + 9);

        destino = puertas[casaRandom];
        Aldeano.SetDestination(destino);
        AldeanoFSM_FSM.Fire("BuscaCasa_AvanzaCasa");
    }

    private void AvanzaCasaAction()
    {

    }

    private void DormirAction()
    {
        AldeanoRenderer.enabled = false;
        Aldeano.velocity.Set(0, 0, 0);
        if (!estaAsustado)
        {
            horaDespertar = Random.Range(8, 10);
        }
        else
        {
            horaDespertar = Globales.horaActual + 4;
            estaAsustado = false;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].thisCollider.tag == "aldeanovision")
        {
            if(collision.collider.tag == "zombi" && AldeanoFSM_FSM.GetCurrentState().Name == "Avanzando" && !estaAsustado)
            {
                estaAsustado = true;
                AldeanoFSM_FSM.Fire("Avanzando_Zombi");
            }
        }

        if (collision.gameObject.tag == "distrito1") 
        {
            distrito = 0;
        }else if (collision.gameObject.tag == "distrito2")
        {
            distrito = 1;
        }
        else if (collision.gameObject.tag == "distrito3")
        {
            distrito = 2;
        }
        else if (collision.gameObject.tag == "distrito4")
        {
            distrito = 3;
        }
        else if (collision.gameObject.tag == "distrito5")
        {
            distrito = 4;
        } 
        else if (collision.gameObject.tag == "distrito6")
        {
            distrito = 5;
        }
    }

}