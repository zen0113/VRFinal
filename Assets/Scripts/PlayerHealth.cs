using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;   // 초기 체력
    public int currentHP = 10;     // 현재 체력
    public RectTransform hpBar;
    [SerializeField] private float maxStretchHeight = 100f; // UI가 최대 늘어날 높이
    [SerializeField] private float minStretchHeight = 10f;  // UI가 최소 높이
    public bool IsImmune { get; private set; } = false; // 면역 상태 플래그
    public UnityEvent OnHit;
    public UnityEvent PlayerDead;

    // 체력을 감소시키는 함수
    public void TakeDamage(int damage)
    {
        OnHit?.Invoke();
        currentHP -= damage; // 체력 감소
        Debug.Log("플레이어 체력: " + currentHP);

        UpdateHPBar(); // 체력 UI 업데이트

        // 체력이 0 이하가 되면 사망 처리
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

    // 면역 상태 활성화
    public void ActivateImmunity(float duration)
    {
        if (!IsImmune) // 이미 면역 상태가 아니면
        {
            IsImmune = true; // 면역 상태 설정
            Debug.Log(duration + "초간 면역");
            Invoke(nameof(DeactivateImmunity), duration); // duration 후에 면역 비활성화
        }
    }

    // 면역 상태 비활성화
    private void DeactivateImmunity()
    {
        IsImmune = false;
        Debug.Log("면역 상태 해제");
    }

    // 플레이어 사망 처리
    private void Die()
    {
        PlayerDead?.Invoke();
    }
}
