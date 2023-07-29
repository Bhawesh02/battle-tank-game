
using System;

public class EventService : SingletonGeneric<EventService>
{

    public Action OnPlayerBulletFire;
    public Action OnPlayerDead;
    public Action OnPlayerEscapeFromChasingTank;
    public Action<BulletController> NewBulletCreated;
    public Action PlayerTankSpawned;
}
