using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image hpBar;
    public Player player;
    float max_HP;
	// Use this for initialization
	void Start () {
        hpBar.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = player.pInfo.hp/100;
        hpBar.color = new Color(1,0.01f * player.pInfo.hp,0);
        Debug.Log(0.01f * player.pInfo.hp);
	}
}
