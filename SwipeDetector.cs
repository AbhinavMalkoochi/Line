using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SwipeDetector : MonoBehaviour
{
    public TextMeshProUGUI  outputText;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private float width;
	private float height;
	public GameObject gb;
	private float playerWidth;
	private float playerHeight;
	private float angle;
    SpriteRenderer sRenderer;

	void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

    }

	void Start(){
		sRenderer = GetComponent<SpriteRenderer>();

		playerWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x;
        playerHeight= sRenderer.sprite.bounds.size.y * transform.lossyScale.y;

	}
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

			startTouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
			currentPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 pos = currentPosition-startTouchPosition;
			pos.x=(pos.x-width)/width;
			pos.y=(pos.y-height)/height;
			
			//Debug.Log("currentPos"+pos.ToString());
			
			//Instantiate(gb, new Vector2(pos.x,pos.y+(playerHeight/2)), upright);
		}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
				Debug.Log("Touch Ended"+endTouchPosition.ToString());
				endTouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				//angle = Vector2.Angle(startTouchPosition,endTouchPosition);
				float yDifference =  endTouchPosition.y-startTouchPosition.y;
				float xDifference = endTouchPosition.x-startTouchPosition.x;
				Debug.Log("YDifference: "+yDifference+" xDifference: "+xDifference);
				angle = Mathf.Atan2((endTouchPosition.y-startTouchPosition.y),(endTouchPosition.x-startTouchPosition.x))*Mathf.Rad2Deg;
				Debug.Log("Angle"+angle);
				if(angle<0){
					angle=0;
				}else if(angle>180){
					angle=180;
				}
				GameObject o = Instantiate(gb, new Vector2(endTouchPosition.x,endTouchPosition.y), Quaternion.identity);
				o.transform.rotation = Quaternion.Euler(Vector3.forward * (angle-90));				//Quaternion upright = Quaternion.Euler(0f,0f,angle);

				//Instantiate(gb, new Vector2(endTouchPosition.x,endTouchPosition.y), upright);

			/*
			endTouchPosition.x=(endTouchPosition.x-width)/width;
			endTouchPosition.y=(endTouchPosition.y-height)/height;
			
			angle = (float)Mathf.Acos(currentPosition.x);
			Debug.Log("Qauternion Angles"+upright.ToString());
			if(currentPosition.y<0){
				angle=-angle;
			}			
			Vector2 Distance = endTouchPosition - startTouchPosition;
			float sign = (endTouchPosition.y < startTouchPosition.y)? -1.0f : 1.0f;
         	float  pos=Vector2.Angle(Vector2.right, Distance) * sign;
			Quaternion upright = Quaternion.Euler(pos*(Mathf.PI/180),0f,0f);
			//Instantiate(gb, new Vector2(endTouchPosition.x,endTouchPosition.y+(playerHeight/2)), upright);	
			Instantiate(gb, new Vector2(.1f,-4f+(playerHeight/2)), upright);
            stopTouch = false;

			Debug.Log("EndPos"+endTouchPosition.ToString());
		*/

        }

        
    }
	}





