using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankView : MonoBehaviour
{
    public PlayerTankController tankController { get;private set; }

    private Rigidbody rb;

    public GameObject BulletShooter;

    public PlayerTankModel tankModel;

    public void SetTankController(PlayerTankController _tankController)
    {
        tankController = _tankController;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        tankController.SetRigidBody(rb);
        tankModel = tankController.tankModel;

    }
    void Update()
    {
        Movement();
        if(Input.GetMouseButtonDown(0))
        {
            tankController.FireBullet(BulletShooter.transform.position);
        }
    }

    private void Movement()
    {
        float move = Input.GetAxis("Vertical1");
        if (move != 0)
            tankController.MoveTank(move);
        float rotation = Input.GetAxisRaw("Horizontal1");
        if (rotation != 0)
            tankController.RotateTank(rotation);
    }
}