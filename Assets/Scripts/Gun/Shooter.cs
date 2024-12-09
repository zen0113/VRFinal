using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask; // 충돌 가능 레이어
    public GameObject portalPrefab_1; // 생성할 포탈 프리팹
    public GameObject portalPrefab_2; // 생성할 포탈 프리팹
    public Transform shootPoint; // 레이저 시작 지점
    public float shootDelay = 0.1f; // 레이저 발사 간격
    public float maxDistance = 100f; // 최대 거리

    private GameObject portalA; // 첫 번째 포탈
    private GameObject portalB; // 두 번째 포탈
    private bool isFirstPortal = true; // 첫 번째 포탈을 쏘는지 여부

    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var wait = new WaitForSeconds(shootDelay);

        while (true)
        {
            Shoot();
            yield return wait;
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            CreatePortal(hitInfo.point, hitInfo.normal);
        }
        else
        {
            Vector3 hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            CreatePortal(hitPoint, Vector3.up); // 기본 방향으로 포탈 생성
        }
    }

    private void CreatePortal(Vector3 position, Vector3 normal)
    {
        if (isFirstPortal)
        {
            // 기존 포탈이 있다면 모두 제거
            if (portalA != null) Destroy(portalA);
            if (portalB != null) Destroy(portalB);

            // 첫 번째 포탈 생성
            portalA = Instantiate(portalPrefab_1, position, Quaternion.LookRotation(normal));
            isFirstPortal = false;
        }
        else
        {
            // 두 번째 포탈 생성
            if (portalB != null) Destroy(portalB);
            portalB = Instantiate(portalPrefab_2, position, Quaternion.LookRotation(normal));
            isFirstPortal = true; // 다음 발사 시 첫 번째 포탈을 쏘도록 설정
        }
    }

    public Vector3 GetPortalExit(Vector3 currentPosition)
    {
        // 현재 위치가 첫 번째 포탈 근처라면 두 번째 포탈 위치 반환
        if (portalA != null && Vector3.Distance(currentPosition, portalA.transform.position) < 1f && portalB != null)
        {
            return portalB.transform.position + portalB.transform.forward;
        }
        // 현재 위치가 두 번째 포탈 근처라면 첫 번째 포탈 위치 반환
        else if (portalB != null && Vector3.Distance(currentPosition, portalB.transform.position) < 1f && portalA != null)
        {
            return portalA.transform.position + portalA.transform.forward;
        }

        return currentPosition; // 포탈 근처가 아니면 현재 위치 반환
    }
}
