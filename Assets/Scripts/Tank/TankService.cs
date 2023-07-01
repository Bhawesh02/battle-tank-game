using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankView tankView;
    // Start is called before the first frame update
    void Start()
    {
        TankModel model = new(20,10);
        TankController controller = new(model, tankView);
    }

    
}