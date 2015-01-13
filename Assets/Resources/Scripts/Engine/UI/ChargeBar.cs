using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {

	
	public float ChargeRate = 0.01f;
	public bool isDetectingTap = true;
	private Image img;
	private RectTransform rect;	
	

	void Start () 
	{
		rect = gameObject.GetComponent<RectTransform>();
		img = gameObject.GetComponent<Image>();	
		gameObject.SetActive(Ball.instance.isFreeTap);	
	}
	
	
	public void DecreaseBar()
	{
		
		if (rect.localScale.y > 0)
		{
			rect.localScale -= new Vector3(0,ChargeRate,0);
		}	
	}
	
	
	public void RechargeBar()
	{
		//print ("RechargeTimeOutBar()");
		if (rect.localScale.y < 1)
		{
			rect.localScale += new Vector3(0,ChargeRate,0);
		}	
	}
	
	void Update()
	{
		
		if (isDetectingTap && Input.GetMouseButton(0)) DecreaseBar();
		else RechargeBar();
	
		if (rect.localScale.y < 0f) 
		{
			EventManager.fireEvent(EventManager.EVENT_CHARGE_DEPLETED);
			img.color = Color.red;
			isDetectingTap = false;
		}
		else
		{
			img.color = Color.black;
		}
		
		if (Input.GetMouseButtonUp(0)) isDetectingTap = true;
		
	}
	
	
	
	
}
