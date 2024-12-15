using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventBridge : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // �̵��� �� �̸�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Call()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
