using UnityEngine;
using System.Collections;

public class SplatterOnTheWall : MonoBehaviour 
{
    bool showGui = false;

    float resolutionMod;
    private GameObject cam;

	// Use this for initialization
	void Start () 
    {

        cam = GameObject.Find("Main Camera");
        resolutionMod = cam.transform.camera.aspect * cam.transform.camera.orthographicSize;

        SplatterShower.Instance.ShowSplatter();

        StartCoroutine(faireLeTruc());

	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    IEnumerator faireLeTruc()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject.GetComponent<SplatterShower>());
        gameObject.renderer.enabled = true;
        gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("image/TacheSang");

        cam.GetComponent<CameraManager>().team1Score.renderer.enabled = true;
        cam.GetComponent<CameraManager>().team1Txt.renderer.enabled = true;
        cam.GetComponent<CameraManager>().team2Score.renderer.enabled = true;
        cam.GetComponent<CameraManager>().team2Txt.renderer.enabled = true;

        showGui = true;
    }

    void OnGUI()
    { 
    
        if (showGui)
            if (GUI.Button(new Rect(resolutionMod + 300.0f, resolutionMod + 5.0f, 200.0f, 50.0f), "Click to start a new game"))
                Application.LoadLevel("Selection");
    }
}
