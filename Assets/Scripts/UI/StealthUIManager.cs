using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class StealthUIManager {
    [SerializeField] private StealthScroll scrollbar;

    public void IncrementScrollBar(float currStealth, float maxStealth) {
        Scrollbar scroll = scrollbar.GetComponent<Scrollbar>();
        float ratio = currStealth / maxStealth;
        DOTween.To(() => scroll.size, x => scroll.size = x, ratio, 0.5f);
    }
}
