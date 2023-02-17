using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject spawnPointVisibility;

    private void Awake()
    {
        spawnPointVisibility.SetActive(false);
    }
}
