
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyTankView : MonoBehaviour, ITakeDamage
{
    public EnemyTankIdleState idleState;
    public EnemyTankChaseState chaseState;
    public EnemyTankFightState fightState;
    public EnemyTankPetrolState petrolState;

    public EnemyTankState startState;
    public EnemyTankState currentState;

    
    public PlayerTankView PlayerTank;

    public List<GameObject> PetrolPoints;

    public EnemyTankController TankController { get; private set; }
    public EnemyTankScriptableObject EnemyTankScriptableObject;

    public GameObject BulletShooter;


    private void Start()
    {
        TankService.Instance.EnemyTanks.Add(this);
        EnemyTankModel model = new(EnemyTankScriptableObject);
        TankController = new(model, this);
        DestoryEverything.Instance.EnemyTanks.Add(this);
        ChangeState(startState);
        TankController.ChangeToPetrolState();
        EventService.Instance.PlayerTankSpawned += SetPlayerTank;
        EventService.Instance.PlayerTankSpawned += TankController.ChangeStateBasedOnPlayer;
        EventService.Instance.OnPlayerDead += EnemyTankView_OnPlayerDead;
    }

    public void SetPlayerTank()
    {
        PlayerTank = TankService.Instance.PlayerTank;
    }
    public void ChangeState(EnemyTankState state)
    {
        currentState?.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void EnemyTankView_OnPlayerDead()
    {
        ChangeState(idleState);
    }

    public void TakeDamage(BulletModel bulletModel)
    {
        TankController.TakeDamage(bulletModel.Power);
    }


    private void OnDestroy()
    {
        currentState.OnStateExit();
        EventService.Instance.PlayerTankSpawned -= SetPlayerTank;
        EventService.Instance.PlayerTankSpawned -= TankController.ChangeStateBasedOnPlayer;
        EventService.Instance.OnPlayerDead -= EnemyTankView_OnPlayerDead;
    }
}
