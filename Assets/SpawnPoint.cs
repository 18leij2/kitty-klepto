using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start() {
        GameManager.Instance.OnStateTransition += ResetPlayer;
    }

    public void ResetPlayer(GameManager.GameState state) {
        if (state is GameManager.GameState.Menu) {
            Transform player = FindObjectOfType<PlayerController>().transform;
            player.position = transform.position;
            player.rotation = transform.rotation;
        }
    }
}
