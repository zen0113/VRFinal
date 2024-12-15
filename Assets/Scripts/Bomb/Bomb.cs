using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bomb : MonoBehaviour
{

    public LayerMask targetLayer; // 타겟 레이어 설정
    public UnityEvent OnExplosion; // 폭발 시 이벤트
    public GameObject hitEffectPrefab; // 파티클 이펙트 프리팹


    // 폭탄을 던지는 로직
    public void Throw()
    {
        // XRGrabInteractable를 사용해 폭탄을 던질 때 상호작용 해제
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);
        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 200, 500));
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 레이어가 타겟 레이어와 일치하는지 확인
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            // 레이어가 일치할 경우 로직 실행
            var boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(1); // Boss 체력 감소
                // Debug.Log($"Boss 체력이 감소되었습니다. 남은 체력: {boss.currentHP}");
            }

            // 파티클 이펙트 생성
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity); // 폭발 위치에 이펙트 생성
            } 

            Explosion(); // 폭발 처리
        }
    }

    public void Explosion()
    {
        OnExplosion?.Invoke(); // 폭발 시 이벤트 실행
        Destroy(this.gameObject); // 폭탄 오브젝트 삭제
    }

    private void Start()
    {

    }
}
