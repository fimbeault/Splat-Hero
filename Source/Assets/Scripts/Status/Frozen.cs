using UnityEngine;
using System.Collections;

public class Frozen : Status {
	
	private Transform prefab;
	Texture2D ice = null;
	GameObject iceObject = null;
	
	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("Ball"))
		{
			ice = (Texture2D)Resources.Load("Sprites/SmallIceBlock");
			statusTime = 0.5f;
		}
		
		else
		{
			ice = (Texture2D)Resources.Load("Sprites/BigIceBlock");
			statusTime = 3.0f;
		}
		
		if (prefab == null) {
			prefab = Resources.Load("Stun", typeof(Transform)) as Transform;
		}
		
		iceObject = ((Transform)Instantiate(prefab)).gameObject;
		iceObject.transform.position = gameObject.transform.position + new Vector3(0,2,0);
		iceObject.transform.rotation = gameObject.transform.rotation;
		iceObject.transform.localScale = gameObject.transform.localScale;
		
		iceObject.renderer.material.mainTexture = ice;
		iceObject.renderer.material.shader = Shader.Find("Unlit/Transparent");
		
		iceObject.transform.parent = gameObject.transform;
	}
	
	public override void StartStatusEffect()
	{
		attachedEntity.CanMove = false;
	}
	
	public override void ProcessStatusEffect()
	{
		attachedEntity.gameObject.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	public override void EndStatusEffect()
	{
		attachedEntity.CanMove = true;
		Destroy(iceObject);
		Destroy(this);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(gameObject.CompareTag("Ball"))
		{
			foreach (ContactPoint contact in collision.contacts) {
				if(contact.otherCollider.gameObject.CompareTag("Player") 
					&& contact.otherCollider.gameObject.GetComponent<Player>().character != Player.Character.TURQUOISE_MAGE)
				{
					if(contact.otherCollider.gameObject.GetComponent<Frozen>() == null)
					{
						Status status = contact.otherCollider.gameObject.AddComponent<Frozen>();
						status.SetEntity(contact.otherCollider.gameObject.GetComponent<Player>());
					}
				}
	        }
		}
	}
}
