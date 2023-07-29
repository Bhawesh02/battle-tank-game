
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private bool playerSpawned;

    private PlayerTankView playerTank;

    private Vector3 playerOffset;

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
        Vector3 playerPos = playerTank.transform.position;
        playerOffset = playerPos - transform.position;
    }

    void LateUpdate()
    {
        if (!playerSpawned) { return; }
        Vector3 playerPos = playerTank.transform.position;
        transform.position = playerPos - playerOffset;
    }
}
