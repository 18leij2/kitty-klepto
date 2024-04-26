using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class QTEManager : QTE
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
    public GameObject arrowHolder;
    public bool arrowsOn = false;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public RectTransform parentTransform;
    public int numberOfImages = 3; // max should be 7!!
    public float spacing = 150f;
    public List<GameObject> order = new List<GameObject>();
    public int orderIndex = 0;
    public Image timerBar;
    RectTransform scaleRectTransform;
    public float timerSpeed = 3;

    // for tracking glow effect
    public GameObject glowObject;

    // for the player bool tracker
    public bool isQTE = false;

    // for dialogue systems
    public Dialogue dialogueScript;
    public string[] dialogueVending = { "Woah, Pratik! Now that I've returned this item to the vending machine, I should feel a little better...",
                                        "I hope no one saw me put that item back... in a way, this feels like a large burden has been lifted off my chest!",
                                        "If only I could talk to Pratik again... ever since he broke up with me for stealing, life has not been the same."};
    public string[] dialoguePoster = { "How Pratikular! Now that I've returned this poster of Teek to the wall, I feel a lot better!",
                                       "I hope no one saw me put that item back... hopefully, it was okay to use my saliva to stick it to the wall.",
                                       "If only I could talk to Pratik again... ever since he broke up with me for commiting manslaughter, life has not been the same."};
    public string[] dialogueCinema = { "WHEW! I can't believe the cinema door is locked! What am I supposed to do?", 
                                       "Well, I can just leave these tickets by the door, can't I? I'm sure whoever I took this from will come back to find it.", 
                                       "The ticket was for yesterday's screening though... maybe the owner can trade it in for a new showing?" };

    // Start is called before the first frame update
    void Start()
    {
        scannerRectTransform = scanner.rectTransform;
        areaRectTransform = area.rectTransform;

        scaleRectTransform = timerBar.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            QTE(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arrows(this.gameObject);
        }

        if (scannerOn)
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            float xPosition = currScannerPosition.x + (scannerSpeed * Time.deltaTime);
            scannerRectTransform.anchoredPosition = new Vector2(xPosition, currScannerPosition.y);
            if (xPosition >= 267)
            {
                isQTE = false;
                scannerOn = false;
                Debug.Log("Timeout...");
                QTEHolder.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && scannerOn)
        {
            Vector2 currScannerPosition = scannerRectTransform.anchoredPosition;
            Vector2 currAreaPosition = areaRectTransform.anchoredPosition;
            float areaWidthHalf = areaRectTransform.sizeDelta.x / 2;

            if (scannerOn && currScannerPosition.x >= currAreaPosition.x - areaWidthHalf && currScannerPosition.x <= currAreaPosition.x + areaWidthHalf)
            {
                isQTE = false;
                scannerOn = false;
                Debug.Log("Success!");
                SucceedGame();
                initiateDialogue();
                glowObject.SetActive(false);
                QTEHolder.SetActive(false);
            } 
            else if (scannerOn)
            {
                isQTE = false;
                scannerOn = false;
                FailGame();
                Debug.Log("Fail...");
                QTEHolder.SetActive(false);
            } 
        }

        if (arrowsOn)
        {
            Vector2 currScale = scaleRectTransform.localScale;
            if (currScale.x > 0)
            {
                scaleRectTransform.localScale = new Vector2(currScale.x - (timerSpeed * Time.deltaTime), currScale.y);
            }
            else
            {
                isQTE = false;
                arrowsOn = false;
                Debug.Log("Timout arrows...");
                FailGame();
                arrowHolder.SetActive(false);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (order[orderIndex].CompareTag("Up"))
                {
                    order[orderIndex].SetActive(false);
                    orderIndex++;

                    if (orderIndex >= order.Count)
                    {
                        isQTE = false;
                        arrowsOn = false;
                        Debug.Log("Won arrows");
                        SucceedGame();
                        initiateDialogue();
                        glowObject.SetActive(false);
                        arrowHolder.SetActive(false);
                    }
                } 
                else
                {
                    isQTE = false;
                    arrowsOn = false;
                    Debug.Log("Failed arrows");
                    FailGame();
                    arrowHolder.SetActive(false);
                }    
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (order[orderIndex].CompareTag("Down"))
                {
                    order[orderIndex].SetActive(false);
                    orderIndex++;

                    if (orderIndex >= order.Count)
                    {
                        isQTE = false;
                        arrowsOn = false;
                        Debug.Log("Won arrows");
                        SucceedGame();
                        initiateDialogue();
                        glowObject.SetActive(false);
                        arrowHolder.SetActive(false);
                    }
                }
                else
                {
                    isQTE = false;
                    arrowsOn = false;
                    Debug.Log("Failed arrows");
                    FailGame();
                    arrowHolder.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (order[orderIndex].CompareTag("Left"))
                {
                    order[orderIndex].SetActive(false);
                    orderIndex++;

                    if (orderIndex >= order.Count)
                    {
                        isQTE = false;
                        arrowsOn = false;
                        Debug.Log("Won arrows");
                        SucceedGame();
                        initiateDialogue();
                        glowObject.SetActive(false);
                        arrowHolder.SetActive(false);
                    }
                }
                else
                {
                    isQTE = false;
                    arrowsOn = false;
                    Debug.Log("Failed arrows");
                    FailGame();
                    arrowHolder.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (order[orderIndex].CompareTag("Right"))
                {
                    order[orderIndex].SetActive(false);
                    orderIndex++;

                    if (orderIndex >= order.Count)
                    {
                        isQTE = false;
                        arrowsOn = false;
                        Debug.Log("Won arrows");
                        SucceedGame();
                        initiateDialogue();
                        glowObject.SetActive(false);
                        arrowHolder.SetActive(false);
                    }
                }
                else
                {
                    isQTE = false;
                    arrowsOn = false;
                    Debug.Log("Failed arrows");
                    FailGame();
                    arrowHolder.SetActive(false);
                }
            }
        }
    }

    public void QTE(GameObject inObject)
    {
        isQTE = true;
        glowObject = inObject;

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

    public void arrows(GameObject inObject)
    {
        isQTE = true;
        glowObject = inObject;

        arrowHolder.SetActive(true);
        arrowsOn = true;
        orderIndex = 0;
        order.Clear();

        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            // Get the child at index i
            Transform child = parentTransform.GetChild(i);

            // Unparent the child (this automatically removes it from the parentRectTransform)
            child.SetParent(null);

            // Destroy the child object
            Destroy(child.gameObject);
        }

        // reset the bar to full
        Vector2 currentScale = timerBar.GetComponent<RectTransform>().localScale;
        timerBar.GetComponent<RectTransform>().localScale = new Vector2(10, currentScale.y);

        switch (difficulty)
        {
            case 1:
                numberOfImages = 3;
                timerSpeed = 3;
                break;
            case 2:
                numberOfImages = 5;
                timerSpeed = 4;
                break;
            case 3:
                numberOfImages = 7;
                timerSpeed = 5;
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

    private void initiateDialogue()
    {
        if (glowObject.CompareTag("QTE"))
        {
            dialogueScript.startDialogue(dialoguePoster);
        }
        else if (glowObject.CompareTag("DDR"))
        {
            dialogueScript.startDialogue(dialogueVending);
        }
        else if (glowObject.CompareTag("Cinema"))
        {
            dialogueScript.startDialogue(dialogueCinema);
        }
    }
}
