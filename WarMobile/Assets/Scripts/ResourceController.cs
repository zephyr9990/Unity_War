using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private Slider resourceSlider;
    [SerializeField] private Text resourceNumText;
    private SpawnClick[] spawnClicks;
    private int currentResourceValue;

    private void Start()
    {
        resourceNumText.text = "0";
        resourceSlider.value = 0;
        currentResourceValue = 0;

        spawnClicks = FindObjectsOfType<SpawnClick>();
        for (int i = 0; i < spawnClicks.Length; i++)
        {
            spawnClicks[i].onTroopPurchased += SubtractResource;
        }
    }

    private void Update()
    {
        IncrementResourceByTime();
    }

    private void IncrementResourceByTime()
    {
        resourceSlider.value += Time.deltaTime * .333333f;
        if (resourceSlider.value - currentResourceValue > 1)
        {
            currentResourceValue++;
            UpdateUI();
        }
    }

    private void SubtractResource(int amount)
    {
        resourceSlider.value -= amount;
        currentResourceValue -= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        resourceNumText.text = currentResourceValue.ToString();
    }

    public int GetResources()
    {
        return currentResourceValue;
    }
}
