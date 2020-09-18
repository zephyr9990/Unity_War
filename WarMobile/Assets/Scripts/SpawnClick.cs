using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnClick : MonoBehaviour // Should be renamed to Troop Card. Did not have time to manually re-enter values after rename.
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] Text resourceNumText;
    [SerializeField] private int amountToSpawn = 3;
    [SerializeField] private int resourceCost = 1;
    [SerializeField] private bool isTroop = true;

    public delegate void OnTroopSpawn(GameObject TroopToSpawn, int amount);
    public OnTroopSpawn onTroopSpawn;
    public delegate void OnTroopPurchased(int resourceCost);
    public OnTroopPurchased onTroopPurchased;

    private ResourceController resourceController;


    private void Start()
    {
        resourceNumText.text = resourceCost.ToString();
        resourceController = FindObjectOfType<ResourceController>();

        if (isTroop)
            gameObject.GetComponent<Button>().onClick.AddListener(SpawnTroop);
    }

    /// <summary>
    /// Checks to see if the player has enough resources. If they do, troops are spawned.
    /// </summary>
    private void SpawnTroop()
    {
        if (PlayerHasEnoughResources())
        {
            onTroopPurchased?.Invoke(resourceCost);
            onTroopSpawn?.Invoke(spawnObject, amountToSpawn);
        }
    }

    /// <summary>
    /// Checks to see if the player has enough resources to spawn troops.
    /// </summary>
    /// <returns>Returns true if they do, false if not.</returns>
    private bool PlayerHasEnoughResources()
    {
        int currentResources = resourceController.GetResources();
        return (currentResources - resourceCost) >= 0;
    }
}
