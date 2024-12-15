using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    // UI �ؽ�Ʈ ����
    public Text targetText; 
    public string changetext = ""; // ������ �ؽ�Ʈ ����

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
