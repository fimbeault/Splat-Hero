using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {
	
	public GameObject buttonPrefab = null;
	public GameObject cursor = null;
	
	public GameObject rironman = null;
	public GameObject leTruc = null;
	public GameObject turquoiseMage = null;
	public GameObject shipper = null;
	
	GameObject menuCursor;
	int xIndex = 0;
	int yIndex = 0;
	GameObject[,] buttons = new GameObject[2,2];
	
	int activeTeam = 1;
	int choosenTeam1 = 0;
	int choosenTeam2 = 0;
	
	CharacterPicks picks;
	
	// Use this for initialization
	void Start () {
	
		picks = GameObject.Find("CharacterPicks").GetComponent<CharacterPicks>();
			
			
		GameObject b1 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b1.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Rironman.icon");
		b1.transform.position = new Vector3(-20,20,0);
		CharacterButton button1 = b1.AddComponent<CharacterButton>();
		button1.character = Player.Character.RIRONMAN;
		
		GameObject b2 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b2.AddComponent<CharacterButton>();
		b2.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Truc.icon");
		b2.transform.position = new Vector3(20,20,0);
		CharacterButton button2 = b2.AddComponent<CharacterButton>();
		button2.character = Player.Character.LE_TRUC;
		
		GameObject b3 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b3.AddComponent<CharacterButton>();
		b3.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Turquoise.icon");
		b3.transform.position = new Vector3(-20,-20,0);
		CharacterButton button3 = b3.AddComponent<CharacterButton>();
		button3.character = Player.Character.TURQUOISE_MAGE;
		
		GameObject b4 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b4.AddComponent<CharacterButton>();
		b4.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Shipper.icon");
		b4.transform.position = new Vector3(20,-20,0);
		CharacterButton button4 = b4.AddComponent<CharacterButton>();
		button4.character = Player.Character.RIRONMAN;
		
		buttons[0,0] = b1;
		buttons[1,0] = b2;
		buttons[0,1] = b3;
		buttons[1,1] = b4;
		
		menuCursor = (GameObject)GameObject.Instantiate(cursor);
		menuCursor.transform.position = new Vector3(-20,10,-6);
		
		DetermineFirstTeam();
		PlayAudio();
	}
	
	void DetermineFirstTeam()
	{
		activeTeam = Random.Range(1, 3);
		
		if(activeTeam == 1)
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("TeamSelectorLeft");
		
		else
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("TeamSelectorRight");
	}
	
	void PlayAudio()
	{
		AudioClip clip = (AudioClip)Resources.Load("Audio/chooseTeam");
		gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
	}
	
	// Update is called once per frame
	void Update () {
	
		MoveCursor();
		SelectCharacter();
		
		if(choosenTeam1 + choosenTeam2 > 3)
		{
			StartCoroutine( LoadLevel("main") );
		}
	}
	
	public IEnumerator LoadLevel(string level)
	{
		yield return new WaitForSeconds(2);
        Application.LoadLevel(level);
	}
	
	void MoveCursor()
	{
		float xMovement = Input.GetAxis("Player1_MoveX");
		float zMovement = -Input.GetAxis("Player1_MoveZ");
		
		if(xMovement < 0 && xIndex > 0)
			xIndex--;
		
		if(xMovement > 0 && xIndex < 1)
			xIndex++;
		
		if(zMovement < 0 && yIndex > 0)
			yIndex--;
		
		if(zMovement > 0 && yIndex < 1)
			yIndex++;
		
		Vector3 p = buttons[xIndex, yIndex].transform.position;
		p.y -= 10;
		p.z -= 2;
		menuCursor.transform.position = p;
	}
	
	void SelectCharacter()
	{
		if(Input.GetAxisRaw("Player1_Fire") > 0)
		{
			CharacterButton button = buttons[xIndex, yIndex].GetComponent<CharacterButton>();
			
			if(button.activated == true)
			{
				button.activated = false;
				
				if(xIndex == 0)
				{
					if(yIndex == 0)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/RironmanBW.icon");
						SpawnCharacter(Player.Character.RIRONMAN);
					}
					
					else if(yIndex == 1)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/TurquoiseBW.icon");
						SpawnCharacter(Player.Character.TURQUOISE_MAGE);
					}
				}
				
				else if(xIndex == 1)
				{
					if(yIndex == 0)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/TrucBW.icon");
						SpawnCharacter(Player.Character.LE_TRUC);
					}
					
					else if(yIndex == 1)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/ShipperBW.icon");
						SpawnCharacter(Player.Character.SHIPPER);
					}
				}
			}
		}
	}
	
	void SpawnCharacter(Player.Character character)
	{
		GameObject obj = null;
		
		if(character == Player.Character.RIRONMAN)
		{
			obj = (GameObject)GameObject.Instantiate(rironman);
			obj.transform.Rotate(90, 180, 0);
		}
		
		else if(character == Player.Character.LE_TRUC)
		{
			obj = (GameObject)GameObject.Instantiate(leTruc);
			obj.transform.position = new Vector3(-80,20,0);
			obj.transform.Rotate(90, 180, 0);
		}
		
		else if(character == Player.Character.TURQUOISE_MAGE)
		{
			obj = (GameObject)GameObject.Instantiate(turquoiseMage);
			obj.transform.position = new Vector3(-80,20,0);
			obj.transform.Rotate(90, 180, 0);
		}
		
		else if(character == Player.Character.SHIPPER)
		{
			obj = (GameObject)GameObject.Instantiate(shipper);
			obj.transform.position = new Vector3(-80,20,0);
			obj.transform.Rotate(90, 180, 0);
		}
		
		if(activeTeam == 1)
		{
			picks.team1[choosenTeam1] = character;
			obj.GetComponent<CharacterImage>().SetAnimation("RunRight");
			
			if(choosenTeam1 == 0)
				obj.transform.position = new Vector3(-80,20,0);
			
			else
				obj.transform.position = new Vector3(-55,5,0);
			
			choosenTeam1++;
			
			if(choosenTeam1 + choosenTeam2 == 1 || choosenTeam1 + choosenTeam2 == 3 )
			{
				menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("TeamSelectorRight");
				activeTeam = 2;
			}
		}
		
		else
		{
			picks.team2[choosenTeam2] = character;
			obj.GetComponent<CharacterImage>().SetAnimation("RunLeft");
			
			if(choosenTeam2 == 0)
				obj.transform.position = new Vector3(80,20,0);
			
			else
				obj.transform.position = new Vector3(55,5,0);
			
			choosenTeam2++;
			
			if(choosenTeam1 + choosenTeam2 == 1 || choosenTeam1 + choosenTeam2 == 3 )
			{
				menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("TeamSelectorLeft");
				activeTeam = 1;
			}
		}
	}
}
