using UnityEngine;
using System.Collections;

public class GoblinBall : Entity {
	
	float wanderTheta = 0.0f;
	
	public float wanderRadius = 16.0f;
	public float wanderDistance = 60.0f;
	public float change = 0.25f;
	public float maxspeed = 20;
	
	public bool isLocked = false;	
	float lockedTime = 0;
	
	GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		base.Start();
		
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/martian");
		spritesheet.CreateAnimation("Patrick", 300);
		spritesheet.AddFrame("Patrick", 0, 0, 16, 16);
		spritesheet.AddFrame("Patrick", 0, 16, 16, 16);
		spritesheet.AddFrame("Patrick", 0, 32, 16, 16);
		spritesheet.AddFrame("Patrick", 0, 48, 16, 16);
		spritesheet.SetCurrentAnimation("Patrick");

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<GameCamera>().SetNewTarget(this);
		
		gameManager = GameManager.Instance.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocked)
		{
			if(CanMove)
			{
				Wander();
				
				Vector3 position = gameObject.transform.position;
				position.y = 0;
				gameObject.transform.position = position;
			}
		}
		else
		{
			lockedTime += Time.deltaTime;
			
			if(lockedTime > 1)
				isLocked = false;
		}
		
		base.Update();
	}
	
	public void Lock()
	{
		lockedTime = 0;
		isLocked = true;
	}
	
	void Wander()
	{
		wanderTheta += Random.Range(-change, change);
		
		Vector3 circleLocation = gameObject.rigidbody.velocity;
		circleLocation.Normalize();
		circleLocation *= wanderDistance;
		circleLocation += gameObject.transform.position;
		
		Vector3 circleOffset = new Vector3(wanderRadius * Mathf.Cos(wanderTheta), 0, wanderRadius * Mathf.Sin(wanderTheta));
		Vector3 fleeForce = Flee();
		
		Vector3 target = circleLocation + circleOffset + fleeForce;
		gameObject.rigidbody.AddForce(Steer(target));
	}
	
	Vector3 Flee()
	{
		Vector3 fleeForce = new Vector3(0,0,0);
		
		foreach(GameObject entity in gameManager.Entities)
		{
			if(entity.CompareTag("Player"))
			{
				Vector3 distanceVector = gameObject.transform.position - entity.transform.position;
				float distance = distanceVector.magnitude;
				
				if(distance < 25)
				{
					fleeForce += distanceVector;
				}
			}
		}
		
		return fleeForce;
	}
	
	Vector3 Steer(Vector3 target)
	{
		Vector3 steer;
		Vector3 desired = target - gameObject.transform.position;
		
		float distance = desired.magnitude;
		
		if(distance > 0)
		{
			desired.Normalize();
			desired *= maxspeed;
			
			steer = desired - gameObject.rigidbody.velocity;
			
			if(steer.magnitude > maxspeed)
			{
				steer.Normalize();
				steer *= maxspeed;
			}
		}
		
		else
			steer = new Vector3(0,0,0);
		
		return steer;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(CanMove && !isLocked)
		{
			foreach (ContactPoint contact in collision.contacts) {
			
				Vector3 vect = gameObject.transform.position - contact.point;
				vect.Normalize();
				vect *= 25;
	            gameObject.rigidbody.AddForce(vect);
	        }
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if(CanMove && !isLocked)
		{
			foreach (ContactPoint contact in collision.contacts) {
				Vector3 vect = gameObject.transform.position - contact.point;
				vect.Normalize();
							
				gameObject.transform.Translate(vect * 0.1f);
	        }
		}
	}
}
