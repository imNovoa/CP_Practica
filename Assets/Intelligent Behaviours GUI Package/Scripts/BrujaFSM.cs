using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Random;

using ejemplo;

public class BrujaFSM : MonoBehaviour
{


    //public static Spawn s = new Spawn();

    #region variables

    private StateMachineEngine BrujaFSM_FSM;

    private IsInStatePerception ParadoBuscandoPerception;
    private PushPerception BuscandoAvanzandoPerception;
    private PushPerception SpawnearZombiePerception;
    private ValuePerception EstaEnDestinoPerception;
    private TimerPerception CooldownPerception;
    private State Parado;
    private State Buscando;
    private State Avanzando;
    private State SpawnearZombie;
    private Vector3[] positions = Spawn.GetSpawnpoints();
    private NavMeshAgent Bruja;
    private GameObject modelo;
    private MeshRenderer BrujaRenderer;
    private Vector3 destino = new Vector3(0, 0, 0);
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
    private int porcentajeSpawn;
    public GameObject zombieOriginal;
    public GameObject zombie;
    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        BrujaFSM_FSM = new StateMachineEngine(false);
        r = Random.Range(0, 30);
        

        CreateStateMachine();
    }


    private void CreateStateMachine()
    {


        // States
        Parado = BrujaFSM_FSM.CreateEntryState("Parado", ParadoAction);
        Buscando = BrujaFSM_FSM.CreateState("Buscando", BuscandoAction);
        Avanzando = BrujaFSM_FSM.CreateState("Avanzando", AvanzandoAction);
        SpawnearZombie = BrujaFSM_FSM.CreateState("SpawnearZombie", SpawnearZombieAction);




        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        ParadoBuscandoPerception = BrujaFSM_FSM.CreatePerception<IsInStatePerception>(BrujaFSM_FSM, "Parado");
        BuscandoAvanzandoPerception = BrujaFSM_FSM.CreatePerception<PushPerception>();
        SpawnearZombiePerception = BrujaFSM_FSM.CreatePerception<PushPerception>();
        CooldownPerception = BrujaFSM_FSM.CreatePerception<TimerPerception>(3);
        EstaEnDestinoPerception = BrujaFSM_FSM.CreatePerception<ValuePerception>(() => Vector3.Distance(destino, Bruja.transform.position) <= 10.0f);

        // ExitPerceptions

        // Transitions
        BrujaFSM_FSM.CreateTransition("Parado_Buscando", Parado, ParadoBuscandoPerception, Buscando);
        BrujaFSM_FSM.CreateTransition("Buscando_Avanzando", Buscando, BuscandoAvanzandoPerception, Avanzando);
        BrujaFSM_FSM.CreateTransition("Buscando_Spawnear", Buscando, SpawnearZombiePerception, SpawnearZombie);
        BrujaFSM_FSM.CreateTransition("Spawnear_Parado", SpawnearZombie, CooldownPerception, Parado);
        BrujaFSM_FSM.CreateTransition("EstaEnDestino", Avanzando, EstaEnDestinoPerception, Parado);

        // ExitTransitions

    }

    // Update is called once per frame
    private void Update()
    {
        BrujaFSM_FSM.Update();
    }

    // Create your desired actions


    private void BuscandoAction()
    {
        porcentajeSpawn = Random.Range(0, 100);
        if (porcentajeSpawn < 70)
        {
            r = Random.Range(0, 30);
            destino = positions[r];
            Bruja.SetDestination(destino);
            BrujaFSM_FSM.Fire("Buscando_Avanzando");
        }
        else
        {
            BrujaFSM_FSM.Fire("Buscando_Spawnear");
        }
    }

    private void SpawnearZombieAction()
    {
        Quaternion spawnRotation = Quaternion.identity;
        zombie = Instantiate(zombieOriginal, this.transform.position, spawnRotation);
        zombie.SetActive(true);
    }

    private void AvanzandoAction()
    {

    }

    private void ParadoAction()
    {
        Bruja.velocity.Set(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "distrito1")
        {
            distrito = 0;
        }
        else if (collision.gameObject.tag == "distrito2")
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