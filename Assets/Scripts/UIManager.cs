using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Slider capacityBar;
    public TextMeshProUGUI missedCountText;

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

    void Start()
    {
        if (capacityBar != null)
        {
            capacityBar.minValue = 0f;
            capacityBar.maxValue = 6f;
            capacityBar.value = 0f;
        }

        UpdateMissedCount(0);
    }

    public void UpdateCapacityBar(float currentCapacity, float maxCapacity)
    {
        if (capacityBar != null)
        {
            capacityBar.maxValue = maxCapacity;
            capacityBar.value = currentCapacity;
        }
    }

    public void UpdateMissedCount(int count)
    {
        if (missedCountText != null)
        {
            missedCountText.text = "Missed: " + count;
        }
    }
}
