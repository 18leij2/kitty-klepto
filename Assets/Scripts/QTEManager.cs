using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public int difficulty = 1; // 1 is easy, 2 is medium, 3 is hard

    // for qte ping
    public GameObject QTEHolder;
    public Image scanner;
    public Image area;
    public bool scannerOn = false;
    public float scannerSpeed = 200f;
    public float areaClosestPosition = 120f;
    RectTransform scannerRectTransform;
    RectTransform areaRectTransform;

    // for qte arrows
    public bool arrowsOn = false;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public RectTransform parentTransform;
    public int numberOfImages = 3; // max should be 7!!
    public float spacing = 150f;
    public List<GameObject> order = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        scannerRectTransform = scanner.rectTransform;
        areaRectTransform = area.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            QTE();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arrows();
        }

        if (scannerOn)
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            float xPosition = currScannerPosition.x + (scannerSpeed * Time.deltaTime);
            scannerRectTransform.anchoredPosition = new Vector2(xPosition, currScannerPosition.y);
            if (xPosition >= 267)
            {
                scannerOn = false;
                Debug.Log("Timeout...");
                QTEHolder.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            Vector2 currAreaPosition = areaRectTransform.anchoredPosition;
            float areaWidthHalf = areaRectTransform.sizeDelta.x / 2;

            if (currScannerPosition.x >= currAreaPosition.x - areaWidthHalf && currScannerPosition.x <= currAreaPosition.x + areaWidthHalf)
            {
                scannerOn = false;
                Debug.Log("Success!");
                QTEHolder.SetActive(false);
            } 
            else if (scannerOn)
            {
                scannerOn = false;
                Debug.Log("Fail...");
                QTEHolder.SetActive(false);
            } 
        }

        if (arrowsOn)
        {

        }
    }

    void QTE()
    {
        QTEHolder.SetActive(true);
        switch (difficulty)
        {
            case 1:
                scannerSpeed = 200;
                areaClosestPosition = 120;
                break;
            case 2:
                scannerSpeed = 300;
                areaClosestPosition = 0;
                break;
            case 3:
                scannerSpeed = 400;
                areaClosestPosition = -120;
                break;
        }

        Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
        Vector2 currAreaPosition = areaRectTransform.anchoredPosition;

        float areaPosition = Random.Range(areaClosestPosition, 245f);

        scannerRectTransform.anchoredPosition = new Vector2(-266, currScannerPosition.y);
        areaRectTransform.anchoredPosition = new Vector2(areaPosition, currAreaPosition.y);
        scannerOn = true;
    }

    void arrows()
    {
        switch (difficulty)
        {
            case 1:
                numberOfImages = 3;
                break;
            case 2:
                numberOfImages = 5;
                break;
            case 3:
                numberOfImages = 7;
                break;
        }

        // Calculate the total width of the row of images to center the squares
        float totalWidth = numberOfImages * spacing;

        // Calculate the starting x-position to center the row of images
        float startX = -totalWidth / 2f + spacing / 2f;
        
        for (int i = 0; i < numberOfImages; i++)
        {
            // Calculate position for the new image
            Vector2 newPosition = new Vector2(startX + spacing * i, 0);

            // Instantiate the image prefab
            int arrowKey = Random.Range(0, 4);
            GameObject newImage = null;
            switch (arrowKey)
            {
                case 0:
                    newImage = Instantiate(up, parentTransform);        
                    break;
                case 1:
                    newImage = Instantiate(down, parentTransform);
                    break;
                case 2:
                    newImage = Instantiate(left, parentTransform);
                    break;
                case 3:
                    newImage = Instantiate(right, parentTransform);
                    break;
            }

            if (newImage != null)
            {
                order.Add(newImage);
                // Set the position of the new image
                RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
                newImageRectTransform.anchoredPosition = newPosition;
            }          
        }
    }
}
