using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;   // �ʱ� ü��
    public int currentHP = 10;     // ���� ü��
    public RectTransform hpBar;
    [SerializeField] private float maxStretchHeight = 100f; // UI�� �ִ� �þ ����
    [SerializeField] private float minStretchHeight = 10f;  // UI�� �ּ� ����
    public bool IsImmune { get; private set; } = false; // �鿪 ���� �÷���
    public UnityEvent OnHit;
    public UnityEvent PlayerDead;

    // ü���� ���ҽ�Ű�� �Լ�
    public void TakeDamage(int damage)
    {
        OnHit?.Invoke();
        currentHP -= damage; // ü�� ����
        Debug.Log("�÷��̾� ü��: " + currentHP);

        UpdateHPBar(); // ü�� UI ������Ʈ

        // ü���� 0 ���ϰ� �Ǹ� ��� ó��
        if (currentHP <= 0)
        {
            Die();
        }
    }

    // ü�� UI ������Ʈ
    private void UpdateHPBar()
    {
        if (hpBar == null) return;

        // ü�� ���� ���
        float hpRatio = Mathf.Clamp01((float)currentHP / maxHP);

        // Bottom �� ���� (Stretch ȿ��)
        float newBottom = Mathf.Lerp(minStretchHeight, maxStretchHeight, hpRatio);
        hpBar.offsetMin = new Vector2(hpBar.offsetMin.x, newBottom);
    }

    // �鿪 ���� Ȱ��ȭ
    public void ActivateImmunity(float duration)
    {
        if (!IsImmune) // �̹� �鿪 ���°� �ƴϸ�
        {
            IsImmune = true; // �鿪 ���� ����
            Debug.Log(duration + "�ʰ� �鿪");
            Invoke(nameof(DeactivateImmunity), duration); // duration �Ŀ� �鿪 ��Ȱ��ȭ
        }
    }

    // �鿪 ���� ��Ȱ��ȭ
    private void DeactivateImmunity()
    {
        IsImmune = false;
        Debug.Log("�鿪 ���� ����");
    }

    // �÷��̾� ��� ó��
    private void Die()
    {
        PlayerDead?.Invoke();
    }
}
