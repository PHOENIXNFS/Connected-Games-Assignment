using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnManager : MonoBehaviour
{
    public static GameSpawnManager gameSpawnManagerInstance;

    SpawnPoint[] playerSpawnpoints;

    private void Awake()
    {
        gameSpawnManagerInstance = this;
        playerSpawnpoints = GetComponentsInChildren<SpawnPoint>();
    }

    public Transform GetSpawnPoint()
    {
        return playerSpawnpoints[Random.Range(0, playerSpawnpoints.Length)].transform;
    }


}
