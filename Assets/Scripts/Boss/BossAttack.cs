using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject targetObject;
    public LayerMask bombLayer;
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 경우
        if (other.gameObject == targetObject)
        {
            // PlayerHealth 컴포넌트를 가져옴
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // PlayerHealth가 null이 아니고, 면역 상태가 아닌 경우만 피해 처리
            if (playerHealth != null && !playerHealth.IsImmune)
            {
                playerHealth.TakeDamage(1); // 체력 1 감소
                playerHealth.ActivateImmunity(5f); // 5초간 면역 활성화
            }
        }

        if(other.gameObject.layer == bombLayer) 
        {
            // Bomb 스크립트 가져오기
            Bomb bomb = other.GetComponent<Bomb>();
            bomb.Explosion();
        }
    }
}
