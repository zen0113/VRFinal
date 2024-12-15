using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // �̵��� �� �̸�
    public GameObject targetObject;


    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ���� ���
        if (other.gameObject == targetObject)
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
