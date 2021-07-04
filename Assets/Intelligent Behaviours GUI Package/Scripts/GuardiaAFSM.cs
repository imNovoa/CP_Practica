using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardiaAFSM : MonoBehaviour {

    #region variables

    private StateMachineEngine GuardiaAFSM_FSM;
    

    private ValuePerception NewTransitionPerception;
    private PushPerception NewTransition1Perception;
    private PushPerception NewTransition2Perception;
    private PushPerception NewTransition3Perception;
    private ValuePerception NewTransition4Perception;
    private PushPerception NewTransition5Perception;
    private ValuePerception NewTransition6Perception;
    private ValuePerception NewTransition7Perception;
    private PushPerception NewTransition8Perception;
    private ValuePerception NewTransition9Perception;
    private ValuePerception NewTransition10Perception;
    private ValuePerception NewTransition11Perception;
    private ValuePerception NewTransition12Perception;
    private ValuePerception NewTransition13Perception;
    private ValuePerception NewTransition14Perception;
    private ValuePerception NewTransition15Perception;
    private ValuePerception NewTransition16Perception;
    private State Patrullando;
    private State VigilarPuerta;
    private State FormarPlaza;
    private State IrAPlaza;
    private State IrAPuerta;
    private State Perseguirformacion;
    private State Buscarformacion;
    private State Perseguirpuerta;
    private State Buscarpuerta;
    private State Perseguir;
    private State Buscar;
    private Vector3 posicionGuardia; //se calcula en el estado en el que están esperando ordenes
    private Vector3 posicionFormacion; //asignar identificador a los guardias
    
    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        GuardiaAFSM_FSM = new StateMachineEngine(false);
        

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        NewTransitionPerception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition1Perception = GuardiaAFSM_FSM.CreatePerception<PushPerception>();
        NewTransition2Perception = GuardiaAFSM_FSM.CreatePerception<PushPerception>();
        NewTransition3Perception = GuardiaAFSM_FSM.CreatePerception<PushPerception>();
        NewTransition4Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition5Perception = GuardiaAFSM_FSM.CreatePerception<PushPerception>();
        NewTransition6Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition7Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition8Perception = GuardiaAFSM_FSM.CreatePerception<PushPerception>();
        NewTransition9Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition10Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition11Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition12Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition13Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition14Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition15Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        NewTransition16Perception = GuardiaAFSM_FSM.CreatePerception<ValuePerception>(() => false /*Replace this with a boolean function*/);
        
        // States
        Patrullando = GuardiaAFSM_FSM.CreateEntryState("Patrullando", PatrullandoAction);
        VigilarPuerta = GuardiaAFSM_FSM.CreateState("VigilarPuerta", VigilarPuertaAction);
        FormarPlaza = GuardiaAFSM_FSM.CreateState("FormarPlaza", FormarPlazaAction);
        IrAPlaza = GuardiaAFSM_FSM.CreateState("IrAPlaza", IrAPlazaAction);
        IrAPuerta = GuardiaAFSM_FSM.CreateState("IrAPuerta", IrAPuertaAction);
        Perseguirformacion = GuardiaAFSM_FSM.CreateState("Perseguir(formacion)", PerseguirformacionAction);
        Buscarformacion = GuardiaAFSM_FSM.CreateState("Buscar(formacion)", BuscarformacionAction);
        Perseguirpuerta = GuardiaAFSM_FSM.CreateState("Perseguir(puerta)", PerseguirpuertaAction);
        Buscarpuerta = GuardiaAFSM_FSM.CreateState("Buscar(puerta)", BuscarpuertaAction);
        Perseguir = GuardiaAFSM_FSM.CreateState("Perseguir", PerseguirAction);
        Buscar = GuardiaAFSM_FSM.CreateState("Buscar", BuscarAction);
        
        // Transitions
        GuardiaAFSM_FSM.CreateTransition("Formar en la puerta", IrAPuerta, NewTransitionPerception, VigilarPuerta);
        GuardiaAFSM_FSM.CreateTransition("Recibir ordenes", FormarPlaza, NewTransition1Perception, IrAPuerta);
        GuardiaAFSM_FSM.CreateTransition("Ir a formar", VigilarPuerta, NewTransition2Perception, IrAPlaza);
        GuardiaAFSM_FSM.CreateTransition("Patrulla plaza", Patrullando, NewTransition3Perception, IrAPlaza);
        GuardiaAFSM_FSM.CreateTransition("Esta formando", IrAPlaza, NewTransition4Perception, FormarPlaza);
        GuardiaAFSM_FSM.CreateTransition("Formar plaza zombie", FormarPlaza, NewTransition5Perception, Perseguirformacion);
        GuardiaAFSM_FSM.CreateTransition("Busca zombie formacion", Perseguirformacion, NewTransition6Perception, Buscarformacion);
        GuardiaAFSM_FSM.CreateTransition("Vuelve a plaza", Buscarformacion, NewTransition7Perception, FormarPlaza);
        GuardiaAFSM_FSM.CreateTransition("Puerta zombie", VigilarPuerta, NewTransition8Perception, Perseguirpuerta);
        GuardiaAFSM_FSM.CreateTransition("Busca zombie puerta", Perseguirpuerta, NewTransition9Perception, Buscarpuerta);
        GuardiaAFSM_FSM.CreateTransition("Volver a la puerta", Buscarpuerta, NewTransition10Perception, VigilarPuerta);
        GuardiaAFSM_FSM.CreateTransition("Encuentra zombie formacion", Buscarformacion, NewTransition11Perception, Perseguirformacion);
        GuardiaAFSM_FSM.CreateTransition("Encuentra zombie puerta", Buscarpuerta, NewTransition12Perception, Perseguirpuerta);
        GuardiaAFSM_FSM.CreateTransition("Patrullando zombie", Patrullando, NewTransition13Perception, Perseguir);
        GuardiaAFSM_FSM.CreateTransition("Perseguir a buscar patrullando", Perseguir, NewTransition14Perception, Buscar);
        GuardiaAFSM_FSM.CreateTransition("Buscar a perseguir patrullando", Buscar, NewTransition15Perception, Perseguir);
        GuardiaAFSM_FSM.CreateTransition("Vuelve a patrular", Buscar, NewTransition16Perception, Patrullando);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        GuardiaAFSM_FSM.Update();
    }

    // Create your desired actions
    
    private void PatrullandoAction()
    {
        
    }
    
    private void VigilarPuertaAction()
    {
        
    }
    
    private void FormarPlazaAction()
    {
        
    }
    
    private void IrAPlazaAction()
    {
        
    }
    
    private void IrAPuertaAction()
    {
        
    }
    
    private void PerseguirformacionAction()
    {
        
    }
    
    private void BuscarformacionAction()
    {
        
    }
    
    private void PerseguirpuertaAction()
    {
        
    }
    
    private void BuscarpuertaAction()
    {
        
    }
    
    private void PerseguirAction()
    {
        
    }
    
    private void BuscarAction()
    {
        
    }
    
}