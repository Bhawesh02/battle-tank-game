
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DestoryEverything : MonoSingletonGeneric<DestoryEverything>
{
    public PlayerTankView PlayerTank;
    public List<EnemyTankView> EnemyTanks;
    public GameObject[] EnviromentItems;
    public ParticleSystem TankExplosion;
    public int timeForTankExplosion = 2000;

    private CancellationTokenSource cancellationTokenSource;

    private void Start()
    {
        EventService.Instance.OnPlayerDead += DestroyEverythingInGame;
        cancellationTokenSource = new CancellationTokenSource();
    }

    public async void DestroyEverythingInGame()
    {
        try
        {
            await Task.Delay(500);
            DestroyGameObject(PlayerTank.gameObject);
            await Task.Delay(2000);

            foreach (EnemyTankView EnemyTank in EnemyTanks)
            {
                EnemyTank.TankController.TankDestroy();
            }
            foreach (BulletView BuletView in BulletService.Instance.bullets)
            {
                Destroy(BuletView.gameObject);
                await Task.Delay(500, cancellationTokenSource.Token);
            }
            foreach (GameObject item in EnviromentItems)
            {
                Destroy(item);
                await Task.Delay(1000, cancellationTokenSource.Token);
            }
        }
        catch 
        {
            //Cancel all Destroy
        }
    }

    public async void DestroyGameObject(GameObject gameObject)
    {
        try {
            Vector3 explosionPos = gameObject.transform.position;
            Destroy(gameObject);
            ParticleSystem explosion = Instantiate(TankExplosion, explosionPos, TankExplosion.transform.rotation);
            explosion.Play();
            await Task.Delay(timeForTankExplosion, cancellationTokenSource.Token);
            Destroy(explosion.gameObject);
        }
        catch 
        {
            //Cancel Destroy
        }
    }


#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
    private static void OnLoad()
    {
        UnityEditor.EditorApplication.playModeStateChanged +=
            (change) =>
            {
                if (
                    change == UnityEditor.PlayModeStateChange.ExitingPlayMode ||
                    change == UnityEditor.PlayModeStateChange.ExitingEditMode
                )
                {
                    DestoryEverything.Instance.cancellationTokenSource.Cancel();
                }
            };
    }
#endif
}
