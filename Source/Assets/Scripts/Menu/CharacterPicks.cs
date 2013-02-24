using UnityEngine;
using System.Collections;

public class CharacterPicks : MonoBehaviour {
	
	public Player.Character[] team1 = new Player.Character[2];
	public Player.Character[] team2 = new Player.Character[2];
	
	void Awake()
	{
		Object.DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
