using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AldeanoFSM : MonoBehaviour {


    public static Spawn s = new Spawn();

    #region variables

    private StateMachineEngine AldeanoFSM_FSM;
    

    private IsInStatePerception ParadoBuscandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private ValuePerception EstaEnDestinoPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;
    private Vector3[] positions = s.getPosiciones(); 
    private NavMeshAgent Aldeano;
    private Vector3 destino = new Vector3(0,0,0);
    private float r;

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
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = AldeanoFSM_FSM.CreatePerception<IsInStatePerception>(AldeanoFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = AldeanoFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = AldeanoFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Aldeano.transform.position) <= 1);
        
        // States
        Parado = AldeanoFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = AldeanoFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = AldeanoFSM_FSM.CreateState("Avanzando", AvanzandoAction);
        
        // Transitions
        AldeanoFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        AldeanoFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        AldeanoFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);
        
        // ExitPerceptions
        
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
        
        Mathf.Round(r);
        destino = positions[(int)r];
        Aldeano.SetDestination(destino);
    }
    
    private void AvanzandoAction()
    {
        
    }
    
}