using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserCollapse : MonoBehaviour
{
    public Transform cube; // 리셋할 큐브 오브젝트
    private Vector3 initialPosition; // 큐브의 초기 위치
    private Quaternion initialRotation; // 큐브의 초기 회전
    public LayerMask resetLayer; // 충돌을 확인할 레이어
    private XRGrabInteractable grabInteractable; // Grab Interactable 컴포넌트

    void Start()
    {
        // 초기 위치와 회전값 저장
        initialPosition = cube.position;
        initialRotation = cube.rotation;

        // Grab Interactable 컴포넌트 가져오기
        grabInteractable = cube.GetComponent<XRGrabInteractable>();
    }

    // 충돌했을 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 레이어가 resetLayer에 포함되어 있는지 확인
        if ((resetLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            // Grab 해제
            if (grabInteractable != null && grabInteractable.isSelected)
            {
                grabInteractable.interactionManager.SelectExit(grabInteractable.selectingInteractor, grabInteractable);
            }

            // 큐브의 위치와 회전 초기화
            cube.position = initialPosition;
            cube.rotation = initialRotation;

            // 물리적 안정성을 위한 속도 초기화
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // 속도 초기화
                rb.angularVelocity = Vector3.zero; // 각속도 초기화
            }
        }
    }
}
