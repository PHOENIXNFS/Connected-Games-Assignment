using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GamePlayerManager : MonoBehaviour
{
    PhotonView PlayerPV;

    private void Awake()
    {
        PlayerPV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(PlayerPV.IsMine)
        {
            CreatePlayerController();
        }
    }

    void CreatePlayerController()
    {
        Debug.Log("Player Controller Initiated");
        PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Game Player Controller"), Vector3.zero, Quaternion.identity);
    }
}
