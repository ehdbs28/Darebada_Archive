[System.Serializable]
public class PlayerData : SaveData
{
    public float MaxSpeed = 10;
    public float Acceleration = 5;
    public float Deceleration = 5;

    public override void Reset(){}
}
