using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance => instance;
    
    public delegate void StateTransition(GameState state);
    public event StateTransition OnStateTransition;

    public int returns = 0;

    public enum GameState {
        Menu,
        Cutscene,
        Game,
        Dialogue,
        Pause,
        Reset,
        Win
    }

    public GameState State;

    private void Awake() {
        instance = this;
        UpdateGameState(GameState.Menu);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;
        OnStateTransition?.Invoke(State);
        switch (newState) {
            case GameState.Menu:
                Debug.Log("Game State: Menu");
                break;
            case GameState.Cutscene:
                Debug.Log("Game State: Cutscene");
                break;
            case GameState.Game:
                Debug.Log("Game State: Game");
                break;
            case GameState.Dialogue:
                Debug.Log("Game State: Dialogue");
                break;
            case GameState.Reset:
                Debug.Log("Game State: Reset");
                break;
            case GameState.Pause:
                Debug.Log("Game State: Pause");
                break;
            case GameState.Win:
                Debug.Log("Game State: Win");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
