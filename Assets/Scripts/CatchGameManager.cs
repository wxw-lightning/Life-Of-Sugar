using UnityEngine;

public class CatchGameManager : MonoBehaviour
{
    public static CatchGameManager Instance { get; private set; }

    [Header("Game Stats")]
    public int missedObjectsCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseMissedCount(int amount)
    {
        missedObjectsCount += amount;
        UIManager.Instance.UpdateMissedCount(missedObjectsCount);
    }

    public int GetMissedCount()
    {
        return missedObjectsCount;
    }
}
