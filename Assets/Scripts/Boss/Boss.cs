using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public int maxHP = 10;   // �ʱ� ü��
    public int currentHP = 10;     // ���� ü��
    public Camera targetCamera; // ��鸱 ī�޶� ����
    public UnityEvent OnHit;
    public UnityEvent BossDead;
    public RectTransform hpBar;
    [SerializeField] private float shakeDuration = 0.5f; // ��鸲 ���� �ð�
    [SerializeField] private float shakeRoughness = 5f;  // ��鸲 ��ĥ��
    [SerializeField] private float shakeMagnitude = 0.2f; // ��鸲 ũ��
    [SerializeField] private float maxStretchHeight = 100f; // UI�� �ִ� �þ ����
    [SerializeField] private float minStretchHeight = 10f;  // UI�� �ּ� ����


    // �������� ���� �� ȣ��
    public void TakeDamage(int damage)
    {
        OnHit?.Invoke(); // �������� �Ծ��� ���� �̺�Ʈ ����
        currentHP -= damage; // ���� ü�� ����
        Debug.Log($"Boss ü���� {damage}��ŭ �����߽��ϴ�. ���� ü��: {currentHP}");

        UpdateHPBar(); // ü�� UI ������Ʈ

        // ī�޶� ��鸲 ȿ�� ����
        if (targetCamera != null)
        {
            StartCoroutine(ShakeCamera(targetCamera, shakeDuration, shakeRoughness, shakeMagnitude));
        }

        // ü���� 0 ������ ��� Boss ��� ó��
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

    private void Die()
    {
        Debug.Log("Boss�� ����߽��ϴ�.");
        BossDead?.Invoke();
    }

    // ī�޶� ��鸲 ȿ��
    private IEnumerator ShakeCamera(Camera camera, float duration, float roughness, float magnitude)
    {
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);
        Vector3 originalPosition = camera.transform.position; // ī�޶��� ���� ��ġ ����

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            tick += Time.deltaTime * roughness;

            // ���� ������ ���� ���� ��ġ���� ��鸲 �߰�
            camera.transform.position = originalPosition + new Vector3(
                (Mathf.PerlinNoise(tick, 0) - 0.5f) * magnitude,
                (Mathf.PerlinNoise(0, tick) - 0.5f) * magnitude,
                0);

            yield return null;
        }

        camera.transform.position = originalPosition; // �������� ���� ��ġ�� ����
    }

}
