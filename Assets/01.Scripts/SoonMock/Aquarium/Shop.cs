using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Facility
{
    public int employee = 0;
    public int level = 1;
    public int sales
    {
        get {
            return (employee * level * 50)* level * 400;
        }
        private set { 
        }
    }
    public void AddEmployee()
    {
        if(employee < level * 3)
        {
            GameManager.Instance.GetManager<AquariumNumericalManager>().employeeCnt++;
            employee ++;
        }
    }
    public void SubEmployee()
    {

        if (employee > 0)
        {
            GameManager.Instance.GetManager<AquariumNumericalManager>().employeeCnt--;
            employee--;
        }
    }
    public void LevelUp()
    {
        level++;
    }
    public override Facility OnTouched()
    {
        return this;
    }
}
