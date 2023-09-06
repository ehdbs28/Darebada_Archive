using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Facility
{
    public override Facility OnTouched()
    {
        return this;
    }

}
