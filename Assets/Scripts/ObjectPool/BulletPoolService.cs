
public class BulletPoolService : SingletonGeneric<BulletPoolService>
{
    private readonly BulletPool bulletPool = new();

    public BulletController GetBullet()
    {
        return bulletPool.GetItem();
    }

    public void ReturnItem(BulletController bulletController)
    {
        bulletPool.ReturnItem(bulletController);
    }
}

public class BulletPool : PoolService<BulletController>
{
    protected override BulletController CreateItem()
    {
        BulletController bulletControler = new(BulletService.Instance.BulletModel, BulletService.Instance.BulletPrefab);
        EventService.Instance.NewBulletCreated?.Invoke(bulletControler);
        return bulletControler;
    }
    public override void ReturnItem(BulletController bulletController)
    {
        base.ReturnItem(bulletController);
        bulletController.BulletView.gameObject.SetActive(false);
    }
} 
