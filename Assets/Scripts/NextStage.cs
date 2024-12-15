using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // 이동할 씬 이름
    public GameObject targetObject;


    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 경우
        if (other.gameObject == targetObject)
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
