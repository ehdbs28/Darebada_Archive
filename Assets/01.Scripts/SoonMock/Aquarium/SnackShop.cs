using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackShop : Facility
{
    public override Facility OnTouched()
    {
        return this;
    }
}
