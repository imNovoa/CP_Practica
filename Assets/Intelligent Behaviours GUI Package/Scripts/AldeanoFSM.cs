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
    private ValuePerception EstaEnDestinoPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;
    private Vector3[] positions = Spawn.GetSpawnpoints();
    private NavMeshAgent Aldeano;
    private Vector3 destino = new Vector3(0,0,0);
    private int r;
    private int distrito;

    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        AldeanoFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        Aldeano = GetComponent<NavMeshAgent>();
        

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        
        
        // States
        Parado = AldeanoFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = AldeanoFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = AldeanoFSM_FSM.CreateState("Avanzando", AvanzandoAction);

        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = AldeanoFSM_FSM.CreatePerception<IsInStatePerception>(AldeanoFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = AldeanoFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Aldeano.transform.position) <= 5.0f);
        // ExitPerceptions

        // Transitions
        AldeanoFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        AldeanoFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        AldeanoFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);

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

    

    private void OnCollisionEnter(Collision collision)
    {
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