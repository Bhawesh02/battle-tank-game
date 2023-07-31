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
        
    }

    
    public override void OnStateExit()
    {
        base.OnStateExit();
        cancellationTokenSource.Cancel();

    }
}
