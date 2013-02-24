using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
    public GameObject team1Score;
    public GameObject team2Score;
    public GameObject team1Txt;
    public GameObject team2Txt;

    void Start()
    {
        team1Score.GetComponent<TextMesh>().text = (Goal.Player1_Goal.Score).ToString();
        team1Score.renderer.enabled = false;
        team1Txt.renderer.enabled = false;
        team2Score.GetComponent<TextMesh>().text = (Goal.Player2_Goal.Score).ToString();
        team2Score.renderer.enabled = false;
        team2Txt.renderer.enabled = false;
    }

	// Use this for initialization
	void Awake () 
    {
        //this.gameObject.rigidbody.AddForce(250000.0f, 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () 
    {
	}
}
