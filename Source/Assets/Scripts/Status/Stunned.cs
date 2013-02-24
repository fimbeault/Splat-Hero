using UnityEngine;
using System.Collections;

public class Stunned : Status {
	private static Transform prefab;
	Spritesheet stun = null;
	GameObject stunObject = null;
	
	public override void StartStatusEffect()
	{
		statusTime = 2;
		if(attachedEntity is Player)
		{
			if (prefab == null) {
				prefab = Resources.Load("Stun", typeof(Transform)) as Transform;
			}
			
			attachedEntity.CanMove = false;
			
			stunObject = ((Transform)Instantiate(prefab)).gameObject;
			stunObject.transform.position = gameObject.transform.position + new Vector3(0,2,-10);
			stunObject.transform.rotation = gameObject.transform.rotation;
			stunObject.transform.localScale = gameObject.transform.localScale;
			
			stun = new Spritesheet(stunObject);
			stun.Load("Sprites/stuntEffect");
		
			stun.CreateAnimation("anim", 300);
			stun.AddFrame("anim", 0, 0, 32, 32);
			stun.AddFrame("anim", 0, 32, 32, 32);
			stun.AddFrame("anim", 0, 64, 32, 32);
			stun.AddFrame("anim", 0, 96, 32, 32);
			
			stun.SetCurrentAnimation("anim");
			
			stunObject.transform.parent = gameObject.transform;
		}
	}
	
	public override void ProcessStatusEffect()
	{
		if(attachedEntity is Player) {
			attachedEntity.CanMove = false;
			attachedEntity.gameObject.rigidbody.velocity = new Vector3(0,0,0);
			stun.Render();
		}
	}
	
	public override void EndStatusEffect()
	{
		if(attachedEntity is Player)
			attachedEntity.CanMove = true;
		
		Destroy(stunObject);
		Destroy(this);
	}
}
