using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleyerGroundCheck : MonoBehaviour
{
    [SerializeField] GamePlayerController gamePlayerController;

    //private void Awake()
    //{
    //    gamePlayerController = GetComponent<GamePlayerController>();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gamePlayerController.gameObject)
            return;
        gamePlayerController.SetIsGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gamePlayerController.gameObject)
            return;
        gamePlayerController.SetIsGrounded(false);
    }

}
