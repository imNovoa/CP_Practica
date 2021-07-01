using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Random;

using ejemplo;

public class Guardia_A_FSM : MonoBehaviour {


    //public static Spawn s = new Spawn();
    private Vector3[] posGuardia;
    #region variables

    private StateMachineEngine GuardiaFSM_FSM;
    

    private IsInStatePerception ParadoBuscandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private ValuePerception EstaEnDestinoPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;


    private State Formando;
    private ValuePerception AFormarPerception;
    private ValuePerception ARomperFormacionPerception;


    private Vector3[] positions = Spawn.GetSpawnpoints();
    private Vector3[] formaciones = Spawn.GetFormaciones();
    private NavMeshAgent Guardia;
    private Vector3 destino = new Vector3(0,0,0);
    private int r;

    private bool midday = false;
    private bool midnight = false;
    private bool noon = false;
    private bool dawn = false;

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

        Formando = GuardiaFSM_FSM.CreateState("Formando", FormandoAction);

        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = GuardiaFSM_FSM.CreatePerception<IsInStatePerception>(GuardiaFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = GuardiaFSM_FSM.CreatePerception<PushPerception>();
        EstaEnDestinoPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Guardia.transform.position) <= 5.0f);

        AFormarPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => midday);
        ARomperFormacionPerception = GuardiaFSM_FSM.CreatePerception<ValuePerception>(() => !midday);

        // ExitPerceptions

        // Transitions
        GuardiaFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        GuardiaFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        GuardiaFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);

        GuardiaFSM_FSM.CreateTransition("EstaFormando", Avanzando, AFormarPerception, Formando);
        GuardiaFSM_FSM.CreateTransition("RomperFilas", Formando, ARomperFormacionPerception, Parado);


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
        if (!midday)
        {
            r = Random.Range(0, 30);
            destino = positions[r];
            Guardia.SetDestination(destino);
            GuardiaFSM_FSM.Fire("Buscando_Avanzando");
        } else
        {
            r = Random.Range(0, 11);
            destino = formaciones[r];
            Guardia.SetDestination(destino);
            GuardiaFSM_FSM.Fire("Buscando_Avanzando");
        }
        
    }

    private void FormandoAction()
    {
        Guardia.velocity.Set(0, 0, 0);
        
    }

    private void VigilandoAction()
    {
        Guardia.velocity.Set(0, 0, 0);
    }

    private void AvanzandoAction()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "midnight")
        {
            midday = false;
            midnight = true;
            noon = false;
            dawn = false;
        }
        else if (collision.gameObject.tag == "dawn")
        {
            midday = false;
            midnight = false;
            noon = false;
            dawn = true;
        }
        else if (collision.gameObject.tag == "midday")
        {
            midday = true;
            midnight = false;
            noon = false;
            dawn = false;
        }
        else if (collision.gameObject.tag == "noon")
        {
            midday = true;
            midnight = false;
            noon = false;
            dawn = false;
        }
    }

}