using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public CanvasGroup canvasGroup;
    public string[] lines;
    public float textSpeed; // lower the faster

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        textComponent.text = string.Empty;
        // startDialogue(new string[] { "What the fuck! Pratik! How dos this look", "whatttt" });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.State == GameManager.GameState.Dialogue)
        {
            if (textComponent.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void startDialogue(string[] inLines)
    {
        textComponent.text = string.Empty;
        lines = inLines;
        canvasGroup.alpha = 1;
        GameManager.Instance.UpdateGameState(GameManager.GameState.Dialogue);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            GameManager.Instance.returns += 1;
            if (GameManager.Instance.returns == 6)
            {
                Debug.Log("FINISHED GAME");
            }
            canvasGroup.alpha = 0;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
        }
    }
}
