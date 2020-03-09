using UnityEngine;

/*
 * Swipe Input script for Unity by @fonserbc, free to use wherever
 *
 * Attack to a gameObject, check the static booleans to check if a swipe has been detected this frame
 * Eg: if (SwipeInput.swipedRight) ...
 *
 * 
 */

public class SwipeInput : MonoBehaviour {

	// If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
	public const float MaxSwipeTime = 0.5f; 
	
	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	public const float MinSwipeDistance = 0.17f;

	public static bool SwipedRight = false;
	public static bool SwipedLeft = false;
	public static bool SwipedUp = false;
	public static bool SwipedDown = false;
	
	public bool debugWithArrowKeys = true;

	Vector2 _startPos;
	float _startTime;

	public void Update()
	{
		SwipedRight = false;
		SwipedLeft = false;
		SwipedUp = false;
		SwipedDown = false;

		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				_startPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
				_startTime = Time.time;
			}
			if(t.phase == TouchPhase.Ended)
			{
				if (Time.time - _startTime > MaxSwipeTime) // press too long
					return;

				Vector2 endPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
				Vector2 swipe = new Vector2(endPos.x - _startPos.x, endPos.y - _startPos.y);

				if (swipe.magnitude < MinSwipeDistance) // Too short swipe
					return;

				if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) 
				{ // Horizontal swipe
					if (swipe.x > 0)
						SwipedRight = true;
					else 
						SwipedLeft = true;
				}
				else 
				{ // Vertical swipe
					if (swipe.y > 0) 
						SwipedUp = true;
					else 
						SwipedDown = true;
				}
			}
		}

		if (debugWithArrowKeys) 
		{
			SwipedDown = SwipedDown || Input.GetKeyDown (KeyCode.DownArrow);
			SwipedUp = SwipedUp|| Input.GetKeyDown (KeyCode.UpArrow);
			SwipedRight = SwipedRight || Input.GetKeyDown (KeyCode.RightArrow);
			SwipedLeft = SwipedLeft || Input.GetKeyDown (KeyCode.LeftArrow);
		}
	}
}