using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    public GameObject credits;
    public GameObject help;
    public Image soundImage;

    public void start(){
	AudioListener.volume = 1;
    }

    public void Update(){
	Image image = soundImage.GetComponent<Image>();
	if (AudioListener.volume == 0){
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
	}
	else{
	    image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
	}
    }	

    public void START_GAME() {
        SceneManager.LoadScene("arena");
    }

    public void QUIT_GAME() {
        Application.Quit();
    }

    public void SHOW_CREDITS() {
        credits.SetActive(!credits.active);
	help.SetActive(false);
    }

    public void SHOW_HELP() {
        help.SetActive(!help.active);
	credits.SetActive(false);
    }

    public void SOUND() {
        AudioListener.volume = 1 - AudioListener.volume;
    }
}
