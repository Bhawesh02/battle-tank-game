
using System.Threading;
using System.Threading.Tasks;

public class EnemyTankFightState : EnemyTankState, IBulletFirer
{
    private PlayerTankView playerTank;
    private readonly int fireRate = 2;

    private static CancellationTokenSource cancellationTokenSource;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        playerTank = tankView.PlayerTank;
        cancellationTokenSource = new CancellationTokenSource();
        FireBullet();
    }
    private void Update()
    {
        tankController.RotateTank(playerTank.transform.position);
        
    }

    private async void FireBullet()
    {
        try {
            //Debug.Log("Bullet Fired");
            BulletService.Instance.GenerateBullet(tankView.BulletShooter.transform.position, tankView.transform.rotation,this);
            int waitTime = 1000 / fireRate;
            await Task.Delay(waitTime, cancellationTokenSource.Token);
            FireBullet();
        }
        catch(TaskCanceledException) { }
        
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        cancellationTokenSource?.Cancel();
    }

}
