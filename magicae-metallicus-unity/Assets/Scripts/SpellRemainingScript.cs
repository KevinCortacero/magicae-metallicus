using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRemainingScript : MonoBehaviour {

    public Player player;
    public Text spell1Remaining;
    public Text spell2Remaining;
    public Text spell3Remaining;
    public Text spell4Remaining;
    public Slider cursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.spell1Remaining.text = "x" + this.player.GetSpellRemaining(1);
        this.spell2Remaining.text = "x" + this.player.GetSpellRemaining(2);
        this.spell3Remaining.text = "x" + this.player.GetSpellRemaining(3);
        this.spell4Remaining.text = "x" + this.player.GetSpellRemaining(4);

        this.cursor.value = this.player.CURSOR;
    }
}
