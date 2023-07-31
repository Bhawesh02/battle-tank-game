
using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    public BulletView BulletPrefab;
    public BulletModel BulletModel;

    [SerializeField]
    private BulletScriptableObjectList bulletScriptableObjectList;

    public List<BulletView> bullets;


    private BulletPoolService bulletPool;
    private void Start()
    {
        bulletPool = BulletPoolService.Instance;
        EventService.Instance.NewBulletCreated += BulletService_NewBulletCreated;
    }

    private void BulletService_NewBulletCreated(BulletController bulletController)
    {
        bullets.Add(bulletController.BulletView);
    }

    public void GenerateBullet(Vector3 pos, Quaternion rotation, IBulletFirer bulletFirer)
    {
        BulletModel = new(bulletScriptableObjectList.bullets[0]);
        BulletController bulletController = bulletPool.GetBullet();
        //BulletController bulletController = BulletPoolService.Instance.GetBullet();
        bulletController.SetPosition(pos);
        bulletController.SetRotation(rotation);
        bulletController.SetBulletFirer(bulletFirer);
        bulletController.BulletView.gameObject.SetActive(true);
    }

    public void DeleteBullet(BulletController bulletController)
    {
        bulletPool.ReturnItem(bulletController);
    }
}
