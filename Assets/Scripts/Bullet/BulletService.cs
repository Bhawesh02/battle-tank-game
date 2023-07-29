
using System;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService> 
{
    public BulletView BulletPrefab;
    public BulletModel BulletModel;

    [SerializeField]
    private BulletScriptableObjectList bulletScriptableObjectList;


    
    private BulletPoolService bulletPool;
    private void Start()
    {
        bulletPool = new();
    }

    public void GenerateBullet(Vector3 pos,Quaternion rotation)
    {
        BulletModel = new(bulletScriptableObjectList.bullets[0]);
        BulletController bulletController = bulletPool.GetBullet();
        //BulletController bulletController = BulletPoolService.Instance.GetBullet();
        bulletController.SetPosition(pos);
        bulletController.SetRotation(rotation);
        bulletController.BulletView.gameObject.SetActive(true);
    }

    public void DeleteBullet(BulletController bulletController)
    {
        bulletPool.ReturnItem(bulletController);
    }
}
