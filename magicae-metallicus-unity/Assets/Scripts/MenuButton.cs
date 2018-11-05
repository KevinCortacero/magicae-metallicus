using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public void START_GAME()
    {
        Application.LoadLevel("arena");
    }

    public void QUIT_GAME()
    {
        Application.Quit();
    }
}
