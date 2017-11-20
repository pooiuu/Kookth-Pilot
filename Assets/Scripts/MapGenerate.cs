using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerate : MonoBehaviour {

    //장애물 관련 변수
    public GameObject PlayerPrefab;//플레이어 prefab (플레이어의 크기 계산을 위해서 사용됨)
    public GameObject pillarPrefab;//기둥 prefab
    public GameObject floor;//지붕과 바닥 prefab
    public GameObject Goal; //결승선 prefab

    //뷰포트 크기에 따른 맵의 높이와 너비
    public float viewWidth;
    public float viewHeight;

    //맵의 넓이 = 천장(바닥)의 길이(Camera 무빙에 사용)
    public float mapWidth;

    //랜덤 맵 제너레이터 관련 변수들
    public int pillarsNum = 10; //생성할 기둥 쌍(위 아래)의 개수 
    public float pillarSpace = 5; //기둥이 나타나는 간격

    //장애물 기둥의 포지션. createPillar에서 다룹니다.
    public Vector3 pillarPos;

    //지붕과 바닥의 위치
    Vector3 roofPos;
    Vector3 floorPos;

    public GameObject[] instFloor;



    //랜덤 맵을 생성하는 함수(Start 문에서 사용합니다)
    void createPillar()
    {

        //기둥 시작 position. 기둥 시작은 viewport 오른쪽 끝자락부터. 현재 Viewport의 좌표를 월드 좌표계로 받아옵니다.
        Vector3 startPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        //기둥의 첫 시작 position은 Viewport의 오른쪽 끝입니다.(1, 0.5) 좌표 > Viewport의 X축 오른쪽 끝, Y축 상 가운데.
        pillarPos = new Vector3(startPos.x,startPos.y,0);

        //위 아래 기둥을 저장할 배열입니다.
        GameObject[] tempPillar = new GameObject[2];


        //플레이어(비행물체)의 Width 값을 받아옵니다. 이는 플레이어의 콜라이더(BoxCollider2D)의 x 너비를 받아옵니다.
        float playerSize = PlayerPrefab.GetComponent<Collider2D>().bounds.size.x;
        //기둥의 너비를 Random 값으로 받기 위해 선언한 변수입니다.
        float pillarWidth;
        //위 아래 기둥 사이의 간격을 Random 값으로 받기 위해 선언한 변수입니다.
        float gapSize;
        //기둥 사이의 간격의 높이를 Random 값으로 받기 위한 변수입니다.
        float gapHeight;
        //기둥과 기둥사이의 간격을 임의로 설정하기 위한 변수입니다.
        float tempSpace;

        //어떤 화면에서든 맵 생성이 잘 될 수 있게 하기 위해 View의 높이를 구합니다. Viewport의 Y축 최소에서 최대까지의 World 좌표계를 구하여 계산합니다.
        viewHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        //장애물 기둥의 기본 길이는 ViewHeight입니다. X크기를 0으로 둔 이유는 후에 pillarWidth를 이용하여 Random값으로 받기 위함입니다.
        pillarPrefab.transform.localScale = new Vector3(0, viewHeight, 1);

        for (int i=0; i<pillarsNum; i++)
        {
            //기둥의 너비를 Random으로 받음 (PlayerSize 는 현재 플레이어(비행물체)의 Width 값을 받았습니다. Random.range 를 이용하여 Width값의 1/2에서 X2 까지의 값 중에서 랜덤하게 받아옵니다.)
            pillarWidth = Random.Range(playerSize / 2, playerSize * 2);
            //기둥 사이의 간격을 Random으로 받음 (역시 PlayerSize 를 이용하여 Random 값을 받습니다.)
            gapSize = Random.Range(playerSize, playerSize * 2);

            gapHeight = Random.Range(-viewHeight / 2 + gapSize / 2, viewHeight / 2 - gapSize / 2);

            for (int j = 0; j < 2; j++)
            {
                //기둥을 생성합니다.
                tempPillar[j] = Instantiate(pillarPrefab, pillarPos, Quaternion.identity);
                //기둥들의 Parent는 Mapgenrator 코드를 지니고 있는 객체로 설정됩니다.
                tempPillar[j].transform.parent = transform;
                //Random 값으로 받은 pillarWidth를 localScale에 더하여 기둥의 Width를 설정합니다.
                tempPillar[j].transform.localScale += new Vector3(pillarWidth, 0, 0);

            }
            //맵의 높이의 1/2(= 중간) + gapsize를 위 아래 기둥에 각각 적용합니다. gapsize만큼 위 아래 기둥의 간격을 벌립니다.
            tempPillar[0].transform.position += new Vector3(0, (viewHeight / 2 + gapSize / 2) + gapHeight, 0);
            tempPillar[1].transform.position += new Vector3(0, -(viewHeight / 2 + gapSize / 2) + gapHeight, 0);

            if (i >= pillarsNum * (0.6f))
            {
                //맵의 60퍼 이상에 도달하면 기둥과 기둥간의 간격이 랜덤하게 줄어듭니다.
                float temp1 = pillarWidth + playerSize;
                float temp2 = pillarWidth + pillarSpace;
                if(temp1 > temp2)
                {
                    tempSpace = Random.Range(temp2, temp1);
                }else
                {
                    tempSpace = Random.Range(temp1, temp2);
                }
            }
            else
            {
                tempSpace = pillarWidth + pillarSpace;
            }
            
            //장애물 기둥의 위치를 지정합니다.
            pillarPos += new Vector3(tempSpace, 0, 0);
        }
    }

    //맵의 왼쪽, 오른쪽 끝에 벽을 생성합니다.
	void createWall()
	{
		Vector3 startPos = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0.5f, 0f)) + Vector3.right*0.5f; // 벽의 두깨가 1이기에 0.5만큼 더 밀어줌
		startPos.z = 0f;
		Vector3 endPos = startPos + Vector3.right * (mapWidth - 1f);
		GameObject leftWall = Instantiate (pillarPrefab, startPos, Quaternion.identity);
		GameObject rightWall = Instantiate (pillarPrefab, endPos, Quaternion.identity);
		//createPillar에서 pillarPrefab의 사이즈를 Vector3 (0, height, 1)로 지정함
		leftWall.transform.localScale += Vector3.right; // 각 벽의 x 사이즈를 1로 지정
		rightWall.transform.localScale += Vector3.right;

		leftWall.name = "LeftWall";
		rightWall.name = "RightWall";
		leftWall.transform.parent = transform;
		rightWall.transform.parent = transform;

		//Create Goal
		float goalSizeX = Goal.GetComponent<BoxCollider2D>().size.x;
		GameObject tmpGoal = Instantiate(Goal, endPos - Vector3.right * goalSizeX, Quaternion.identity);
		tmpGoal.transform.parent = transform;
		tmpGoal.transform.localScale = new Vector3 (tmpGoal.transform.localScale.x, viewHeight, 1f);
	}

    // Use this for initialization
    // 맵의 생성은 한 번만 해도 되므로 Start에서 시행합니다.
    void Start () {
        instFloor = new GameObject[2];
        viewWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        createPillar();
        //천장과 바닥 위치와 크기 설정
        roofPos = new Vector3(pillarPos.x / 2, Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y, 0);
        floorPos = new Vector3(pillarPos.x / 2, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 0);
        instFloor[0] = Instantiate(floor, roofPos, Quaternion.identity);
        instFloor[1] = Instantiate(floor, floorPos, Quaternion.identity);

        for (int i = 0; i < 2; i++)
        {
            Vector3 tempScale = instFloor[i].transform.localScale;
            instFloor[i].transform.localScale = new Vector3(viewWidth + pillarPos.x, tempScale.y, tempScale.z);
           
        }
        mapWidth = viewWidth + pillarPos.x;
		createWall ();
    }

    private void Update()
    {
        //R을 누르면 scene을 재시작합니다.
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
        }
    }
}
