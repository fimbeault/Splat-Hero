using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
	
	public class DelayedCollision
	{
		Collider collider1;
		Collider collider2;
		
		float ignoreDelay;
		bool ignoreCollision;
		
		float delayCount = 0;
		
		public DelayedCollision(Collider coll1, Collider coll2, float delay, bool ignore)
		{
			collider1 = coll1;
			collider2 = coll2;
			ignoreDelay = delay;
			ignoreCollision = ignore;
		}
		
		public bool Update()
		{
			delayCount += Time.deltaTime;
			
			if(delayCount > ignoreDelay)
			{
				try {
					Physics.IgnoreCollision(collider1, collider2, ignoreCollision);
				} catch (MissingReferenceException) {
				}
				return true;
			}
			
			return false;
		}
	}
	
	List<DelayedCollision> delayedCollision = new List<DelayedCollision>();
	List<DelayedCollision> resolvedCollision = new List<DelayedCollision>();
	
	protected Spritesheet spritesheet;
	public bool CanMove{ get; set; }
	
	// Use this for initialization
	protected void Start () {
		CanMove = true;
	}
	
	// Update is called once per frame
	protected void Update () {
		spritesheet.Render();
		
		foreach(DelayedCollision collision in delayedCollision)
		{
			bool resolved = collision.Update();
			
			if(resolved)
				resolvedCollision.Add(collision);
		}
		
		delayedCollision.RemoveAll(delegate(DelayedCollision collision)
		{
			if(resolvedCollision.Contains(collision))
				return true;
			
			return false;
		});
	}
	
	public void PlayAnimation(string animation)
	{
		spritesheet.SetCurrentAnimation(animation);
	}
	
	public void DelayIgnoreCollision(Collider collider1, Collider collider2, float delay, bool ignore = true)
	{
		delayedCollision.Add(new DelayedCollision(collider1, collider2, delay, ignore));
	}
}
