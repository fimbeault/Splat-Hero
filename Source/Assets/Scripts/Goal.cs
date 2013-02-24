using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Goal : MonoBehaviour {
	public int Score;
	private GUIText label;
	public Player.PlayerID Owner;
	
	public static Goal Player1_Goal;
	public static Goal Player2_Goal;
	
	private void Start() {
		if (Owner == Player.PlayerID.PLAYER1 && Player1_Goal == null) {
			Player1_Goal = this;
			DontDestroyOnLoad(this);
		}
		
		if (Owner == Player.PlayerID.PLAYER2 && Player2_Goal == null) {
			Player2_Goal = this;
			DontDestroyOnLoad(this);
		}
	}
	
	private void OnCollisionEnter(Collision other) {
		GoblinBall candidate = other.gameObject.GetComponent<GoblinBall>();
		if (candidate != null && candidate.isLocked) {
			Score++;
			
			AudioClip clip = (AudioClip)Resources.Load("Audio/kill");
			gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
			
			GameManager.Instance.DestroyObject(candidate.gameObject);
			StartCoroutine(MakeBallSoonish());
			SplatterShower.Instance.ShowSplatter();
		}
	}
	
	private IEnumerator MakeBallSoonish() {
			yield return new WaitForSeconds(1.5f);
			GameManager.Instance.NewBall();
		
			AudioClip clip = (AudioClip)Resources.Load("Audio/whistle");
			gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
	}
}
