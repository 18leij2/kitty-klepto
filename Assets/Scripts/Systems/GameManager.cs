using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance => instance;
    
    public delegate void StateTransition(GameState state);
    public event StateTransition OnStateTransition;

    public enum GameState {
        Menu,
        Cutscene,
        Game,
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
                Debug.Log("Game State: Options");
                break;
            case GameState.Game:
                Debug.Log("Game State: Game");
                break;
            case GameState.Reset:
                Debug.Log("Game State: Reset");
                break;
            case GameState.Win:
                Debug.Log("Game State: Win");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
