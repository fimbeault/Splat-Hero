using UnityEngine;
using System.Collections;

public abstract class Status : MonoBehaviour {
	
	public float statusTime = 0;
	protected Entity attachedEntity = null;
	
	bool inCooldown = false;
	float cooldown = 0;
	
	public bool infinite = false;
	
	// Update is called once per frame
	protected void Update () {
	
		if(infinite)
			ProcessStatusEffect();
		
		else if(inCooldown)
		{
			ProcessStatusEffect();
			
			cooldown += Time.deltaTime;
			if(cooldown > statusTime)
			{
				inCooldown = false;
				EndStatusEffect();
			}
		}
	}
	
	public void SetEntity(Entity entity)
	{
		attachedEntity = entity;
		inCooldown = true;
		
		StartStatusEffect();
	}
	
	public abstract void StartStatusEffect();
	public abstract void ProcessStatusEffect();
	public abstract void EndStatusEffect();
}
