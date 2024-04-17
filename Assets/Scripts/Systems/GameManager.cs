using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public static event Action<GameState> OnStateChange;

    public enum GameState {
        Menu,
        Options,
        Game,
        Lose,
        Win
    }

    public GameState State;

    private void Awake() {
        Instance = this;
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.Menu:
                break;
            case GameState.Options:
                break;
            case GameState.Game:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
