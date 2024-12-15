using System.Collections;
using UnityEngine;

public class DoorMng : MonoBehaviour
{
    public Transform doorLeft; // 왼쪽으로 이동할 오브젝트
    public Transform doorRight; // 오른쪽으로 이동할 오브젝트
    public float moveDistance = 3f; // 이동할 거리
    public float moveSpeed = 1f; // 이동 속도 (초당 이동할 비율)

    private Vector3 doorLeftStartPos; // 문 왼쪽 시작 위치
    private Vector3 doorRightStartPos; // 문 오른쪽 시작 위치
    private Vector3 doorLeftTargetPos; // 문 왼쪽 목표 위치
    private Vector3 doorRightTargetPos; // 문 오른쪽 목표 위치
    private bool isOpening = false; // 문이 열리고 있는지 확인하는 변수
    private bool isClosing = false; // 문이 닫히고 있는지 확인하는 변수

    void Start()
    {
        doorLeftStartPos = doorLeft.position; // 초기 위치 저장
        doorRightStartPos = doorRight.position; // 초기 위치 저장

        // 문 방향 수정: 왼쪽 문은 왼쪽으로, 오른쪽 문은 오른쪽으로 이동
        doorLeftTargetPos = doorLeftStartPos + Vector3.left * moveDistance;
        doorRightTargetPos = doorRightStartPos + Vector3.right * moveDistance;
    }

    void Update()
    {
        if (isOpening)
        {
            MoveDoors(doorLeftTargetPos, doorRightTargetPos); // 문 열기
        }

        if (isClosing)
        {
            MoveDoors(doorLeftStartPos, doorRightStartPos); // 문 닫기
        }
    }

    public void OpenDoor()
    {
        if (!isOpening && !isClosing)
        {
            isOpening = true; // 문 열기 시작
        }
    }

    public void CloseDoor()
    {
        if (!isOpening && !isClosing)
        {
            isClosing = true; // 문 닫기 시작
        }
    }

    private void MoveDoors(Vector3 targetLeft, Vector3 targetRight)
    {
        // 왼쪽 문이 목표 위치에 도달할 때까지 이동
        if (Vector3.Distance(doorLeft.position, targetLeft) > 0.01f)
        {
            doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetLeft, moveSpeed * Time.deltaTime);
        }

        // 오른쪽 문이 목표 위치에 도달할 때까지 이동
        if (Vector3.Distance(doorRight.position, targetRight) > 0.01f)
        {
            doorRight.position = Vector3.MoveTowards(doorRight.position, targetRight, moveSpeed * Time.deltaTime);
        }

        // 목표 지점에 도달했으면 이동 멈추기
        if (Vector3.Distance(doorLeft.position, targetLeft) <= 0.01f && Vector3.Distance(doorRight.position, targetRight) <= 0.01f)
        {
            isOpening = false;
            isClosing = false;
        }
    }
}
