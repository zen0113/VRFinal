using System.Collections;
using UnityEngine;

public class DoorMng : MonoBehaviour
{
    public Transform doorLeft; // �������� �̵��� ������Ʈ
    public Transform doorRight; // ���������� �̵��� ������Ʈ
    public float moveDistance = 3f; // �̵��� �Ÿ�
    public float moveSpeed = 1f; // �̵� �ӵ� (�ʴ� �̵��� ����)

    private Vector3 doorLeftStartPos; // �� ���� ���� ��ġ
    private Vector3 doorRightStartPos; // �� ������ ���� ��ġ
    private Vector3 doorLeftTargetPos; // �� ���� ��ǥ ��ġ
    private Vector3 doorRightTargetPos; // �� ������ ��ǥ ��ġ
    private bool isOpening = false; // ���� ������ �ִ��� Ȯ���ϴ� ����
    private bool isClosing = false; // ���� ������ �ִ��� Ȯ���ϴ� ����

    void Start()
    {
        doorLeftStartPos = doorLeft.position; // �ʱ� ��ġ ����
        doorRightStartPos = doorRight.position; // �ʱ� ��ġ ����

        // �� ���� ����: ���� ���� ��������, ������ ���� ���������� �̵�
        doorLeftTargetPos = doorLeftStartPos + Vector3.left * moveDistance;
        doorRightTargetPos = doorRightStartPos + Vector3.right * moveDistance;
    }

    void Update()
    {
        if (isOpening)
        {
            MoveDoors(doorLeftTargetPos, doorRightTargetPos); // �� ����
        }

        if (isClosing)
        {
            MoveDoors(doorLeftStartPos, doorRightStartPos); // �� �ݱ�
        }
    }

    public void OpenDoor()
    {
        if (!isOpening && !isClosing)
        {
            isOpening = true; // �� ���� ����
        }
    }

    public void CloseDoor()
    {
        if (!isOpening && !isClosing)
        {
            isClosing = true; // �� �ݱ� ����
        }
    }

    private void MoveDoors(Vector3 targetLeft, Vector3 targetRight)
    {
        // ���� ���� ��ǥ ��ġ�� ������ ������ �̵�
        if (Vector3.Distance(doorLeft.position, targetLeft) > 0.01f)
        {
            doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetLeft, moveSpeed * Time.deltaTime);
        }

        // ������ ���� ��ǥ ��ġ�� ������ ������ �̵�
        if (Vector3.Distance(doorRight.position, targetRight) > 0.01f)
        {
            doorRight.position = Vector3.MoveTowards(doorRight.position, targetRight, moveSpeed * Time.deltaTime);
        }

        // ��ǥ ������ ���������� �̵� ���߱�
        if (Vector3.Distance(doorLeft.position, targetLeft) <= 0.01f && Vector3.Distance(doorRight.position, targetRight) <= 0.01f)
        {
            isOpening = false;
            isClosing = false;
        }
    }
}
