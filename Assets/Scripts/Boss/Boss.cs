using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public int maxHP = 10;   // 초기 체력
    public int currentHP = 10;     // 현재 체력
    public Camera targetCamera; // 흔들릴 카메라 지정
    public UnityEvent OnHit;
    public UnityEvent BossDead;
    public RectTransform hpBar;
    [SerializeField] private float shakeDuration = 0.5f; // 흔들림 지속 시간
    [SerializeField] private float shakeRoughness = 5f;  // 흔들림 거칠기
    [SerializeField] private float shakeMagnitude = 0.2f; // 흔들림 크기
    [SerializeField] private float maxStretchHeight = 100f; // UI가 최대 늘어날 높이
    [SerializeField] private float minStretchHeight = 10f;  // UI가 최소 높이


    // 데미지를 받을 때 호출
    public void TakeDamage(int damage)
    {
        OnHit?.Invoke(); // 데미지를 입었을 때의 이벤트 실행
        currentHP -= damage; // 현재 체력 감소
        Debug.Log($"Boss 체력이 {damage}만큼 감소했습니다. 남은 체력: {currentHP}");

        UpdateHPBar(); // 체력 UI 업데이트

        // 카메라 흔들림 효과 실행
        if (targetCamera != null)
        {
            StartCoroutine(ShakeCamera(targetCamera, shakeDuration, shakeRoughness, shakeMagnitude));
        }

        // 체력이 0 이하일 경우 Boss 사망 처리
        if (currentHP <= 0)
        {
            Die();
        }
    }

    // 체력 UI 업데이트
    private void UpdateHPBar()
    {
        if (hpBar == null) return;

        // 체력 비율 계산
        float hpRatio = Mathf.Clamp01((float)currentHP / maxHP);

        // Bottom 값 조정 (Stretch 효과)
        float newBottom = Mathf.Lerp(minStretchHeight, maxStretchHeight, hpRatio);
        hpBar.offsetMin = new Vector2(hpBar.offsetMin.x, newBottom);
    }

    private void Die()
    {
        Debug.Log("Boss가 사망했습니다.");
        BossDead?.Invoke();
    }

    // 카메라 흔들림 효과
    private IEnumerator ShakeCamera(Camera camera, float duration, float roughness, float magnitude)
    {
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);
        Vector3 originalPosition = camera.transform.position; // 카메라의 원래 위치 저장

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            tick += Time.deltaTime * roughness;

            // 현재 프레임 기준 원래 위치에서 흔들림 추가
            camera.transform.position = originalPosition + new Vector3(
                (Mathf.PerlinNoise(tick, 0) - 0.5f) * magnitude,
                (Mathf.PerlinNoise(0, tick) - 0.5f) * magnitude,
                0);

            yield return null;
        }

        camera.transform.position = originalPosition; // 마지막에 원래 위치로 복원
    }

}
