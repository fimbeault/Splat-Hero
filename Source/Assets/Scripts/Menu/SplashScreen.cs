using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	
	public Texture mSplashScreenTex;
	public Texture mBlackTexture;
	
	float mAlphaFadeValue = 1;
	bool allowSkip = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(allowSkip && Input.GetAxisRaw("Player1_Fire") > 0)
			StartCoroutine( LoadLevel("selection") );
	}
	
	public IEnumerator LoadLevel(string level)
	{
		yield return new WaitForSeconds(2);
        Application.LoadLevel(level);
	}
	
	void OnGUI()
	{
		Rect splash = new Rect(0,0,Screen.width,Screen.height);
		GUI.DrawTexture(splash, mSplashScreenTex);
		
		if (mAlphaFadeValue > 0)
			mAlphaFadeValue -= Mathf.Clamp01(Time.deltaTime);
		
		else
			allowSkip = true;
		
		GUI.color = new Color(0,0,0, mAlphaFadeValue);
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), mBlackTexture);
	}
}
