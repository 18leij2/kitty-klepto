using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class StealthUIManager {
    [SerializeField] private CanvasGroup display;

    public void IncrementScrollBar(float currStealth, float maxStealth) {
        Scrollbar scroll = display.GetComponentInChildren<Scrollbar>();
        float ratio = currStealth / maxStealth;
        DOTween.To(() => scroll.size, x => scroll.size = x, ratio, 0.5f);
    }
    
    public void ToggleDisplay(GameManager.GameState state) {
        if (state == GameManager.GameState.Menu) Toggle(false);
        else if (state == GameManager.GameState.Game) Toggle(true);
    }

    private void Toggle(bool toggle) {
        Debug.Log("toggling");
        display.alpha = toggle ? 1f : 0f;
    }
}
