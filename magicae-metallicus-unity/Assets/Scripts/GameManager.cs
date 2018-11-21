using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player1HUD;
    public GameObject player2HUD;

    public static GameManager instance = null;

    // Use this for initialization
    void Start() {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetPlayer1(Player player) {
        this.player1HUD.GetComponentInChildren<HUDPlayerScript>().player = player;
        this.player1HUD.GetComponentInChildren<SpellRemainingScript>().player = player;
    }

    public void SetPlayer2(Player player) {
        this.player2HUD.GetComponentInChildren<HUDPlayerScript>().player = player;
        this.player2HUD.GetComponentInChildren<SpellRemainingScript>().player = player;
    }
}
