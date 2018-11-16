using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    public GameObject credits;
    public GameObject help;

    public void START_GAME() {
        SceneManager.LoadScene("arena");
    }

    public void QUIT_GAME() {
        Application.Quit();
    }

    public void SHOW_CREDITS() {
        credits.SetActive(!credits.active);
    }

    public void SHOW_HELP() {
        help.SetActive(!help.active);
    }
}
