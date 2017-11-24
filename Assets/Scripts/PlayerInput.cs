using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class engineInput
{
    public KeyCode Left;
    public KeyCode Right;
}

public class PlayerInput : MonoBehaviour {
	public List<Player> localPlayer;
    public List<engineInput> playerInput;

	// Use this for initialization
	void Start () {
        int i = 0;
        Player[] ps = GameObject.FindObjectsOfType<Player> ();
		foreach(Player p in ps)
		{
			if (p.pInfo.index == Player.localIndex)
				localPlayer.Add (p);
            localPlayer[i].Linput = playerInput[i].Left;
            localPlayer[i].Rinput = playerInput[i].Right;

            i++;
        }

        
	}
	
	// Update is called once per frame
	//void Update () {
	//	inputA = Input.GetKey (KeyCode.A);
	//	inputD = Input.GetKey (KeyCode.D);
	//	// 2인 플레이가 아닌 P2P 대전이 될때는 아래 스크립트를 지워도 됨
	//	inputArrow_Left = Input.GetKey (KeyCode.LeftArrow);
	//	inputArrow_Right = Input.GetKey (KeyCode.RightArrow);
	//}
	//private void FixedUpdate ()
	//{
	//	if (localPlayer.Count < 1) // Local Player가 1명 이상일 때만 인풋을 받음
	//		return;
	//	if (inputA) {
            
	//	}
	//	if (inputD) {

 //       }
	//	// 2인 플레이가 아닌 P2P 대전이 될때는 아래 스크립트를 지워도 됨
	//	if (localPlayer.Count < 2) // Local Player가 2명 이상일 때만 인풋을 받음
	//		return;
	//	if (inputArrow_Left) {

 //       }
	//	if (inputArrow_Right) {

	//	}
	//}
}
