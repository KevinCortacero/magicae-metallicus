using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {


    public Player player1;
    public Player player2;
    public GameObject gameOverWindow;
    public Text text;
    private bool isOver;

	// Use this for initialization
	void Start () {
        this.gameOverWindow.gameObject.SetActive(false);
        this.isOver = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.isOver == false && this.player1 == null && this.player2 == null)
        {
            this.text.text = "EQUALITY !";
            this.gameOverWindow.gameObject.SetActive(true);
            this.isOver = true;
        }

        else if (this.isOver == false && this.player1 == null)
        {
            this.text.text = "PLAYER 2 WON !";
            this.gameOverWindow.gameObject.SetActive(true);
            Destroy(this.player2);
            this.isOver = true;
        }

        else if (this.isOver == false && this.player2 == null)
        {
            this.text.text = "PLAYER 1 WON !";
            this.gameOverWindow.gameObject.SetActive(true);
            Destroy(this.player1);
            this.isOver = true;
        }
    }

    public void AGAIN(){
	SceneManager.LoadScene("arena");
    }

    public void MENU(){
	SceneManager.LoadScene("Menu");
    }
}
