
using Newtonsoft.Json.Bson;
using System.Security.Cryptography;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public Rigidbody Rb;


    private BulletController bulletController;
    
    public BulletModel BulletModel { get;private set; }


    public void SetBullerControler(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }

    

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        BulletModel = bulletController.BulletModel;
    }

    // Update is called once per frame
    void Update()
    {
        bulletController.MoveForword();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IBulletFirer>() == BulletModel.BulletFirer)
        {
            return;
        }
        if (other.GetComponent<BulletView>()!=null)
        {
            //Debug.Log("Collided with other bullet");
            return;
        }
        ITakeDamage ob = other.gameObject.GetComponent<ITakeDamage>();
        ob?.TakeDamage(BulletModel);
        BulletService.Instance.DeleteBullet(bulletController);
    }

}
