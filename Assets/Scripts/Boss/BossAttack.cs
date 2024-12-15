using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject targetObject;
    public LayerMask bombLayer;
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ���� ���
        if (other.gameObject == targetObject)
        {
            // PlayerHealth ������Ʈ�� ������
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // PlayerHealth�� null�� �ƴϰ�, �鿪 ���°� �ƴ� ��츸 ���� ó��
            if (playerHealth != null && !playerHealth.IsImmune)
            {
                playerHealth.TakeDamage(1); // ü�� 1 ����
                playerHealth.ActivateImmunity(5f); // 5�ʰ� �鿪 Ȱ��ȭ
            }
        }

        if(other.gameObject.layer == bombLayer) 
        {
            // Bomb ��ũ��Ʈ ��������
            Bomb bomb = other.GetComponent<Bomb>();
            bomb.Explosion();
        }
    }
}
