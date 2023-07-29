
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public PlayerTankView playerTankView;
    public PlayerTankScriptableObject playerTankScriptableObject;
    [SerializeField]
    private BulletView bulletPrefab;

    public List<EnemyTankView> EnemyTanks;
    public PlayerTankView PlayerTank { get;private set; }

    void Start()
    {
        SpawnPlayerTank();
    }

   

    private async void SpawnPlayerTank()
    {
        PlayerTankModel model = new(playerTankScriptableObject);
        PlayerTankController controller = new(model, playerTankView,transform.position);
        PlayerTank = controller.TankView;
        await Task.Delay(500);
        EventService.Instance.PlayerTankSpawned?.Invoke();
    }

}
