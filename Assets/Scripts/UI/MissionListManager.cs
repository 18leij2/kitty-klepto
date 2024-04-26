using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionListManager : MonoBehaviour {
    private CanvasGroup _missionList;
    private bool _active;

    private void Awake() {
        GetComponent<CanvasGroup>().alpha = 0f;
        _missionList = transform.GetChild(1).GetComponent<CanvasGroup>();
    }

    private void Start() {
        GameManager.Instance.OnStateTransition += EnableMissonList;
        _missionList.transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
    }

    public void EnableMissonList(GameManager.GameState state) {
        if (state == GameManager.GameState.Game) {
            GetComponent<CanvasGroup>().alpha = 1f;
        }
        else {
            GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    public void SetActive() {
        if (!_active) {
            _missionList.alpha = 1;
            _missionList.transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
            _missionList.transform.DOScaleX(1f, 0.5f).SetEase(Ease.OutBounce);
            _active = true;
        }
    }

    private void Update() {
        if (_active) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                _active = false;
                _missionList.transform.DOScaleX(0f, 0.5f).SetEase(Ease.InBounce);
            }
        }
    }
}
