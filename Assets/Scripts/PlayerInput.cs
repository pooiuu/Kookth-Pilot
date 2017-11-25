using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	public List<Player> localPlayer;
	public bool inputA;
	public bool inputD;
	// 2인 플레이가 아닌 P2P 대전이 될때는 아래 스크립트를 지워도 됨
	public bool inputArrow_Left;
	public bool inputArrow_Right;

	private Player lPlayer01;
	private Player lPlayer02;

	// Use this for initialization
	void Start () {
		Player[] ps = GameObject.FindObjectsOfType<Player> ();
		foreach(Player p in ps)
		{
			if (p.pInfo.index == Player.localIndex)
				localPlayer.Add (p);
		}
		lPlayer01 = localPlayer [0];
		lPlayer02 = localPlayer [1];
	}

	// Update is called once per frame
	void Update () {
		inputA = Input.GetKey (KeyCode.A);
		inputD = Input.GetKey (KeyCode.D);
		// 2인 플레이가 아닌 P2P 대전이 될때는 아래 스크립트를 지워도 됨
		inputArrow_Left = Input.GetKey (KeyCode.LeftArrow);
		inputArrow_Right = Input.GetKey (KeyCode.RightArrow);
	}
	private void FixedUpdate ()
	{
		if (localPlayer.Count < 1) // Local Player가 1명 이상일 때만 인풋을 받음
			return;
		// Player01의 Engine 사용 Check
		ProcessPlayerAction (lPlayer01, inputA, inputD);
		
		// 2인 플레이가 아닌 P2P 대전이 될때는 아래 스크립트를 지워도 됨
		if (localPlayer.Count < 2) // Local Player가 2명 이상일 때만 인풋을 받음
			return;
		// Player02의 Engine 사용 Check
		ProcessPlayerAction (lPlayer02, inputArrow_Left, inputArrow_Right);
	}
	public void ProcessPlayerAction (Player p, bool lInput, bool rInput)
	{
		if (lInput)
			p.UseEngine (EngineDirection.Left);
		else
			p.UnUseEngine (EngineDirection.Left);
		if (rInput)
			p.UseEngine (EngineDirection.Right);
		else
			p.UnUseEngine (EngineDirection.Right);
	}
}