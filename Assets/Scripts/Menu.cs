using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool bIsThisMenuOpen;

    public void OpenMenuObject()
    {
        bIsThisMenuOpen = true;
        gameObject.SetActive(true);
    }

    public void CloseMenuObject()
    {
        bIsThisMenuOpen = false;
        gameObject.SetActive(false);
    }
}
