
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
        EventService.Instance.PlayerTankSpawned += SetPlayerTank;
    }

    public void SetPlayerTank()
    {
        PlayerTank = TankService.Instance.PlayerTank;
    }
    private void Update()
    {
        TankController.ChangeStateBasedOnPlayer();
    }
    public void ChangeState(EnemyTankState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = state;
        currentState.OnStateEnter();
    }

    public void TakeDamage(BulletModel bulletModel)
    {
        TankController.TakeDamage(bulletModel.Power);
    }
    void OnDrawGizmos()
    {
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }

        if (TankController ==  null)
        {
            return;
        }
        Debug.Log("Draw");
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, TankController.tankModel.FightRadius);
    }
}
