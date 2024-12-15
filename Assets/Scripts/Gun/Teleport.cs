using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // 포탈건
    public Transform player; // 이동할 플레이어
    public Shooter shooter; // Shooter 클래스 참조
    private bool canTeleport = true; // 텔레포트 가능 여부
    public float teleportCooldown = 5f; // 텔레포트 쿨다운 시간 (초)

    void Update()
    {
        if (!canTeleport) return; // 텔레포트 쿨다운 중이면 실행하지 않음

        // 플레이어 위치를 업데이트하며 포탈 이동 로직 실행
        Vector3 currentPosition = player.position;

        // 이동할 위치를 계산
        Vector3 newPosition = shooter.GetPortalExit(currentPosition);

        // 위치가 바뀌었다면 플레이어 이동 처리
        if (newPosition != currentPosition)
        {
            player.position = newPosition;
            StartCoroutine(TeleportCooldown()); // 쿨다운 시작
        }
    }

    // 포탈을 타고 있는 것을 생각해 쿨타운 추가
    IEnumerator TeleportCooldown()
    {
        canTeleport = false; // 텔레포트 비활성화
        yield return new WaitForSeconds(teleportCooldown); // 쿨다운 시간 대기
        canTeleport = true; // 텔레포트 활성화
    }
}
