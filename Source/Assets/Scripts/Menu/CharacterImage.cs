using UnityEngine;
using System.Collections;

public class CharacterImage : Entity {
	
	public Player.Character character = Player.Character.RIRONMAN;
	string characterAnimation = "IdleLeft";
	
	// Use this for initialization
	void Start () {
		base.Start();
		
		switch(character)
		{
		case Player.Character.RIRONMAN:
			CreateRironman();
			break;
			
		case Player.Character.LE_TRUC:
			CreateLeTruc();
			break;
			
		case Player.Character.TURQUOISE_MAGE:
			CreateTurquoiseMage();
			break;
			
		case Player.Character.SHIPPER:
			CreateShipper();
			break;
		}
	}
	
	void CreateRironman()
	{
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/ironMan");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 32, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 64, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 96, 32, 32);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 32, 0, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 32, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 64, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 96, 32, 32);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 32, 32);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 32, 0, 32, 32);
		
		spritesheet.SetCurrentAnimation(characterAnimation);
	}
	
	void CreateLeTruc()
	{
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/truc");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 64, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 128, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 192, 48, 64);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 48, 0, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 64, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 128, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 192, 48, 64);
		
		spritesheet.CreateAnimation("GrabLeft", 300);
		spritesheet.AddFrame("GrabLeft", 144, 0, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 64, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 128, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 192, 48, 64);
		
		spritesheet.CreateAnimation("GrabRight", 300);
		spritesheet.AddFrame("GrabRight", 96, 0, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 64, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 128, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 192, 48, 64);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 48, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleGrabLeft", 0);
		spritesheet.AddFrame("IdleGrabRight", 144, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleGrabRight", 0);
		spritesheet.AddFrame("IdleGrabLeft", 96, 0, 48, 64);		
		
		spritesheet.SetCurrentAnimation(characterAnimation);
	}
	
	void CreateTurquoiseMage()
	{
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/LeMage");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 32, 48);
		spritesheet.AddFrame("RunLeft", 0, 48, 32, 48);
		spritesheet.AddFrame("RunLeft", 0, 96, 32, 48);
		spritesheet.AddFrame("RunLeft", 0, 144, 32, 48);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 32, 0, 32, 48);
		spritesheet.AddFrame("RunRight", 32, 48, 32, 48);
		spritesheet.AddFrame("RunRight", 32, 96, 32, 48);
		spritesheet.AddFrame("RunRight", 32, 144, 32, 48);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 32, 48);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 32, 0, 32, 48);
		
		spritesheet.SetCurrentAnimation(characterAnimation);
	}
	
	void CreateShipper()
	{
		spritesheet = new Spritesheet(gameObject);		
		spritesheet.Load("Sprites/chipper");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 64, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 128, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 192, 48, 64);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 48, 0, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 64, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 128, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 192, 48, 64);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 48, 0, 48, 64);
		
		spritesheet.SetCurrentAnimation(characterAnimation);
	}
		
	
	// Update is called once per frame
	void Update () {
		spritesheet.Render();
	}
	
	public void SetAnimation(string animation)
	{
		characterAnimation = animation;
	}
}
