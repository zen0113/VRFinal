using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // ��ƼŬ ���� �ð� ������ ����
    public GameObject particlePrefab; // ������ ��ƼŬ ������
    // Start is called before the first frame update
    void Start()
    {
       Destroy(particlePrefab, 1.5f); // 1.5�� �ڿ� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
