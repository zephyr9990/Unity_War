using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    private SpawnClick[] spawnClicks;

    private void Start()
    {
        // Subscribe to events.
        spawnClicks = FindObjectsOfType<SpawnClick>();
        for (int i = 0; i < spawnClicks.Length; i++)
        {
            spawnClicks[i].onTroopSpawn += SpawnTroop;
        }
    }

    /// <summary>
    /// Spawns troops.
    /// </summary>
    /// <param name="troopToSpawn">The troop to spawn.</param>
    /// <param name="amount">The amount to spawn.</param>
    private void SpawnTroop(GameObject troopToSpawn, int amount)
    {
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(troopToSpawn, spawnLocations[spawnIndex].position, spawnLocations[spawnIndex].rotation);
        }
    }
}
