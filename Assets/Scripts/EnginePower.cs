using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : MonoBehaviour {
    public float power = 100.0f;
    public Transform engine1;
    public Transform engine2;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForceAtPosition(power * engine1.up, engine1.position);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForceAtPosition(power * engine2.up, engine2.position);
        }
    }
    
}
