using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    // collision detection stuff
    public QTEManager manager;
    public FishingManager fishManager;
    public GameObject QTEDDRobject;
    public GameObject fishingObject;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector3 viewDir = player.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horInput, 0f, vertInput);
        if (inputDir != Vector3.zero) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            rb.AddForce(-1 * inputDir * moveSpeed / 10f);
            Debug.Log("Turning");
        }
    }



    // uhhh sorry this is the quickest way i could think of for the collision detection if theres a better way lmk but the demo deadline was too close T-T
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("QTE"))
        {
            manager.QTE();
        }

        if (other.CompareTag("DDR"))
        {
            manager.arrows();
        }

        if (other.CompareTag("Fishing"))
        {
            fishManager.fishing();
        }
    }
}
