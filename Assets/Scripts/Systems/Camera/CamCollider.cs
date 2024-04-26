using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamCollider : MonoBehaviour {
    private CinemachineVirtualCamera _camera;

    private GlobalCameraManager _manager;
    // Start is called before the first frame update
    void Start() {
        _camera = transform.parent.GetComponent<CinemachineVirtualCamera>();
        _manager = transform.GetComponentInParent<GlobalCameraManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.State == GameManager.GameState.Game) {
            if (other.transform.GetComponent<PlayerController>() != null) _manager.SwitchPerspectiveCam(_camera);
        }
    }
}
