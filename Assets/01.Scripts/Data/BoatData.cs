[System.Serializable]
public class BoatData : SaveData
{
    public float BoatMaxSpeed = 5f; 
    public float BoatForwardAcceleration = 10f;
    public float BoatBackwardAcceleration = 3f;
    public float BoatDeceleration = 5f;

    public float BoatMaxRotationSpeed = 1f;
    public float BoatRotationAcceleration = 10f;
    public float BoatRotationDeceleration = 10f;

    public float MaxFuel = 100f;
    public float MaxDurablity = 100f;
    public float CurrentDurablity = 0f;

    public override void Reset(){}
}

