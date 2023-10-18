using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealthManager : BasicHealthManager
{
    public override void Death()
    { 
        base.Death();
        GetComponent<ExplosiveBarrel>().Explode();
        this.enabled = false;
    }
}
