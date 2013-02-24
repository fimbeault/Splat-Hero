using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public Texture2D HUDBackground;
	public Font TimerFont;
	private float scale;
	private Texture2D activeBar;
	private Texture2D readyBar;
	private Texture2D coolingDownBar;
	
	private void Start ()
	{
		Texture2D cropped = new Texture2D (581, 60);
		cropped.SetPixels (HUDBackground.GetPixels (0, 4, 581, 60));
		cropped.Apply ();
		HUDBackground = cropped;
		
		scale = Screen.width / 581.0f;
		
		activeBar = new Texture2D (1, 1);
		activeBar.SetPixel (0, 0, Color.cyan);
		activeBar.Apply ();
		
		readyBar = new Texture2D (1, 1);
		readyBar.SetPixel (0, 0, Color.green);
		readyBar.Apply ();
		
		coolingDownBar = new Texture2D (1, 1);
		coolingDownBar.SetPixel (0, 0, Color.yellow);
		coolingDownBar.Apply ();
	}
	
	private void OnGUI ()
	{
		GUIStyle style = new GUIStyle ();
		style.fontSize = (int)(27 * scale);
		style.font = TimerFont;
		style.alignment = TextAnchor.MiddleCenter;
		
		
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width / 10), HUDBackground);
		ScalableRect r = new ScalableRect (220, 0, 38, 38);
		GUI.DrawTexture (r * scale, GameManager.Instance.Players[1].Icon, ScaleMode.ScaleToFit);
		r = new ScalableRect (324, 0, 38, 38);
		GUI.DrawTexture (r * scale, GameManager.Instance.Players[3].Icon, ScaleMode.ScaleToFit);
		
		r = new ScalableRect (180, 0, 50, 50);
		GUI.DrawTexture (r * scale, GameManager.Instance.Players[0].Icon, ScaleMode.ScaleToFit);
		r = new ScalableRect (351, 0, 50, 50);
		GUI.DrawTexture (r * scale, GameManager.Instance.Players[2].Icon, ScaleMode.ScaleToFit);
		
		r = new ScalableRect (258, 0, 324 - 258, 40);
		GUI.Box (r * scale, GameManager.Instance.SecondsLeft.ToString ("0"), style);
		
		
		r = new ScalableRect (4, 3, 44, 44);
		GUI.Box (r * scale, Goal.Player1_Goal.Score.ToString (), style);
		r = new ScalableRect (533, 3, 44, 44);
		GUI.Box (r * scale, Goal.Player2_Goal.Score.ToString (), style);
		
		
		r = new ScalableRect (52, 8, 70, 10);
		style.fontSize = (int)(12 * scale);
		GUI.Box (r * scale, "Cooldowns", style);
		
		Texture t;
		Power p;
		float pct;
		//P1
		p = GameManager.Instance.Players [0].ShownCooldown;
		t = readyBar;
		pct = 1;
		if (p.powerInUse) {
			t = activeBar;
			pct = (p.useCooldown - p.cooldown) / p.useCooldown;
			
		} else if (p.powerInCooldown) {
			t = coolingDownBar;
			pct = Mathf.Abs((p.cooldown - p.useCooldown) / (p.powerCooldown - p.useCooldown));
		}
		r = new ScalableRect (52, 34, 70 * pct, 6);
		GUI.DrawTexture (r * scale, t);
		
		//P2
		p = GameManager.Instance.Players [1].ShownCooldown;
		t = readyBar;
		pct = 1;
		if (p.powerInUse) {
			t = activeBar;
			pct = (p.useCooldown - p.cooldown) / p.useCooldown;
		} else if (p.powerInCooldown) {
			t = coolingDownBar;
			pct = Mathf.Abs((p.cooldown - p.useCooldown) / (p.powerCooldown - p.useCooldown));
		}
		r = new ScalableRect (72, 24, 70 * pct, 6);
		GUI.DrawTexture (r * scale, t);
		
		r = new ScalableRect (459, 8, 70, 10);
		GUI.Box (r * scale, "Cooldowns", style);
		
		//P3
		p = GameManager.Instance.Players [2].ShownCooldown;
		t = readyBar;
		pct = 1;
		if (p.powerInUse) {
			t = activeBar;
			pct = (p.useCooldown - p.cooldown) / p.useCooldown;
		} else if (p.powerInCooldown) {
			t = coolingDownBar;
			pct = Mathf.Abs((p.cooldown - p.useCooldown) / (p.powerCooldown - p.useCooldown));
		}
		r = new ScalableRect (459, 34, 70 * pct, 6);
		GUI.DrawTexture (r * scale, t);
		
		//P4
		p = GameManager.Instance.Players [3].ShownCooldown;
		t = readyBar;
		pct = 1;
		if (p.powerInUse) {
			t = activeBar;
			pct = (p.useCooldown - p.cooldown) / p.useCooldown;
		} else if (p.powerInCooldown) {
			t = coolingDownBar;
			pct = Mathf.Abs((p.cooldown - p.useCooldown) / (p.powerCooldown - p.useCooldown));
		}
		r = new ScalableRect (439, 24, 70 * pct, 6);
		GUI.DrawTexture (r * scale, t);
	}
}

public struct ScalableRect
{
	private float x;
	private float y;
	private float w;
	private float h;
		
	public ScalableRect (Rect rhs)
	{
		x = rhs.x;
		y = rhs.y;
		w = rhs.width;
		h = rhs.height;
	}
		
	public ScalableRect (float left, float top, float width, float height)
	{
		x = left;
		y = top;
		w = width;
		h = height;
	}
		
	public static ScalableRect operator* (ScalableRect r, float m)
	{
		return new ScalableRect (r.x * m, r.y * m, r.w * m, r.h * m);
	}
		
	public static implicit operator Rect (ScalableRect r)
	{
		return new Rect (r.x, r.y, r.w, r.h);
	}
}