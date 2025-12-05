using UnityEngine;

public class CapacityManager : MonoBehaviour
{
    [Header("Capacity Settings")]
    public float maxCapacity = 6f;
    public float decreaseRate = 0.5f;

    private float currentCapacity = 0f;

    void Update()
    {
        DecreaseCapacity();
    }

    void DecreaseCapacity()
    {
        if (currentCapacity > 0f)
        {
            currentCapacity -= decreaseRate * Time.deltaTime;
            currentCapacity = Mathf.Max(0f, currentCapacity);

            UIManager.Instance.UpdateCapacityBar(currentCapacity, maxCapacity);
        }
    }

    public void AddToCapacity(int amount)
    {
        currentCapacity += amount;
        currentCapacity = Mathf.Min(currentCapacity, maxCapacity);

        UIManager.Instance.UpdateCapacityBar(currentCapacity, maxCapacity);
    }

    public float GetCurrentCapacity()
    {
        return currentCapacity;
    }

    public float GetMaxCapacity()
    {
        return maxCapacity;
    }
}
