using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // 파티클 일정 시간 지나면 삭제
    public GameObject particlePrefab; // 생성할 파티클 프리팹
    // Start is called before the first frame update
    void Start()
    {
       Destroy(particlePrefab, 1.5f); // 1.5초 뒤에 삭제
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
