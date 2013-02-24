using UnityEngine;
using System.Collections;

public class Dash : Power {
	
	public override void StartPower()
	{
		useCooldown = 0.5f;
		powerCooldown = 3.0f;
	}
	
	public override void ActivatePower()
	{
		if( (attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0 && !powerInCooldown))
		{
			Player.Facing face = gameObject.GetComponent<Player>().facing;
			
			if(face == Player.Facing.LEFT)
				gameObject.GetComponent<Player>().PlayAnimation("DashLeft");
					
			else
				gameObject.GetComponent<Player>().PlayAnimation("DashRight");
			
			ProcessDash();
			powerInUse = true;
			powerInCooldown = true;
			cooldown = 0;
		}
	}
	
	public override void ProcessPower()
	{
	}
	
	public override void UseCooldownCallback()
	{
		attachedPlayer.ClampVelocity();
		attachedPlayer.CanMove = true;
		
		Player.Facing face = gameObject.GetComponent<Player>().facing;
		
		if(face == Player.Facing.LEFT)
			gameObject.GetComponent<Player>().PlayAnimation("RunLeft");
				
		else
			gameObject.GetComponent<Player>().PlayAnimation("RunRight");
	}
	
	public override void PowerCooldownCallback()
	{
	}
	
	void ProcessDash()
	{
		AudioClip clip = (AudioClip)Resources.Load("Audio/dash");
		gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
		
		attachedPlayer.CanMove = false;
		
		Vector3 dashDirection = gameObject.rigidbody.velocity;
		dashDirection.Normalize();
		gameObject.rigidbody.AddForce( dashDirection * 1000 );
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.gameObject.CompareTag("Player") && powerInUse)
			{
				if(contact.otherCollider.gameObject.GetComponent<Stunned>() == null)
				{
					Status status = contact.otherCollider.gameObject.AddComponent<Stunned>();
					status.SetEntity(contact.otherCollider.gameObject.GetComponent<Player>());
				}	
					Grab grab = contact.otherCollider.gameObject.GetComponent<Grab>();
					
					if(grab != null)
					{
						grab.Drop();
					}
			}
			
			else if(!contact.otherCollider.gameObject.CompareTag("Ball"))
			{
				gameObject.GetComponent<AudioSource>().Stop();
			}
        }
	}
}
