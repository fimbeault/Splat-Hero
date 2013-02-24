using UnityEngine;
using System.Collections;

public class SmoothKick : Power {

	public override void StartPower()
	{
		useCooldown = 0.0f;
		powerCooldown = 0.0f;
	}
	
	public override void ActivatePower()
	{
	}
	
	public override void ProcessPower()
	{
	}
	
	public override void UseCooldownCallback()
	{
	}
	
	public override void PowerCooldownCallback()
	{
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.gameObject.CompareTag("Ball"))
			{
				GoblinBall ball = (GoblinBall)contact.otherCollider.gameObject.GetComponent("GoblinBall");
				ball.Lock();
				
				Vector3 vect = contact.otherCollider.transform.position - gameObject.transform.position;
				vect.Normalize();
				vect *= 50;
	            contact.otherCollider.rigidbody.AddForce(vect);
			}
        }
	}
}
