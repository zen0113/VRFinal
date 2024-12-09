using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask; // �浹 ���� ���̾�
    public GameObject portalPrefab_1; // ������ ��Ż ������
    public GameObject portalPrefab_2; // ������ ��Ż ������
    public Transform shootPoint; // ������ ���� ����
    public float shootDelay = 0.1f; // ������ �߻� ����
    public float maxDistance = 100f; // �ִ� �Ÿ�

    private GameObject portalA; // ù ��° ��Ż
    private GameObject portalB; // �� ��° ��Ż
    private bool isFirstPortal = true; // ù ��° ��Ż�� ����� ����

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
            CreatePortal(hitPoint, Vector3.up); // �⺻ �������� ��Ż ����
        }
    }

    private void CreatePortal(Vector3 position, Vector3 normal)
    {
        if (isFirstPortal)
        {
            // ���� ��Ż�� �ִٸ� ��� ����
            if (portalA != null) Destroy(portalA);
            if (portalB != null) Destroy(portalB);

            // ù ��° ��Ż ����
            portalA = Instantiate(portalPrefab_1, position, Quaternion.LookRotation(normal));
            isFirstPortal = false;
        }
        else
        {
            // �� ��° ��Ż ����
            if (portalB != null) Destroy(portalB);
            portalB = Instantiate(portalPrefab_2, position, Quaternion.LookRotation(normal));
            isFirstPortal = true; // ���� �߻� �� ù ��° ��Ż�� ��� ����
        }
    }

    public Vector3 GetPortalExit(Vector3 currentPosition)
    {
        // ���� ��ġ�� ù ��° ��Ż ��ó��� �� ��° ��Ż ��ġ ��ȯ
        if (portalA != null && Vector3.Distance(currentPosition, portalA.transform.position) < 1f && portalB != null)
        {
            return portalB.transform.position + portalB.transform.forward;
        }
        // ���� ��ġ�� �� ��° ��Ż ��ó��� ù ��° ��Ż ��ġ ��ȯ
        else if (portalB != null && Vector3.Distance(currentPosition, portalB.transform.position) < 1f && portalA != null)
        {
            return portalA.transform.position + portalA.transform.forward;
        }

        return currentPosition; // ��Ż ��ó�� �ƴϸ� ���� ��ġ ��ȯ
    }
}
