using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    // UI 텍스트 변경
    public Text targetText; 
    public string changetext = ""; // 변경할 텍스트 내용

    void Start()
    {
        // Start the coroutine to change the text after 5 seconds
        StartCoroutine(ChangeTextAfterDelay());
    }

    IEnumerator ChangeTextAfterDelay()
    {
        yield return new WaitForSeconds(7f);

        targetText.text = changetext;

        yield return new WaitForSeconds(7f);

        // Clear the text
        targetText.text = "";
    }
}
