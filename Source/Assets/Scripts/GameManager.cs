using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }
	
	public GameObject goblinBall;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	
	public Player[] Players;
	
	private Transform player1_Transform;
	private Transform player2_Transform;
	private Transform player3_Transform;
	private Transform player4_Transform;
	private Vector3 player1_Initial;
	private Vector3 player2_Initial;
	private Vector3 player3_Initial;
	private Vector3 player4_Initial;
	
	public float SecondsLeft = 60;
	private bool ballPlayed = false;
	
	CharacterPicks picks;
	
	public static GameManager Instance { get; private set; }
	
	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		}
		
		picks = GameObject.Find("CharacterPicks").GetComponent<CharacterPicks>();
		
		GameObject p1o = (GameObject)Instantiate(player1);
		Player p1 = p1o.GetComponent<Player>();
		p1.character = picks.team1[0];
		SetScale(p1o, p1.character);
		player1_Transform = p1o.transform;
		player1_Initial = player1_Transform.position;
		
		GameObject p2o = (GameObject)Instantiate(player2);
		Player p2 = p2o.GetComponent<Player>();
		p2.character = picks.team1[1];
		player2_Transform = p2o.transform;
		player2_Initial = player2_Transform.position;
		
		GameObject p3o = (GameObject)Instantiate(player3);
		Player p3 = p3o.GetComponent<Player>();
		p3.character = picks.team2[0];
		player3_Transform = p3o.transform;
		player3_Initial = player3_Transform.position;
		
		GameObject p4o = (GameObject)Instantiate(player4);
		Player p4 = p4o.GetComponent<Player>();
		p4.character = picks.team2[1];
		player4_Transform = p4o.transform;
		player4_Initial = player4_Transform.position;
		
		Entities.Add(p1o);
		Entities.Add(p2o);
		Entities.Add(p3o);
		Entities.Add(p4o);
		
		Players = new Player[4]{
			p1o.GetComponent<Player>(), 
			p2o.GetComponent<Player>(), 
			p3o.GetComponent<Player>(), 
			p4o.GetComponent<Player>()
		};
		
		Entities.AddRange(GameObject.FindGameObjectsWithTag("Goal"));
		
		NewBall ();
	}
	
	public void SetScale(GameObject obj, Player.Character character)
	{
		if(character == Player.Character.RIRONMAN)
			obj.transform.localScale = new Vector3(2, 2, 2);
		
		else if(character == Player.Character.LE_TRUC)
			obj.transform.localScale = new Vector3(2, 2, 3);
		
		else if(character == Player.Character.TURQUOISE_MAGE)
			obj.transform.localScale = new Vector3(1.3f, 2, 2);
	}
	
	public void DestroyObject(GameObject o) {
		if (o.GetComponent<GoblinBall>() != null) {
			ballPlayed = false;
		}
		
		Entities.Remove(o);
		Destroy (o);
	}
	
	public void NewBall() {
		Entities.Add((GameObject)Instantiate(goblinBall));
		player1_Transform.position = player1_Initial;
		player2_Transform.position = player2_Initial;
		player3_Transform.position = player3_Initial;
		player4_Transform.position = player4_Initial;
		ballPlayed = true;
	}
	
	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
	
	private void Update() {
		if (ballPlayed) {
			SecondsLeft -= Time.deltaTime;
			if (SecondsLeft <= 0) {
                Application.LoadLevel("EndingScreen");
			}
		}
	}
}
