using System;
using System.Threading;
using System.Threading.Tasks;

public class EnemyTankIdleState : EnemyTankState
{
    private CancellationTokenSource cancellationTokenSource;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        cancellationTokenSource = new CancellationTokenSource();
        ChangeToPetrolState();
    }

    private async void ChangeToPetrolState()
    {
        try
        {
            await Task.Delay(2000, cancellationTokenSource.Token);
            tankView.ChangeState(tankView.petrolState);
        }
        catch (OperationCanceledException)
        {
            //To stop Error in Inspector
        }
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        cancellationTokenSource.Cancel();

    }
}
