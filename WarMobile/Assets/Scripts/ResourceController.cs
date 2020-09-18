using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private Slider resourceSlider;
    [SerializeField] private Text resourceNumText;
    [SerializeField] private int startingResourceValue = 5;
    private SpawnClick[] spawnClicks;
    private TacticCard[] tacticCards;
    private int currentResourceValue;

    private void Start()
    {
        Initialize();

        // Subscribe to events.
        spawnClicks = FindObjectsOfType<SpawnClick>();
        for (int i = 0; i < spawnClicks.Length; i++)
        {
            spawnClicks[i].onTroopPurchased += SubtractResource;
        }

        tacticCards = FindObjectsOfType<TacticCard>();
        for (int i = 0; i < tacticCards.Length; i++)
        {
            tacticCards[i].onTacticPurchased += SubtractResource;
        }
    }

    /// <summary>
    /// Initializes UI and current resource.
    /// </summary>
    private void Initialize()
    {
        resourceNumText.text = "0";
        resourceSlider.value = startingResourceValue;
        currentResourceValue = startingResourceValue;
        UpdateUI();
    }

    private void Update()
    {
        IncrementResourceByTime();
    }

    /// <summary>
    /// Slowly increments the resource value over time.
    /// </summary>
    private void IncrementResourceByTime()
    {
        resourceSlider.value += Time.deltaTime * .333333f;
        if (resourceSlider.value - currentResourceValue >= 1)
        {
            currentResourceValue++;
            UpdateUI();
        }
    }

    /// <summary>
    /// Subtracts from current resource.
    /// </summary>
    /// <param name="amount">The amount to subtract.</param>
    private void SubtractResource(int amount)
    {
        resourceSlider.value -= amount;
        currentResourceValue -= amount;
        UpdateUI();
    }

    /// <summary>
    /// Updates the UI.
    /// </summary>
    private void UpdateUI()
    {
        resourceNumText.text = currentResourceValue.ToString();
    }

    /// <summary>
    /// Gets the current resources.
    /// </summary>
    /// <returns>The current resource value.</returns>
    public int GetResources()
    {
        return currentResourceValue;
    }
}
