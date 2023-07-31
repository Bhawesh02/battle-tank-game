
using System;
using UnityEngine;
[Serializable]
public class PlayerTankController 
{
    public PlayerTankModel TankModel { get; }
    public PlayerTankView TankView { get; }

    private Rigidbody tankRigidBoy;



    public PlayerTankController(PlayerTankModel _tankModel, PlayerTankView _tankView, Vector3 pos)
    {
        TankModel = _tankModel;
        TankView = GameObject.Instantiate<PlayerTankView>(_tankView,pos,_tankView.transform.rotation);
        TankView.SetTankController(this);
    }

    public void SetRigidBody(Rigidbody rb)
    {
        tankRigidBoy = rb;
    }

    public void MoveTank(float _move)
    {
        tankRigidBoy.velocity = _move * TankModel.MovementSpeed * Time.deltaTime * TankView.transform.forward;
    }

    public void RotateTank(float _rotation)
    {
        Vector3 rotate = new (0f,_rotation * TankModel.RotationSpeed,0f);
        Quaternion deltaRotaion = Quaternion.Euler(rotate * Time.deltaTime);
        tankRigidBoy.MoveRotation(tankRigidBoy.rotation * deltaRotaion);
    }

    public void TakeDamage(BulletModel bulletModel)
    {
        TankModel.Health -= bulletModel.Power;
        if (TankModel.Health <= 0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        EventService.Instance.OnPlayerDead?.Invoke();
    }
    public void FireBullet(Vector3 pos)
    {
        BulletService.Instance.GenerateBullet(pos,TankView.transform.rotation,TankView);
        EventService.Instance.OnPlayerBulletFire?.Invoke();
    }

}
