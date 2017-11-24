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
	public Transform engine1;
	public Transform engine2;
}
public class Player : MonoBehaviour {
    public static int localIndex = 0;
    public PlayerInfo pInfo;
    public EngineInfo eInfo;
    public KeyCode Linput;
    public KeyCode Rinput;

    private Rigidbody2D rb;

    public event CollisionProcess OnDamaged;
    public event NoParameter OnDead;
    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(Linput != KeyCode.None && Rinput != KeyCode.None)
        {
            if (Input.GetKey(Linput))
            {
                UseLeftEngine();
                eInfo.engine1.gameObject.SetActive(true);
            }
            else
            {
                eInfo.engine1.gameObject.SetActive(false);
            }
            if (Input.GetKey(Rinput))
            {
                UseRightEngine();
                eInfo.engine2.gameObject.SetActive(true);
            }
            else
            {
                eInfo.engine2.gameObject.SetActive(false);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Damaged(col);
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        Damaged(col);
    }

    public static void SetLocalIndex(int index)
    {
        localIndex = index;
    }

    public void Damaged(Collision2D col)
    {
        //질량과 속도에 비례한 데미지를 입습니다.
        float damage = Mathf.Max(1f, rb.mass * rb.velocity.magnitude);
        pInfo.hp = Mathf.Max(0f, pInfo.hp - damage);

        if (null != OnDamaged)
            OnDamaged.Invoke(col, this);

        if (pInfo.hp <= 0) {
            pInfo.isDead = true;
            if (null != OnDead)
                OnDead.Invoke();
        }
    }

    private void UseLeftEngine ()
    {
        rb.AddForceAtPosition(eInfo.power * eInfo.engine1.up, eInfo.engine1.position);
    }
    private void UseRightEngine ()
    {
        rb.AddForceAtPosition(eInfo.power * eInfo.engine2.up, eInfo.engine2.position);
    }




}
