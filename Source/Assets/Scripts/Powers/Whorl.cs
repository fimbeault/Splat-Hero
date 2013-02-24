using UnityEngine;
using System.Collections;

public class Whorl : Power {
	
	public override void StartPower()
	{
		useCooldown = 0.5f;
		powerCooldown = 1.5f;
	}
	
	public override void ActivatePower()
	{
		if( !powerInCooldown && (
			(attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0)))
		{
			if(attachedPlayer.CanMove)
			{
				ProcessWhorl();
				powerInUse = true;
				powerInCooldown = true;
				
				Player.Facing face = gameObject.GetComponent<Player>().facing;
		
				if(face == Player.Facing.LEFT)
					gameObject.GetComponent<Player>().PlayAnimation("SpinLeft");
						
				else
					gameObject.GetComponent<Player>().PlayAnimation("SpinRight");
			}
		}
	}

	public void ProcessWhorl ()
	{
		AudioClip clip = (AudioClip)Resources.Load("Audio/swirl");
		gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
		
		attachedPlayer.CanMove = false;
		attachedPlayer.rigidbody.velocity = new Vector3(0, 0, 0);
		
		Collider[] collisions = Physics.OverlapSphere(gameObject.transform.position, 20);
		
		foreach (Collider collision in collisions)
		{
			if(collision.collider == gameObject.collider)
				continue;
			
			if(collision.collider.CompareTag("Ball"))
			{
				GoblinBall ball = (GoblinBall)collision.collider.gameObject.GetComponent("GoblinBall");
				ball.Lock();
				
				Vector3 vect = collision.collider.transform.position - gameObject.transform.position;
				vect.Normalize();
				vect *= 75;
	            collision.collider.rigidbody.AddForce(vect);
			}
			
			else if(collision.collider.CompareTag("Player"))
			{
				if(collision.collider.gameObject.GetComponent<Stunned>() == null)
				{
					Status grabbed = collision.collider.gameObject.AddComponent<Stunned>();
					grabbed.SetEntity(collision.collider.GetComponent<Player>());
					grabbed.statusTime = 0.5f;
				}
			}
		}
		
	}
	
	public override void ProcessPower(){}
	
	public override void UseCooldownCallback()
	{
		attachedPlayer.CanMove = true;
		
		Player.Facing face = gameObject.GetComponent<Player>().facing;
		
		if(face == Player.Facing.LEFT)
			gameObject.GetComponent<Player>().PlayAnimation("RunLeft");
				
		else
			gameObject.GetComponent<Player>().PlayAnimation("RunRight");
	}
	
	public override void PowerCooldownCallback(){ResetPower();}
}