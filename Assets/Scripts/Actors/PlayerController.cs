using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;

    // collision detection stuff
    public QTEManager manager;
    public FishingManager fishManager;
    public GameObject QTEDDRobject;
    public GameObject fishingObject;

    public Image interact;
    public Camera mainCamera;
    public RectTransform uiCanvas;
    public RectTransform uiElement;

    private void Start() {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update() {
        Vector3 viewDir = player.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        
        Vector3 horInput = Input.GetAxis("Horizontal") * -camRight;
        Vector3 vertInput = Input.GetAxis("Vertical") * -camForward;
        Vector3 inputDir = horInput + vertInput;
        
        GameManager.GameState currentState = GameManager.Instance.State;
        if (inputDir != Vector3.zero && (!manager.isQTE && !fishManager.isQTE) && currentState == GameManager.GameState.Game) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            rb.AddForce(-1 * inputDir * moveSpeed / 10f);
            Debug.Log("Turning");
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }



    // uhhh sorry this is the quickest way i could think of for the collision detection if theres a better way lmk but the demo deadline was too close T-T
    private void OnTriggerEnter(Collider other)
    {
        objectToUI(other.gameObject);
        interact.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interact.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("QTE") && Input.GetKeyDown(KeyCode.E))
        {
            interact.gameObject.SetActive(false);
            manager.QTE(other.gameObject);
        }

        if (other.CompareTag("DDR") && Input.GetKeyDown(KeyCode.E))
        {
            interact.gameObject.SetActive(false);
            manager.arrows(other.gameObject);
        }

        if (other.CompareTag("Fishing") && Input.GetKeyDown(KeyCode.E))
        {
            interact.gameObject.SetActive(false);
            fishManager.fishing(other.gameObject);
        }

        if (other.CompareTag("Cinema") && Input.GetKeyDown(KeyCode.E))
        {
            interact.gameObject.SetActive(false);
            manager.difficulty = UnityEngine.Random.Range(2, 4);
            manager.QTE(other.gameObject);
        }
    }

    private void objectToUI(GameObject obj)
    {
        // Get the object's world position
        Vector3 objectWorldPos = obj.transform.position;

        // Convert world position to canvas space
        Vector2 viewportPoint = mainCamera.WorldToViewportPoint(objectWorldPos);
        Vector2 canvasSize = uiCanvas.sizeDelta;
        Vector2 canvasPos = new Vector2(
            (viewportPoint.x * canvasSize.x) - (canvasSize.x * 0.5f),
            (viewportPoint.y * canvasSize.y) - (canvasSize.y * 0.5f)
        );

        // Set UI element's position
        uiElement.localPosition = canvasPos;
    }
}
