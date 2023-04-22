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
	private float counter=0;
	private GameObject o;
	private float radians;
	private float xPos;
	private float yPos;
	
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
				counter++;
				Debug.Log("Touch Ended"+endTouchPosition.ToString());
				endTouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				float yDifference =  endTouchPosition.y-startTouchPosition.y;
				float xDifference = endTouchPosition.x-startTouchPosition.x;
				Debug.Log("YDifference: "+yDifference+" xDifference: "+xDifference);
				angle = Mathf.Atan2((yDifference),(xDifference))*Mathf.Rad2Deg;
				radians = Mathf.Atan2((yDifference),(xDifference));
				if(angle<0){
					angle=0;
				}else if(angle>180){
					angle=180;
				}
				if(counter==0){
					o =	(GameObject)Instantiate(gb, new Vector2(gameObject.transform.position.x,gameObject.transform.position.y), Quaternion.identity);
					xPos=gameObject.transform.position.x;
					yPos=gameObject.transform.position.y;
					o=new GameObject();

				}else if(counter>0 && angle<90){
					 o = (GameObject)Instantiate(gb, new Vector2(Mathf.Cos(radians)*(playerHeight/2)+xPos,Mathf.Sin(radians)*(playerHeight/2)+yPos), Quaternion.identity);
					 xPos=Mathf.Cos(radians)*(playerHeight/2)+o.transform.position.x;
					 yPos=Mathf.Sin(radians)*(playerHeight/2)+o.transform.position.y;
					 o=new GameObject();
				}
				else if(counter>0 && angle>90){
					 o = (GameObject)Instantiate(gb, new Vector2(Mathf.Cos(radians)*(playerHeight/2)-xPos,Mathf.Sin(radians)*(playerHeight/2)+yPos), Quaternion.identity);
					 xPos=Mathf.Cos(radians)*(playerHeight/2)-o.transform.position.x;
					 yPos=Mathf.Sin(radians)*(playerHeight/2)+o.transform.position.y;
					 o=new GameObject();
				}
				o.transform.rotation = Quaternion.Euler(Vector3.forward * (angle-90));

        }

        
    }
	}





