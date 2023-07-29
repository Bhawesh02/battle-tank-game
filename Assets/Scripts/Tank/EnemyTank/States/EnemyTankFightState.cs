
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

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
        Debug.Log("Fight");
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
        cancellationTokenSource.Cancel();
        Debug.Log("No more Fight!");
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
                    cancellationTokenSource.Cancel();
                }
            };
    }
#endif
}
