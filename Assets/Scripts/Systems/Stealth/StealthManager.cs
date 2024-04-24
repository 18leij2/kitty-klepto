using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthManager : MonoBehaviour {

    [SerializeField] private float maxStealth;
    [SerializeField] private StealthUIManager uiManager;
    [SerializeField] private float _stealth;

    private void Awake() {
        _stealth = maxStealth;
        QTEEventHandler.OnQTEComplete += AdjustStealth;
    }

    private void AdjustStealth(float stealthReduction) {
        _stealth -= stealthReduction;
        uiManager.IncrementScrollBar(_stealth, maxStealth);
    }
}
