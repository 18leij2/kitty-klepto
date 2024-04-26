using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GlobalCameraManager : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera menuCam;
    [SerializeField] private List<CinemachineVirtualCamera> perspectiveCams;
    
    [Tooltip("The first camera to load in the perspective camera index. Default is 0.")]
    [SerializeField] private int firstLoadIndex = 0;

    private int _perspectiveCamLoadIndex = 0;

    private void Start() {
        GameManager.Instance.OnStateTransition += HandleCameraStates;
        _perspectiveCamLoadIndex = firstLoadIndex;
        SwitchToMenuCam();
    }

    private void HandleCameraStates(GameManager.GameState state) {
        switch (state) {
            case GameManager.GameState.Menu:
                SwitchToMenuCam();
                break;
            case GameManager.GameState.Cutscene:
                break;
            case GameManager.GameState.Game:
                SwitchToPerspectiveCam();
                break;
            case GameManager.GameState.Reset:
                break;
            case GameManager.GameState.Win:
                break;
        }
    }

    private void SwitchToMenuCam() {
        menuCam.m_Priority = 1;
        _perspectiveCamLoadIndex = firstLoadIndex;
        ResetCamParams();
    }

    public void SwitchPerspectiveCam(CinemachineVirtualCamera camera) {
        menuCam.m_Priority = 0;
        for (int i = 0; i < perspectiveCams.Count; i++) {
            if (camera == perspectiveCams[i]) {
                _perspectiveCamLoadIndex = i;
                break;
            }
        }
        SwitchToPerspectiveCam();
    }

    private void SwitchToPerspectiveCam() {
        menuCam.m_Priority = 0;
        ResetCamParams();
        perspectiveCams[_perspectiveCamLoadIndex].m_Priority = 1;
    }

    private void ResetCamParams() {
        foreach (CinemachineVirtualCamera cam in perspectiveCams) {
            cam.m_Priority = 0;
        }
    }
}
