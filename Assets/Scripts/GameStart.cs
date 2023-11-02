using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameStatesType
{
    RACE_INITAL,
    RACE_START,
    RACE_PLAYING,
    RACE_END
}
public class GameStart : MonoBehaviour
{
    const string SAVE_FILE_NAME = "gameSave.json";
    [SerializeField] private List<RaceableObject> _listOfRaceableObject;
    public LanesText LanesText;
    public GameInput GameInput;
    public UIButtons UIButtonEvents;
    public RaceableObjectManager RaceableObjectManager;
    public MathSO MathSO;

    private StateMachine<GameStart> _stateMachine;
    private Dictionary<GameStatesType, State<GameStart>> _mapOfStates;
    private GameStatesType currentState;

    /// <summary>
    /// Data we will save to the game
    /// </summary>
    private GameSaveData _saveData;

    /// <summary>
    /// USe this service to help save the data
    /// </summary>
    private JsonDataService _jsonDataService;


    private void Start()
    {
        currentState = GameStatesType.RACE_INITAL;
        // if no data is loaded lets create a default
        if (_saveData == null)
        {
            _saveData = new GameSaveData();
        } 
        else
        {
            // set the game data up
            _saveData.CopyRaceableObjectDataTo(_listOfRaceableObject);
            currentState = _saveData.GameState;
        }
       
        RaceableObjectManager = new RaceableObjectManager(_listOfRaceableObject);
        this._stateMachine.Initialize(this._mapOfStates[currentState]);
    }

    private void Awake()
    {
        _jsonDataService = new JsonDataService();
        _saveData = _jsonDataService.LoadData<GameSaveData>(SAVE_FILE_NAME);
        this.SetUpStateMachine();
    }

    private void Update()
    {
        this._stateMachine.CurrentState.FrameUpdate();
    }

    private void LateUpdate()
    {
        this._stateMachine.CurrentState.PhysicsUpdate();
    }

    /// <summary>
    /// Change the current state of the game
    /// </summary>
    /// <param name="stateType">State we want to change to</param>
    public void ChangeState(GameStatesType stateType)
    {
        currentState = stateType;
        _saveData.UpdateSaveData(stateType, RaceableObjectManager);
        _jsonDataService.SaveData<GameSaveData>(SAVE_FILE_NAME, _saveData);
        this._stateMachine.ChangeState(this._mapOfStates[stateType]);
    }

    /// <summary>
    /// Set up the state machine
    /// </summary>
    private void SetUpStateMachine()
    {
        this._stateMachine = new StateMachine<GameStart>();
        this._mapOfStates = new Dictionary<GameStatesType, State<GameStart>>();
        this._mapOfStates.Add(GameStatesType.RACE_INITAL, new RaceInitalState(this));
        this._mapOfStates.Add(GameStatesType.RACE_START, new RaceStartState(this));
        this._mapOfStates.Add(GameStatesType.RACE_PLAYING, new RacePlayingState(this));
        this._mapOfStates.Add(GameStatesType.RACE_END, new RaceEndState(this));
    }

    public void InitializeRace()
    {
        this.RaceableObjectManager.Initalize(MathSO.MinWeight, MathSO.MaxWeight, MathSO.MinDuration, MathSO.MaxDuration);
    }
}
