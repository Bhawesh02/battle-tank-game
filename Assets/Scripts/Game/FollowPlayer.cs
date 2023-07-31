
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private bool playerSpawned;

    private PlayerTankView playerTank;

    private Vector3 playerOffset;

    private Vector3 playerPos;

    private void Awake()
    {
        playerSpawned = false;
    }

    private void Start()
    {
        EventService.Instance.PlayerTankSpawned += FollowPlayer_PlayerTankSpawned;
    }
    private void FollowPlayer_PlayerTankSpawned()
    {
        playerSpawned = true;
        playerTank = TankService.Instance.PlayerTank;
        playerPos = playerTank.transform.position;
        playerOffset = playerPos - transform.position;
    }

    void LateUpdate()
    {
        if (!playerSpawned) { return; }
        playerPos = playerTank.transform.position;
        transform.position = playerPos - playerOffset;
    }
}
