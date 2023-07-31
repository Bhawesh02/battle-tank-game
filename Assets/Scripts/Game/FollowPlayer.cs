
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private bool playerInScene;

    private PlayerTankView playerTank;

    private Vector3 playerOffset;

    private Vector3 playerPos;

    private void Awake()
    {
        playerInScene = false;
    }

    private void Start()
    {
        EventService.Instance.PlayerTankSpawned += FollowPlayer_PlayerTankSpawned;
        EventService.Instance.OnPlayerDead += FollowPlayer_OnPlayerDead;
    }
    private void FollowPlayer_OnPlayerDead()
    {
        playerInScene = false;
    }
    private void FollowPlayer_PlayerTankSpawned()
    {
        playerInScene = true;
        playerTank = TankService.Instance.PlayerTank;
        playerPos = playerTank.transform.position;
        playerOffset = playerPos - transform.position;
    }

    void LateUpdate()
    {
        if (!playerInScene) { return; }
        playerPos = playerTank.transform.position;
        transform.position = playerPos - playerOffset;
    }
}
