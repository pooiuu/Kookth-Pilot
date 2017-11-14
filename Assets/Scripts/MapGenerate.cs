using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour {

    //장애물 관련 변수
    public GameObject PlayerPrefab;//플레이어 prefab (플레이어의 크기 계산을 위해서 사용됨)
    public GameObject pillarPrefab;//기둥 prefab
    public GameObject floor;//지붕과 바닥 prefab
    public GameObject Goal; //결승선 prefab
    public float floorHeight;//지붕과 바닥 prefab의 높이

    //뷰포트 크기에 따른 맵의 높이와 너비
    public float viewWidth;
    public float viewHeight;

    //랜덤 맵 제너레이터 관련 변수들
    public int pillarsNum = 10;
    public float pillarSpace = 5;
    
    //지붕과 바닥의 위치
    Vector3 roofPos;
    Vector3 floorPos;

    GameObject[] instFloor;


    //랜덤 맵을 생성하는 함수(Start 문에서 사용합니다)
    void createPillar()
    {
        
        Vector3 startPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));//기둥 시작 position. 기둥 시작은 viewport 오른쪽 끝자락부터.
        Vector3 pillarPos = new Vector3(startPos.x,startPos.y,0);

        GameObject[] tempPillar = new GameObject[2];
        float playerSize = PlayerPrefab.GetComponent<Collider2D>().bounds.size.x;
        float pillarWidth;
        float gapSize;

        viewHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        pillarPrefab.transform.localScale = new Vector3(0, viewHeight, 1);

        for (int i=0; i<pillarsNum; i++)
        {
            pillarWidth = Random.Range(playerSize / 2, playerSize * 2);
            gapSize = Random.Range(playerSize, playerSize * 2);
            for (int j = 0; j < 2; j++)
            {
                tempPillar[j] = Instantiate(pillarPrefab, pillarPos, Quaternion.identity);
                tempPillar[j].transform.parent = transform;
                tempPillar[j].transform.localScale += new Vector3(pillarWidth, 0, 0);

            }
            tempPillar[0].transform.position += new Vector3(0, viewHeight / 2 + gapSize / 2, 0);
            tempPillar[1].transform.position += new Vector3(0, -(viewHeight / 2 + gapSize / 2), 0);
            pillarPos += new Vector3(pillarSpace, 0, 0);
        }
    }

    // Use this for initialization
    void Start () {
        instFloor = new GameObject[2];
        floorHeight = floor.GetComponent<BoxCollider2D>().bounds.size.y;

        viewWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        //천장과 바닥 위치와 크기 설정
        roofPos = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y, 0);
        floorPos = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 0);
        instFloor[0] = Instantiate(floor, roofPos, Quaternion.identity);
        instFloor[1] = Instantiate(floor, floorPos, Quaternion.identity);

        for (int i = 0; i < 2; i++)
        {
            Vector3 tempScale = instFloor[i].transform.localScale;
            instFloor[i].transform.localScale = new Vector3(viewWidth, tempScale.y, tempScale.z);
        }

        createPillar();

    }
	
	// Update is called once per frame
	void Update () {
        instFloor[0].transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y, 0);
        instFloor[1].transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 0);
    }

}
