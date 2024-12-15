using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // ��Ż��
    public Transform player; // �̵��� �÷��̾�
    public Shooter shooter; // Shooter Ŭ���� ����
    private bool canTeleport = true; // �ڷ���Ʈ ���� ����
    public float teleportCooldown = 5f; // �ڷ���Ʈ ��ٿ� �ð� (��)

    void Update()
    {
        if (!canTeleport) return; // �ڷ���Ʈ ��ٿ� ���̸� �������� ����

        // �÷��̾� ��ġ�� ������Ʈ�ϸ� ��Ż �̵� ���� ����
        Vector3 currentPosition = player.position;

        // �̵��� ��ġ�� ���
        Vector3 newPosition = shooter.GetPortalExit(currentPosition);

        // ��ġ�� �ٲ���ٸ� �÷��̾� �̵� ó��
        if (newPosition != currentPosition)
        {
            player.position = newPosition;
            StartCoroutine(TeleportCooldown()); // ��ٿ� ����
        }
    }

    // ��Ż�� Ÿ�� �ִ� ���� ������ ��Ÿ�� �߰�
    IEnumerator TeleportCooldown()
    {
        canTeleport = false; // �ڷ���Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(teleportCooldown); // ��ٿ� �ð� ���
        canTeleport = true; // �ڷ���Ʈ Ȱ��ȭ
    }
}
