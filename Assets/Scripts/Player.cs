using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
	public int index = 0;
	public float hp = 100;

	public bool isDead = false;
}
[System.Serializable]
public class EngineInfo {
	public float power = 100.0f;
	public Transform engine1;
	public Transform engine2;
}
public class Player : MonoBehaviour {
	public static int localIndex = 0;
	public PlayerInfo pInfo;
	public EngineInfo eInfo;
	private Rigidbody2D rb;

	public event NoParameter OnDamaged;
	public event NoParameter OnDead;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}


	public void OnCollisionEnter2D (Collision2D col)
	{
		Damaged ();
	}

	public void OnCollisionStay2D (Collision2D col)
	{
		Damaged ();
	}

	public static void SetLocalIndex (int index)
	{
		localIndex = index;
	}

	public void Damaged ()
	{
		float damage = Mathf.Max(1f, rb.mass * rb.velocity.magnitude);
		pInfo.hp = Mathf.Max (0f, pInfo.hp - damage);
			
		if (null != OnDamaged)
			OnDamaged.Invoke();
		
		if (pInfo.hp <= 0) {
			pInfo.isDead = true;
			if (null != OnDead)
				OnDead.Invoke ();
		}
	}

	public void UseLeftEngine ()
	{
		rb.AddForceAtPosition(eInfo.power * eInfo.engine1.up, eInfo.engine1.position);
	}
	public void UseRightEngine ()
	{
		rb.AddForceAtPosition(eInfo.power * eInfo.engine2.up, eInfo.engine2.position);
	}
}
