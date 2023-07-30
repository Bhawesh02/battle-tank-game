
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyTankController 
{
    public EnemyTankModel tankModel;
    public EnemyTankView tankView;
    private CancellationTokenSource cancellationTokenSource;
    public EnemyTankController(EnemyTankModel tankModel, EnemyTankView tankView)
    {
        this.tankModel = tankModel;
        this.tankView = tankView;
        cancellationTokenSource = new CancellationTokenSource();
    }

    public void TakeDamage(int power)
    {
        tankModel.Health -= power;
        if(tankModel.Health<=0)
        {
            TankDestroy();
        }
    }

    public async void ChangeStateBasedOnPlayer()
    {
        try
        {
            if (tankView.PlayerTank == null)
                return;
            float distanceToPlayer = Vector3.Distance(tankView.transform.position, tankView.PlayerTank.transform.position);
            if (distanceToPlayer > tankModel.FightRadius && distanceToPlayer <= tankModel.ChaseRadius && tankView.currentState != tankView.chaseState)
            {
                tankView.ChangeState(tankView.chaseState);
            }
            else if (distanceToPlayer <= tankModel.FightRadius && tankView.currentState != tankView.fightState)
            { 
                tankView.ChangeState(tankView.fightState); 
            }            
            else if (distanceToPlayer > tankModel.ChaseRadius && tankView.currentState != tankView.petrolState)
            {
                EventService.Instance.OnPlayerEscapeFromChasingTank?.Invoke();
                tankView.ChangeState(tankView.petrolState);
            }
            await Task.Delay(100,cancellationTokenSource.Token);
            ChangeStateBasedOnPlayer();
        }
        catch (TaskCanceledException) 
        {
            
        }
    }


    public void TankDestroy()
    {
        AsyncCleanup();
        DestoryEverything.Instance.DestroyGameObject(tankView.gameObject);
    }

    public void AsyncCleanup()
    {
        tankView.currentState.OnStateExit();
        cancellationTokenSource.Cancel();
    }

    public Quaternion RotateTank(Vector3 targetPos)
    {
        Vector3 direction = targetPos - tankView.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        tankView.transform.rotation = Quaternion.Lerp(tankView.transform.rotation, targetRotation, Time.deltaTime * tankModel.RotationSpeed);
        return targetRotation;
    }

    public void MoveTank(Vector3 targetPos)
    {
        tankView.transform.position = Vector3.MoveTowards(tankView.transform.position, targetPos, tankModel.MovementSpeed * Time.deltaTime);
    }

    
}
