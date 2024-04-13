using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public Image scanner;
    public Image area;
    public bool scannerOn = false;
    public float scannerSpeed = 200f;
    RectTransform scannerRectTransform;
    RectTransform areaRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        scannerRectTransform = scanner.rectTransform;
        areaRectTransform = area.rectTransform;
        QTE();
    }

    // Update is called once per frame
    void Update()
    {
        if (scannerOn)
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            float xPosition = currScannerPosition.x + (scannerSpeed * Time.deltaTime);
            scannerRectTransform.anchoredPosition = new Vector2(xPosition, currScannerPosition.y);
            if (xPosition >= 267)
            {
                scannerOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            Vector2 currAreaPosition = areaRectTransform.anchoredPosition;
            float areaWidthHalf = areaRectTransform.sizeDelta.x / 2;

            if (currScannerPosition.x >= currAreaPosition.x - areaWidthHalf && currScannerPosition.x <= currAreaPosition.x + areaWidthHalf)
            {
                Debug.Log("Success!");
            } else
            {
                Debug.Log("Fail...");
            }
        }
    }

    void QTE()
    {    
        Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
        Vector2 currAreaPosition = areaRectTransform.anchoredPosition;

        float areaPosition = Random.Range(0f, 245f);

        scannerRectTransform.anchoredPosition = new Vector2(-266, currScannerPosition.y);
        areaRectTransform.anchoredPosition = new Vector2(areaPosition, currAreaPosition.y);
        scannerOn = true;
    }
}
