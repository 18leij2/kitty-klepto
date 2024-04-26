using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : QTE
{
    public GameObject fishingHolder;
    public Image imageToScale;
    public Image timerToScale;
    public float maxScaleY = 4.36f;
    public float scaleSpeed = 50f;
    public float timerSpeed = 25f;
    public bool isFishing = false;
    public Image scanner;
    public Image area;
    RectTransform scannerRectTransform;
    RectTransform areaRectTransform;
    public float areaSpeed = 100f;
    public float scannerSpeed = 400f;
    public int moveHold = 0;
    public int moveHoldNum = 25;
    public int moveCase = 0;

    public QTEManager qteScript;
    public GameObject glowObject;

    private Vector3 initialPosition;
    private Vector3 initialPositionTimer;

    public bool isQTE = false;

    // dialogue
    // for dialogue systems
    public Dialogue dialogueScript;
    public string[] dialogueFishing = { "Pratik's critique! Now that I've returned this item to the fish vendor, I should go call Pratik...",
                                       "I hope no one saw me put that item back... the chef was giving me weird stares, I wonder if he suspects me.",
                                       "If only I could talk to Pratik again... ever since he broke up with me for commiting genocide, life has not been the same."};
    public string[] dialogueMuseum = { "Pratik, I stayed up for many hours working on finishing this game. Please give me a break!",
                                       "Sometimes, a life of kleptomaniacy and crime is not one to be desired. My work is simultaneously my passion and my curse.", 
                                       "My remorse knows no bounds, the risks far outweigh the rewards."};

    // camera stuff
    public GlobalCameraManager camManager;
    public CinemachineVirtualCamera museumDoor;
    public CinemachineVirtualCamera museumInside;

    public GameObject museumReturn;
    public GameObject museumTP;
    public GameObject player;
    public GameObject museumZone;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the image
        initialPosition = imageToScale.rectTransform.localPosition;
        initialPositionTimer = timerToScale.rectTransform.localPosition;
        scannerRectTransform = scanner.rectTransform;
        areaRectTransform = area.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            fishing(this.gameObject);
        }

        if (isFishing)
        {
            int actionTaken = Random.Range(0, 4); // 0 is go down, 1 is do nothing, 2 is go up
            if (moveHold == 0)
            {
                moveCase = actionTaken;
                moveHold = moveHoldNum;
            }
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            switch (moveCase)
            {
                case 0:
                    if (currScannerPosition.y > -205)
                    {
                        float yPosition = currScannerPosition.y + (-scannerSpeed * Time.deltaTime);
                        scannerRectTransform.anchoredPosition = new Vector2(currScannerPosition.x, yPosition);
                    }
                    break;

                case 1:
                    break;
                case 2:
                    if (currScannerPosition.y < 214)
                    {
                        float yPosition = currScannerPosition.y + (scannerSpeed * Time.deltaTime);
                        scannerRectTransform.anchoredPosition = new Vector2(currScannerPosition.x, yPosition);
                    }
                    break;
            }
            moveHold--;

            if (timerToScale.rectTransform.localScale.y > 0)
            {
                // Increase the image's scale on the y-axis
                timerToScale.rectTransform.localScale += new Vector3(0, -timerSpeed * Time.deltaTime, 0);

                // Adjust the position to keep the bottom of the image fixed
                float deltaY = (timerToScale.rectTransform.pivot.y) * timerToScale.rectTransform.rect.height * (-timerSpeed * Time.deltaTime);
                timerToScale.rectTransform.localPosition += new Vector3(0, deltaY, 0);
            } else
            {
                isQTE = false;
                isFishing = false;
                Debug.Log("Lose fishing");
                FailGame();
                fishingHolder.SetActive(false);
            }

            // Check if the image's scale on the y-axis is less than the maximum scale
            if (imageToScale.rectTransform.localScale.y < maxScaleY)
            {
                currScannerPosition = scannerRectTransform.anchoredPosition;
                Vector2 currAreaPosition = areaRectTransform.anchoredPosition;
                float areaHeightHalf = areaRectTransform.sizeDelta.y / 2;

                if (currScannerPosition.y >= currAreaPosition.y - areaHeightHalf && currScannerPosition.y <= currAreaPosition.y + areaHeightHalf)
                {
                    // Increase the image's scale on the y-axis
                    imageToScale.rectTransform.localScale += new Vector3(0, scaleSpeed * Time.deltaTime, 0);

                    // Adjust the position to keep the bottom of the image fixed
                    float deltaY = (imageToScale.rectTransform.pivot.y) * imageToScale.rectTransform.rect.height * (scaleSpeed * Time.deltaTime);
                    imageToScale.rectTransform.localPosition += new Vector3(0, deltaY, 0);
                } else if (imageToScale.rectTransform.localScale.y > 0)
                {
                    // Increase the image's scale on the y-axis
                    imageToScale.rectTransform.localScale += new Vector3(0, -scaleSpeed * Time.deltaTime, 0);

                    // Adjust the position to keep the bottom of the image fixed
                    float deltaY = (imageToScale.rectTransform.pivot.y) * imageToScale.rectTransform.rect.height * (-scaleSpeed * Time.deltaTime);
                    imageToScale.rectTransform.localPosition += new Vector3(0, deltaY, 0);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    if (currAreaPosition.y < 175)
                    {
                        currAreaPosition = areaRectTransform.anchoredPosition;
                        float yPosition = currAreaPosition.y + (areaSpeed * Time.deltaTime);
                        areaRectTransform.anchoredPosition = new Vector2(currAreaPosition.x, yPosition);
                    }  
                } else if (currAreaPosition.y > -166)
                {
                    currAreaPosition = areaRectTransform.anchoredPosition;
                    float yPosition = currAreaPosition.y + (-areaSpeed * Time.deltaTime);
                    areaRectTransform.anchoredPosition = new Vector2(currAreaPosition.x, yPosition);
                }
            } else
            {
                isQTE = false;
                isFishing = false;
                Debug.Log("Win fishing");
                SucceedGame();
                initiateDialogue();
                glowObject.SetActive(false);
                fishingHolder.SetActive(false);
            }
        }
    }

     public void fishing(GameObject inObject)
    {
        isQTE = true;
        glowObject = inObject;

        switch (qteScript.difficulty)
        {
            case 1:
                scaleSpeed = 1.5f;
                scannerSpeed = 200f;
                break;
            case 2:
                scaleSpeed = 1.25f;
                scannerSpeed = 225f;
                break;
            case 3:
                scaleSpeed = 1.25f;
                scannerSpeed = 250f;
                break;
        }

        Vector2 currScale = imageToScale.rectTransform.localScale;
        Vector2 currPosition = imageToScale.rectTransform.localPosition;
        imageToScale.rectTransform.localScale = new Vector2(currScale.x, 0);
        imageToScale.rectTransform.localPosition = new Vector2(currPosition.x, -212);       

        Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
        Vector2 currAreaPosition = areaRectTransform.anchoredPosition;

        scannerRectTransform.anchoredPosition = new Vector2(currScannerPosition.x, -205);
        areaRectTransform.anchoredPosition = new Vector2(currAreaPosition.x, -166);

        fishingHolder.SetActive(true);
        isFishing = true;
    }

    private void initiateDialogue()
    {
        if (glowObject.CompareTag("Fishing"))
        {
            dialogueScript.startDialogue(dialogueFishing);
        }
        else if (glowObject.CompareTag("Museum QTE"))
        {
            camManager.SwitchPerspectiveCam(museumDoor);
            player.transform.position = museumReturn.transform.position;
            museumZone.SetActive(false);
            dialogueScript.startDialogue(dialogueMuseum);
        }
    }
}
