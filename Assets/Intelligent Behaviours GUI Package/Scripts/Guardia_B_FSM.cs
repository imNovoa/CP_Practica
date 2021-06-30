using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Random;

using ejemplo;

public class Guardia_B_FSM : MonoBehaviour {


    //public static Spawn s = new Spawn();

    #region variables

    private StateMachineEngine GuardiaFSM_FSM;
    

    private IsInStatePerception ParadoBuscandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private ValuePerception EstaEnDestinoPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;
    private Vector3[] positions = Spawn.GetSpawnpoints();
    private NavMeshAgent Guardia;
    private Vector3 destino = new Vector3(0,0,0);
    private int r;

    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        GuardiaFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        Guardia = GetComponent<NavMeshAgent>();

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        
        
        // States
        Parado = GuardiaFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = GuardiaFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = GuardiaFSM_FSM.CreateState("Avanzando", AvanzandoAction);

        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = GuardiaFSM_FSM.CreatePerception<IsInStatePerception>(GuardiaFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = GuardiaFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Guardia.transform.position) <= 5.0f);
        // ExitPerceptions

        // Transitions
        GuardiaFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        GuardiaFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        GuardiaFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);

        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        GuardiaFSM_FSM.Update();
    }

    // Create your desired actions
    
    private void ParadoAction()
    {
        Guardia.velocity.Set(0, 0, 0);
    }
    
    private void BuscandoAction()
    {
        r = Random.Range(0, 30);
        destino = positions[r];
        Guardia.SetDestination(destino);
        GuardiaFSM_FSM.Fire("Buscando_Avanzando");
    }
    
    private void AvanzandoAction()
    {
        
    }
    
}