using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TankService : MonoSingletonGeneric<TankService>
{
    public PlayerTankView playerTankView;
    public PlayerTankScriptableObject playerTankScriptableObject;
    [SerializeField]
    private BulletView bulletPrefab;
    [SerializeField]
    private EnemyTankView enemyTank;
    public PlayerTankView PlayerTank { get;private set; }

    void Start()
    {
        SpawnPlayerTank();
    }

   

    private void SpawnPlayerTank()
    {
        PlayerTankModel model = new(playerTankScriptableObject);
        PlayerTankController controller = new(model, playerTankView,transform.position);
        PlayerTank = controller.TankView;
        enemyTank.SetPlayerTank();
    }

}
