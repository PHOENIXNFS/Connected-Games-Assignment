using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager gameMenuManagerInstance;
    
    [SerializeField] Menu[] menus;

    private void Awake()
    {
        gameMenuManagerInstance = this;
    }

    public void Open(string menuname)
    {
        for(int i=0; i<menus.Length; i++)
        {
            if(menus[i].menuName == menuname)
            {
                //Open(menus[i]);
                menus[i].OpenMenuObject();
            }
            else if(menus[i].bIsThisMenuOpen)
            {
                Close(menus[i]);
            }
        }
    }

    public void Open(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(menus[i].bIsThisMenuOpen)
            {
                Close(menus[i]);
            }
        }
        menu.OpenMenuObject();
    }

    public void Close(Menu menu)
    {
        menu.CloseMenuObject();
    }
}
