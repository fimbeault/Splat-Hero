using UnityEngine;
using System.Collections;

public class SplatterShower : MonoBehaviour {
	public static SplatterShower Instance { get; private set; }
	private Spritesheet sprites;
		
	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		
		sprites = new Spritesheet(gameObject);
		sprites.Load("Sprites/blood");
		sprites.CreateAnimation("Splash", 50);
		sprites.AddFrame("Splash", 0, 0, 50, 50);
		sprites.AddFrame("Splash", 50, 0, 50, 50);
		sprites.AddFrame("Splash", 100, 0, 50, 50);
		sprites.AddFrame("Splash", 150, 0, 50, 50);
		sprites.AddFrame("Splash", 200, 0, 50, 50);
		sprites.AddFrame("Splash", 0, 50, 50, 50);
		sprites.AddFrame("Splash", 50, 50, 50, 50);
		sprites.AddFrame("Splash", 100, 50, 50, 50);
		sprites.AddFrame("Splash", 150, 50, 50, 50);
		sprites.AddFrame("Splash", 200, 50, 50, 50);
		sprites.AddFrame("Splash", 0, 100, 50, 50);
		sprites.AddFrame("Splash", 50, 100, 50, 50);
		sprites.AddFrame("Splash", 100, 100, 50, 50);
		sprites.AddFrame("Splash", 150, 100, 50, 50);
		sprites.AddFrame("Splash", 200, 100, 50, 50);
		sprites.AddFrame("Splash", 0, 150, 50, 50);
		sprites.AddFrame("Splash", 50, 150, 50, 50);
		sprites.AddFrame("Splash", 100, 150, 50, 50);
		sprites.AddFrame("Splash", 150, 150, 50, 50);
		sprites.AddFrame("Splash", 200, 150, 50, 50);
		sprites.AddFrame("Splash", 0, 200, 50, 50);
		sprites.AddFrame("Splash", 50, 200, 50, 50);
		sprites.AddFrame("Splash", 100, 200, 50, 50);
		sprites.AddFrame("Splash", 150, 200, 50, 50);
		sprites.AddFrame("Splash", 200, 200, 50, 50);
		
		sprites.SetCurrentAnimation("Splash");
		sprites["Splash"].AnimationCompleted += Fade;
		
		Fade ();
	}
	
	public void ShowSplatter() {
		sprites.Reset();
		gameObject.renderer.enabled = true;
	}
	
	private void Fade() {
		gameObject.renderer.enabled = false;
	}
	
	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
	
	void Update() {
		if (gameObject.renderer.enabled) {
			sprites.Render();
		}
	}
}
