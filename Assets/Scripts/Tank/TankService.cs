
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public PlayerTankView playerTankView;
    public PlayerTankScriptableObject playerTankScriptableObject;
    [SerializeField]
    private BulletView bulletPrefab;

    public List<EnemyTankView> EnemyTanks;
    public PlayerTankView PlayerTank { get; private set; }

    private CancellationTokenSource cancellationTokenSource;

    void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        SpawnPlayerTank();
    }



    private async void SpawnPlayerTank()
    {
        try
        {
            PlayerTankModel model = new(playerTankScriptableObject);
            PlayerTankController controller = new(model, playerTankView, transform.position);
            PlayerTank = controller.TankView;
            await Task.Delay(500, cancellationTokenSource.Token);
            EventService.Instance.PlayerTankSpawned?.Invoke();
        }
        catch (TaskCanceledException)
        {

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
                    
                    Instance.cancellationTokenSource?.Cancel();
                }
            };
    }
#endif

}
