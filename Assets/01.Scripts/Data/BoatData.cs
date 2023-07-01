[System.Serializable]
public class BoatData : SaveData
{
    //public BoatStatSO BoatStat = null;
    public float Fuel = 0;

    public override void Reset()
    {
        //BoatStat = null;
        Fuel = 0;
    }
}
