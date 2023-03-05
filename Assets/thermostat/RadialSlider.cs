using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	bool isPointerDown=false;

	public bool gameWon = false;
	public bool isOnTarget = false;

	// Max angle allowable for the dial
	const float maxfill = 0.834f;


	// Called when the pointer enters our GUI component.
	// Start tracking the mouse
	public void OnPointerEnter( PointerEventData eventData )
	{
		StartCoroutine( "TrackPointer" );            
	}
	
	// Called when the pointer exits our GUI component.
	// Stop tracking the mouse
	public void OnPointerExit( PointerEventData eventData )
	{
		StopCoroutine( "TrackPointer" );
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown= true;
		//Debug.Log("mousedown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown= false;
		//Debug.Log("mousedown");
	}

	// mainloop
	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();

		var text = GetComponentInChildren<TextMeshProUGUI>();
		
		if( ray != null && input != null )
		{
			while( !gameWon )
			{                    

				if (isPointerDown)
				{

					Vector2 localPos; // Mouse position  
					RectTransformUtility.ScreenPointToLocalPointInRectangle( transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos );
						
					// local pos is the mouse position.
					// the first 180f is to convert radian to degrees, while the second number is an angle offset
					float angle = (Mathf.Atan2(-localPos.y, localPos.x)*180f/Mathf.PI+270f)/360f % 1;

					if (angle <= maxfill)
					{

						GetComponent<Image>().fillAmount = angle;

						GetComponent<Image>().color = Color.Lerp(Color.blue, Color.red, angle / maxfill);

						int display_temp = (int)(angle / maxfill * 50f + 50f);

						text.SetText(display_temp.ToString());

						//Debug.Log(localPos+" : "+angle);
					}
				}
				else if (isOnTarget) {
					gameWon = true;
                }

				yield return 0;
			}        
		}
		else
			UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" );        
	}

}
