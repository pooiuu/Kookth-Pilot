using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
	public int index = 0;
	public float hp = 100;

	public bool isDead = false;
	public bool isGoal = false;
}
[System.Serializable]
public class EngineInfo {
	public float power = 100.0f;
	public Transform engine1;		 	// Left Engine
	public Transform engine2;  			// Right Engine
	public SpriteRenderer igniteEffect1;    // Left Engine Effect
	public SpriteRenderer igniteEffect2;	// Right Engine Effect
}
public class Player : MonoBehaviour {
	public static int localIndex = 0;
	public PlayerInfo pInfo;
	public EngineInfo eInfo;
	private Rigidbody2D rb;

	public event EngineIdentity OnUseEngine;
	public event EngineIdentity OnUnUseEngine;
	public event CollisionProcess OnDamaged;
	public event NoParameter OnDead;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		eInfo.igniteEffect1 = eInfo.engine1.GetComponent<SpriteRenderer> ();
		eInfo.igniteEffect2 = eInfo.engine2.GetComponent<SpriteRenderer> ();
	}


	public void OnCollisionEnter2D (Collision2D col)
	{
		Damaged (col);
	}

	public void OnCollisionStay2D (Collision2D col)
	{
		Damaged (col);
	}

	public static void SetLocalIndex (int index)
	{
		localIndex = index;
	}

	public void Damaged (Collision2D col)
	{
		//질량과 속도에 비례한 데미지를 입습니다.
		float damage = Mathf.Max(1f, rb.mass * rb.velocity.magnitude);
		pInfo.hp = Mathf.Max (0f, pInfo.hp - damage);

		if (null != OnDamaged)
			OnDamaged (col, this);

		if (pInfo.hp <= 0) {
			pInfo.isDead = true;
			if (null != OnDead)
				OnDead ();
		}
	}

	public void UseEngine (EngineDirection d)
	{
		if (d == EngineDirection.Left)
			UseLeftEngine ();
		else if (d == EngineDirection.Right)
			UseRightEngine ();
		if (null != OnUseEngine)
			OnUseEngine (d);
	}

	public void UnUseEngine (EngineDirection d)
	{
		if (d == EngineDirection.Left)
			UnUseLeftEngine ();
		else if (d == EngineDirection.Right)
			UnUseRightEngine ();
		if (null != OnUnUseEngine)
			OnUnUseEngine (d);
	}

	private void UseLeftEngine ()
	{
		rb.AddForceAtPosition(eInfo.power * eInfo.engine1.up, eInfo.engine1.position);
		eInfo.igniteEffect1.enabled = true;
	}
	private void UseRightEngine ()
	{
		rb.AddForceAtPosition(eInfo.power * eInfo.engine2.up, eInfo.engine2.position);
		eInfo.igniteEffect2.enabled = true;
	}
	private void UnUseLeftEngine ()
	{
		eInfo.igniteEffect1.enabled = false;
	}
	private void UnUseRightEngine ()
	{
		eInfo.igniteEffect2.enabled = false;
	}
}