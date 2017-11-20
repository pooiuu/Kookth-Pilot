using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoalChecker : MonoBehaviour {
	public int goalCount = 0;
	public event PlayerProcess OnGoal;

	void Start ()
	{
		OnGoal += new PlayerProcess (DescribeGoalPlayer);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag("Player")) 
		{
			var p = col.GetComponent<Player> ();
			if (p.pInfo.isGoal)
				return;
			p.pInfo.isGoal = true;
			goalCount++;
			if (null != OnGoal)
				OnGoal.Invoke (p);
		}
	}
	void DescribeGoalPlayer (Player p)
	{
		// 골 정보를 알리는 패킷을 송수신하는데도 사용 가능 합니다.
		Debug.Log ("Player : " + p.name + ", ID : " + p.pInfo.index + " Goal");
	}
}
