using UnityEngine;
using System.Collections;

abstract public class Power : MonoBehaviour {
	
	public bool powerInUse {get; protected set;}
	public bool powerInCooldown {get; protected set;}
	
	public float useCooldown {get; protected set;}
	public float powerCooldown {get; protected set;}
	public float cooldown {get; protected set;}
	
	protected Player attachedPlayer = null;
	
	// Update is called once per frame
	void Update () 
    {
		if(powerInCooldown)
		{
			cooldown += Time.deltaTime;
			
			if(powerInUse && cooldown > useCooldown)
			{
				powerInUse = false;
				UseCooldownCallback();
			}
			
			if(powerInCooldown && cooldown > powerCooldown)
			{
				powerInCooldown = false;
				PowerCooldownCallback();
			}
			
			ProcessPower();
		}
	
		else
		{
			ActivatePower();
		}
	}
	
	public void ResetPower()
	{
		powerInUse = false;
		powerInCooldown = false;
		cooldown = 0;
	}
	
	public void SetPlayer(Player player)
	{
		attachedPlayer = player;		
		StartPower();
	}
	
	public abstract void StartPower();
	public abstract void ActivatePower();
	public abstract void ProcessPower();
	public abstract void UseCooldownCallback();
	public abstract void PowerCooldownCallback();
}

public class NullPower : Power {
	
	public override void StartPower()
	{
		useCooldown = 0;
		powerCooldown = 1.0f;
	}
	public override void ActivatePower(){}
	public override void ProcessPower(){}
	public override void UseCooldownCallback(){}
	public override void PowerCooldownCallback(){}
}