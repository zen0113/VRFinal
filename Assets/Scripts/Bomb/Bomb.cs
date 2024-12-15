using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bomb : MonoBehaviour
{

    public LayerMask targetLayer; // Ÿ�� ���̾� ����
    public UnityEvent OnExplosion; // ���� �� �̺�Ʈ
    public GameObject hitEffectPrefab; // ��ƼŬ ����Ʈ ������


    // ��ź�� ������ ����
    public void Throw()
    {
        // XRGrabInteractable�� ����� ��ź�� ���� �� ��ȣ�ۿ� ����
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);
        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 200, 500));
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� ���̾ Ÿ�� ���̾�� ��ġ�ϴ��� Ȯ��
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            // ���̾ ��ġ�� ��� ���� ����
            var boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(1); // Boss ü�� ����
                // Debug.Log($"Boss ü���� ���ҵǾ����ϴ�. ���� ü��: {boss.currentHP}");
            }

            // ��ƼŬ ����Ʈ ����
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity); // ���� ��ġ�� ����Ʈ ����
            } 

            Explosion(); // ���� ó��
        }
    }

    public void Explosion()
    {
        OnExplosion?.Invoke(); // ���� �� �̺�Ʈ ����
        Destroy(this.gameObject); // ��ź ������Ʈ ����
    }

    private void Start()
    {

    }
}
