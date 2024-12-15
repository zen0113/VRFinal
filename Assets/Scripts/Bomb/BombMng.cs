using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    // ���� ��ź ����
    public GameObject bombPrefab; // ������ Bomb ������
    public Vector2 spawnRangeX; // X�� ���� (min, max)
    public Vector2 spawnRangeZ; // Z�� ���� (min, max)
    public float spawnInterval = 3f; // ���� ���� (��)

    private void Start()
    {
        // Bomb ���� �ݺ� ����
        StartCoroutine(SpawnBombs());
    }

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            // ���� ��ġ ���
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
            Vector3 spawnPosition = new Vector3(randomX, -4, randomZ); // y���� ���� ���̷� ����

            // Bomb ������ ����
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

            // ���� �������� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
