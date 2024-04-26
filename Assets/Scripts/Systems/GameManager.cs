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
                AudioManager.Instance.PlayTrack(0);
                Debug.Log("Game State: Menu");
                break;
            case GameState.Cutscene:
                AudioManager.Instance.SetVolume(0f);
                Debug.Log("Game State: Cutscene");
                break;
            case GameState.Game:
                AudioManager.Instance.SetVolume(0.6f);
                Debug.Log("Game State: Game");
                break;
            case GameState.Dialogue:
                AudioManager.Instance.SetVolume(0.1f);
                Debug.Log("Game State: Dialogue");
                break;
            case GameState.Reset:
                Debug.Log("Game State: Reset");
                break;
            case GameState.Pause:
                Debug.Log("Game State: Pause");
                break;
            case GameState.Win:
                AudioManager.Instance.FadeOut();
                Debug.Log("Game State: Win");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void Update() {
        if (State == GameState.Game) {
            if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.P)) {
                UpdateGameState(GameState.Win);
            }
        }
    }
}
