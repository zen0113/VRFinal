using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMng : MonoBehaviour
{
    // ���� ���� ���� ����
    public GameObject object1; // ���� ������
    public GameObject object2; // ������ ������

    [SerializeField] private Renderer targetRenderer_1; // ���� ���� Renderer
    [SerializeField] private Renderer targetRenderer_2; // ������ ���� Renderer
    [SerializeField] private Material newMaterial; // �� ��Ƽ����
    [SerializeField] private Material originalMaterial; // ���� ���͸���

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

            // ù ��° ������Ʈ Ȱ��ȭ
            targetRenderer_1.material = originalMaterial;
            object1.SetActive(true);
            object2.SetActive(false);
            yield return new WaitForSeconds(5f);

            // �� �� ��Ȱ��ȭ (��� ����)
            object1.SetActive(false);
            yield return new WaitForSeconds(15f);

            targetRenderer_2.material = newMaterial;
            yield return new WaitForSeconds(5f);

            // �� ��° ������Ʈ Ȱ��ȭ
            targetRenderer_2.material = originalMaterial;
            object1.SetActive(false);
            object2.SetActive(true);
            yield return new WaitForSeconds(5f);

            // �� �� ��Ȱ��ȭ (��� ����)
            object2.SetActive(false);
            yield return new WaitForSeconds(15f); // 10�� �ֱ⿡�� 4�� �Ҹ� �� ������ 6��

            targetRenderer_1.material = newMaterial;
            targetRenderer_2.material = newMaterial;
            yield return new WaitForSeconds(5f);

            // �� �� Ȱ��ȭ
            targetRenderer_1.material = originalMaterial;
            targetRenderer_2.material = originalMaterial;
            object1.SetActive(true);
            object2.SetActive(true);
            yield return new WaitForSeconds(7f); // �� �� ���� ���� ����
        }
    }
}
