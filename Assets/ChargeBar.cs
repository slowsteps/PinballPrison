using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {

	
	public float ChargeRate = 0.01f;
	public bool isEnoughEnergy = true;
	private Image img;
	private RectTransform rect;	
	

	void Start () 
	{
		rect = gameObject.GetComponent<RectTransform>();
		img = gameObject.GetComponent<Image>();		
	}
	
	public void DecreaseTimeOutBar()
	{
		if (rect.localScale.y > 0)
		{
			rect.localScale -= new Vector3(0,ChargeRate,0);
		}	
	}
	
	
	public void RechargeTimeOutBar()
	{
		if (rect.localScale.y < 1)
		{
			rect.localScale += new Vector3(0,ChargeRate,0);
		}	
	}
	
	void Update()
	{
		if (rect.localScale.y < 0.3f) 
		{
			img.color = Color.red;
			isEnoughEnergy = false;
		}
		else
		{
			img.color = Color.black;
			isEnoughEnergy = true;
		}
	}
	
	
}
