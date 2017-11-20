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
            //카메라의 위치는 지정된 플레이어 (Gameobject Player)의 x값을 따라 움직입니다. y,z 값은 바뀌지 않습니다.
            Camera.main.transform.position = new Vector3(Player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
            
            //게임 내의 MapGenerate에서 맵의 크기(지붕과 바닥의 길이)를 받아와서 카메라가 그 이외를 넘어가면 고정시킵니다. 
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
