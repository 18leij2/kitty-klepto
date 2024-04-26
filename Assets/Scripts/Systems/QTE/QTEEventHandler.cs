using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEEventHandler : MonoBehaviour {
    
    private static QTEEventHandler _instance;
    public static QTEEventHandler Instance => _instance;
    
    
    [SerializeField] private List<QTE> minigames;

    public new delegate void QTEComplete(float stealth);
    public static event QTEComplete OnQTEComplete;

    private void Awake() {
        if (_instance != null ) { Destroy(this.gameObject); }
        else { _instance = this; }
        
        foreach (QTE qte in minigames) {
            qte.OnSuccess += Success;
            qte.OnFail += QTECompletion;
        }
    }

    private void Success(float stealth) {
        QTECompletion(stealth);
        AudioManager.Instance.PlaySoundEffect(2);
    }

    private void QTECompletion(float stealth) {
        OnQTEComplete?.Invoke(stealth);
    }
    
}