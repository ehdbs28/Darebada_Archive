[System.Serializable]
public class FishingData : SaveData
{
    public float ChargingSpeed = 5f;
    public float MaxChargingPower = 10f;

    public float RotationSpeed = 360f;

    public float ThrowingSpeed = 3f;

    public float StringLength = 10f;

    public float StringStrength = 1f;

    public override void Reset(){}
}