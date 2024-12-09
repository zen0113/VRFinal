using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserCollapse : MonoBehaviour
{
    public Transform cube; // ������ ť�� ������Ʈ
    private Vector3 initialPosition; // ť���� �ʱ� ��ġ
    private Quaternion initialRotation; // ť���� �ʱ� ȸ��
    public LayerMask resetLayer; // �浹�� Ȯ���� ���̾�
    private XRGrabInteractable grabInteractable; // Grab Interactable ������Ʈ

    void Start()
    {
        // �ʱ� ��ġ�� ȸ���� ����
        initialPosition = cube.position;
        initialRotation = cube.rotation;

        // Grab Interactable ������Ʈ ��������
        grabInteractable = cube.GetComponent<XRGrabInteractable>();
    }

    // �浹���� �� ȣ��Ǵ� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� ���̾ resetLayer�� ���ԵǾ� �ִ��� Ȯ��
        if ((resetLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            // Grab ����
            if (grabInteractable != null && grabInteractable.isSelected)
            {
                grabInteractable.interactionManager.SelectExit(grabInteractable.selectingInteractor, grabInteractable);
            }

            // ť���� ��ġ�� ȸ�� �ʱ�ȭ
            cube.position = initialPosition;
            cube.rotation = initialRotation;

            // ������ �������� ���� �ӵ� �ʱ�ȭ
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                rb.angularVelocity = Vector3.zero; // ���ӵ� �ʱ�ȭ
            }
        }
    }
}
