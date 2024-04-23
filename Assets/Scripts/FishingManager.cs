using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
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
                isFishing = false;
                Debug.Log("Lose fishing");
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
                isFishing = false;
                Debug.Log("Win fishing");
                fishingHolder.SetActive(false);
            }
        }
    }

     public void fishing(GameObject inObject)
    {
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
}
