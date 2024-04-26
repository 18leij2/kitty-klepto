using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthManager : MonoBehaviour {

    [SerializeField] private float maxStealth;
    [SerializeField] private StealthUIManager uiManager;
    [SerializeField] private float stealth;
    
    private void Awake() {
        stealth = maxStealth;
        QTEEventHandler.OnQTEComplete += AdjustStealth;
    }

    private void Start() {
        GameManager.Instance.OnStateTransition += ToggleDisplay;
    }

    private void ToggleDisplay(GameManager.GameState state) {
        uiManager.ToggleDisplay(state);
    }

    private void AdjustStealth(float stealthReduction) {
        stealth -= stealthReduction;
        uiManager.IncrementScrollBar(stealth, maxStealth);
        ConditionCheck();
    }

    private void ConditionCheck() {
        if (stealth <= 0) {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Reset);
        }
    }
}
