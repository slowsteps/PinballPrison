using UnityEngine;
using System.Collections;

public class SpriteSwapper : MonoBehaviour, IControlledByOther {

	public Sprite ActivatedSprite;
	
	void Start ()
	{
	
	}
	
	public void Activate()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = ActivatedSprite;
	}
}
