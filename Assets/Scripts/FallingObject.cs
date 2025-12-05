using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public enum ObjectType
    {
        Small,
        Mid,
        Large
    }

    [Header("Object Properties")]
    public ObjectType type;
    public float fallSpeed = 5f;

    private const int SMALL_PENALTY = 1;
    private const int MID_PENALTY = 2;
    private const int LARGE_PENALTY = 3;
    private const int PLATFORM_INCREASE = 1;

    private bool hasBeenCaught = false;

    public int GetGlobalPenalty()
    {
        switch (type)
        {
            case ObjectType.Small:
                return SMALL_PENALTY;
            case ObjectType.Mid:
                return MID_PENALTY;
            case ObjectType.Large:
                return LARGE_PENALTY;
            default:
                return SMALL_PENALTY;
        }
    }

    public int GetPlatformIncrease()
    {
        return PLATFORM_INCREASE;
    }

    public bool IsCaught()
    {
        return hasBeenCaught;
    }

    public void MarkAsCaught()
    {
        hasBeenCaught = true;
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
