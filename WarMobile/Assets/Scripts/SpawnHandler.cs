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
        spawnClicks = FindObjectsOfType<SpawnClick>();
        for (int i = 0; i < spawnClicks.Length; i++)
        {
            spawnClicks[i].onTroopSpawn += SpawnTroop;
        }
    }

    private void SpawnTroop(GameObject troopToSpawn, int amount)
    {
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(troopToSpawn, spawnLocations[spawnIndex].position, spawnLocations[spawnIndex].rotation);
        }
    }
}
