using UnityEngine; 
using System.Collections;

public class Selector : MonoBehaviour {
	UIManager manager;	

	// Use this for initialization
	void Start () {

	}

	public void setManager(UIManager _manager)
	{
		manager = _manager;
	}

	void selectorSnap()
	{
		transform.position.Set (Mathf.FloorToInt (transform.position.x), 
		                       Mathf.FloorToInt (transform.position.y),
		                       Mathf.FloorToInt (transform.position.z));
	}

	public void mouseClick()
	{
		int layerMask = 1 << 9;
		layerMask = ~layerMask;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
//			iTween.MoveTo(this.gameObject,hit.point
//			           		- new Vector3(0f, .5f, 0f)
//			              , .1f);
			iTween.MoveTo(this.gameObject,
			              new Vector3(Mathf.RoundToInt(hit.point.x-.1f),
			            Mathf.RoundToInt(hit.point.y-.1f),
			            Mathf.RoundToInt(hit.point.z-.1f))
			              //- new Vector3(0f, .5f, 0f)
			              , .1f);
		}

	}

	public void touchClick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		
		if(Physics.Raycast(ray,out hit))
			if(hit.collider != null)
		{
			iTween.MoveTo(this.gameObject,
			              new Vector3(Mathf.RoundToInt(hit.point.x-.1f),
			            Mathf.RoundToInt(hit.point.y-.1f),
			            Mathf.RoundToInt(hit.point.z-.1f))
			              //- new Vector3(0f, .5f, 0f)
			              , .1f);
		}

	}

	public Vector3 getLocation()
	{
		return transform.position;
		}

	void Update () {
		if (Input.GetMouseButtonDown(0) && manager.getSelectionMode())
		{
			mouseClick();
		}

		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			
		{
			touchClick();
		}
	}

}
