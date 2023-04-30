//minimum of 15 degree separation between line and previous line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraPlayerMovement : MonoBehaviour
{

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
	private Vector3 offset = new Vector3(0f,0f,-10f);
	private float smoothTime = 0.25f;
	private Vector3 velocity;
	public TextMeshProUGUI counterText;
	[SerializeField] private Transform target;
	void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

    }

	void Start(){
		sRenderer = gb.GetComponent<SpriteRenderer>();

		playerWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x;
        playerHeight= sRenderer.sprite.bounds.size.y * transform.lossyScale.y;
		xPos= gb.transform.position.x;
		yPos= gb.transform.position.y;
 		velocity = Vector3.up*Time.deltaTime*.25f;
		counterText.text = "Points: 0";
		counter++;

	}
    void Update()
    {

		Vector3 targetPostion = gb.transform.position+offset;
		transform.position = Vector3.SmoothDamp(transform.position,targetPostion,ref velocity, smoothTime);
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

				counterText.text = "Points: "+counter.ToString();
				endTouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				float yDifference =  endTouchPosition.y-startTouchPosition.y;
				float xDifference = endTouchPosition.x-startTouchPosition.x;
				radians = Mathf.Atan2(yDifference,xDifference);
				angle = radians*Mathf.Rad2Deg;
				//set xPos to the new x position of the player based on angle
				//set yPos to the new y position of the player based on angle

				Debug.Log("counter"+counter);
				Debug.Log("Xpos"+xPos+" Ypos"+yPos);
				xPos+=(Mathf.Cos(radians)*(playerHeight/2));
				yPos+=(Mathf.Sin(radians)*(playerHeight));

				gb = (GameObject)Instantiate(gb, new Vector2(xPos,yPos), Quaternion.identity);

				gb.transform.rotation = Quaternion.Euler(Vector3.forward * (angle-90));
				counter++;

        }

        
    }
	}





