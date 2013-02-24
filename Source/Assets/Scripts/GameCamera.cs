using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour 
{
    private Entity currentBall;
    private GameObject leftBorder;
    private GameObject rightBorder;

    private List<Entity> playerList = new List<Entity>();
    private List<GameObject> playerMarkerList = new List<GameObject>();

    public GameObject marker_LeTruc;
    public GameObject marker_Rironman;
    public GameObject marker_TurquoiseMage;
    public GameObject marker_Shipper;

    private float resolutionCorrection;
    private float heroPointerScreenPosX, heroNotVisibleScreenPosX;


	// Use this for initialization
	void Start () 
    {
        leftBorder = GameObject.Find("wallLeft");
        rightBorder = GameObject.Find("wallRight");
 
        resolutionCorrection = transform.camera.aspect * transform.camera.orthographicSize;
        heroPointerScreenPosX = resolutionCorrection - 5;
        heroNotVisibleScreenPosX = resolutionCorrection + 5;
	}

    public void SetNewTarget(Entity target)
    {
        currentBall = target;
    }

    public void RegisterPlayer(Entity aPlayer, Player.Character hero)
    {
        playerList.Add(aPlayer);
        
        if(hero == Player.Character.LE_TRUC)
            playerMarkerList.Add(GameObject.Instantiate(marker_LeTruc) as GameObject);

        else if (hero == Player.Character.RIRONMAN)
            playerMarkerList.Add(GameObject.Instantiate(marker_Rironman) as GameObject);

        else if (hero == Player.Character.TURQUOISE_MAGE)
            playerMarkerList.Add(GameObject.Instantiate(marker_TurquoiseMage) as GameObject);

        else if (hero == Player.Character.SHIPPER)
            playerMarkerList.Add(GameObject.Instantiate(marker_Shipper) as GameObject);
    }

	// Update is called once per frame
	void Update () 
    {
        float playerPosX, playerPosZ;

        if (currentBall != null)
        {
            float currentBallPosX = currentBall.transform.position.x * 0.6f;

            transform.position = new Vector3(currentBallPosX, transform.position.y, transform.position.z);
        }	

        if(playerList.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                playerPosX = playerList[i].transform.position.x;
                playerPosZ = playerList[i].transform.position.z;

                if (playerPosX >= transform.position.x + heroNotVisibleScreenPosX)
                {
                    playerMarkerList[i].renderer.enabled = true;
                    playerMarkerList[i].transform.position = new Vector3(transform.position.x + heroPointerScreenPosX, 0.0f, playerPosZ);
                }

                else if (playerPosX <= transform.position.x - heroNotVisibleScreenPosX)
                {
                    playerMarkerList[i].renderer.enabled = true;
                    playerMarkerList[i].transform.position = new Vector3(transform.position.x - heroPointerScreenPosX, 0.0f, playerPosZ);
                }

                else
                {
                    playerMarkerList[i].renderer.enabled = false;
                }
            }
        }
	}
}
