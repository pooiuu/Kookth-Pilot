using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject Player;
    public MapGenerate MG;
    public float mapWidth;
	// Use this for initialization
	void Start () {
        MG = GameObject.Find("Mapgenerator").GetComponent<MapGenerate>();
        mapWidth = MG.mapWidth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player != null)
        {
            Camera.main.transform.position = new Vector3(Player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

            if (Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x < (MG.instFloor[0].transform.position.x - mapWidth / 2))
            {
                Camera.main.transform.position = new Vector3((MG.instFloor[0].transform.position.x - mapWidth / 2) + MG.viewWidth / 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            else if (Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x > (MG.instFloor[0].transform.position.x + mapWidth / 2))
            {
                Camera.main.transform.position = new Vector3((MG.instFloor[0].transform.position.x + mapWidth / 2) - MG.viewWidth / 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
        }
	}
}
