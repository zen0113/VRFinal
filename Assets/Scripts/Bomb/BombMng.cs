using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    // 랜덤 폭탄 스폰
    public GameObject bombPrefab; // 생성할 Bomb 프리팹
    public Vector2 spawnRangeX; // X축 범위 (min, max)
    public Vector2 spawnRangeZ; // Z축 범위 (min, max)
    public float spawnInterval = 3f; // 생성 간격 (초)

    private void Start()
    {
        // Bomb 생성 반복 실행
        StartCoroutine(SpawnBombs());
    }

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            // 랜덤 위치 계산
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
            Vector3 spawnPosition = new Vector3(randomX, -4, randomZ); // y값은 지면 높이로 설정

            // Bomb 프리팹 생성
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

            // 다음 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
