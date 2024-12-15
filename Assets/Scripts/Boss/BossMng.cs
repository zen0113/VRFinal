using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMng : MonoBehaviour
{
    // 보스 공격 패턴 관리
    public GameObject object1; // 왼쪽 레이저
    public GameObject object2; // 오른쪽 레이저

    [SerializeField] private Renderer targetRenderer_1; // 왼쪽 선반 Renderer
    [SerializeField] private Renderer targetRenderer_2; // 오른쪽 선반 Renderer
    [SerializeField] private Material newMaterial; // 새 머티리얼
    [SerializeField] private Material originalMaterial; // 기존 머터리얼

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ToggleObjectsCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ToggleObjectsCycle()
    {
        while (true)
        {
            object1.SetActive(false);
            object2.SetActive(false);
            yield return new WaitForSeconds(15f);

            targetRenderer_1.material = newMaterial;
            yield return new WaitForSeconds(5f);

            // 첫 번째 오브젝트 활성화
            targetRenderer_1.material = originalMaterial;
            object1.SetActive(true);
            object2.SetActive(false);
            yield return new WaitForSeconds(5f);

            // 둘 다 비활성화 (대기 상태)
            object1.SetActive(false);
            yield return new WaitForSeconds(15f);

            targetRenderer_2.material = newMaterial;
            yield return new WaitForSeconds(5f);

            // 두 번째 오브젝트 활성화
            targetRenderer_2.material = originalMaterial;
            object1.SetActive(false);
            object2.SetActive(true);
            yield return new WaitForSeconds(5f);

            // 둘 다 비활성화 (대기 상태)
            object2.SetActive(false);
            yield return new WaitForSeconds(15f); // 10초 주기에서 4초 소모 후 나머지 6초

            targetRenderer_1.material = newMaterial;
            targetRenderer_2.material = newMaterial;
            yield return new WaitForSeconds(5f);

            // 둘 다 활성화
            targetRenderer_1.material = originalMaterial;
            targetRenderer_2.material = originalMaterial;
            object1.SetActive(true);
            object2.SetActive(true);
            yield return new WaitForSeconds(7f); // 둘 다 켜진 상태 유지
        }
    }
}
