using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour
{
	[FormerlySerializedAs("Obstacle1")] public GameObject obstacle1; //Prefab
	[FormerlySerializedAs("Obstacles")] public GameObject obstacles;
	[FormerlySerializedAs("ObstacleSpeed")] public float obstacleSpeed;
	
	public GameObject canvas;
	public GameObject scoreBar;
	private float _xCoord;

    // Start is called before the first frame update
    void Start()
    {
	    canvas = GameObject.Find("Canvas");
	    scoreBar = GameObject.Find("scorebar");
	    _xCoord = 0.66f * Camera.main.orthographicSize * Camera.main.aspect; // 1.65f on my phone
    }

    // Update is called once per frame
    void Update()
    {
	    if (obstacles.transform.childCount <= 10)
		    Generator();
	    Destroyer();
	    
	    int i = 0;
	    foreach (Transform child in obstacles.transform) //stuff for the different obstacles is here
	    {
		    ColorChanger(child);
		    
		    child.Translate(Vector2.down * (obstacleSpeed * Time.deltaTime));
		    if (!child.name.Contains("obstacle"))
			    child.name = "obstacle " + i++;
	    }
	    
	    if (transform.GetChild(0).position.y > 7.0f || transform.GetChild(0).position.y < 3.0f)
	    {
		    var obstaclePosition = Math.Abs(1 / (Math.Abs(GetComponent<LineDrawer>().linePosition1.y) + 
	                                          Math.Abs(GetComponent<LineDrawer>().linePosition2.y)) * 
			    (Math.Abs(GetComponent<LineDrawer>().linePosition2.y) + 
			     Math.Abs(scoreBar.transform.position.y)) - 0.1f);
			Color currentColor = Color.Lerp(GetComponent<LineDrawer>().color2,
				GetComponent<LineDrawer>().color1, obstaclePosition);
			foreach (Transform child in canvas.transform)
				child.gameObject.GetComponent<Text>().color = currentColor;
	    }
    }

    void Destroyer()
    {
	    foreach (Transform child in obstacles.transform)
		    if (-child.position.y - 1 > Camera.main.orthographicSize)
			    Destroy(child.gameObject);
    }

    void Generator()
    {
	    System.Random rnd = new System.Random();
	    float xPosition = rnd.Next(0, 4);
	    float yPosition = rnd.Next(5, 20);
	    Quaternion rotation = Quaternion.identity;
	    switch (xPosition)
	    {
		    case 0: //left
			    xPosition = -_xCoord;
			    rotation *= Quaternion.Euler(0, 180f, 0);
				break;
		    case 1: //mid left
			    xPosition = -_xCoord / 16.5f;
			    break;
		    case 2: //mid right
			    xPosition = _xCoord / 16.5f;
			    rotation *= Quaternion.Euler(0, 180f, 0);
			    break;
		    case 3: //right
			    xPosition = _xCoord;
			    break;
	    }
	    if (IsPosAvailable(xPosition, yPosition))
			Instantiate(obstacle1, new Vector3(xPosition, yPosition, 1.0f), rotation, obstacles.transform);
    }
    
    void ColorChanger(Transform child)
    {
	    var obstaclePosition = Math.Abs(1 / (Math.Abs(GetComponent<LineDrawer>().linePosition1.y) + 
	                                           Math.Abs(GetComponent<LineDrawer>().linePosition2.y)) * 
	                                      (Math.Abs(GetComponent<LineDrawer>().linePosition2.y) + 
	                                       Math.Abs(child.position.y)) - 0.1f);
	    
	    if (transform.GetChild(0).position.y > 7.0f || transform.GetChild(0).position.y < 3.0f)
			child.GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<LineDrawer>().color2,
				GetComponent<LineDrawer>().color1, obstaclePosition);
    }

    bool IsPosAvailable(float xPos, float yPos)
    {
	    bool isAvailable = true;
	    int xNumberAround = 1;
	    foreach (Transform obstacle in obstacles.transform)
	    {
		    if (Math.Abs(obstacle.position.y - yPos) < 1.0f)
			    xNumberAround++;
		    if (Math.Abs(obstacle.position.y - yPos) < 1.0f && Math.Abs(obstacle.position.x - xPos) < 0.05f)
		    {
			    isAvailable = false;
			    break;
		    }
		    if (xNumberAround == 3)
		    {
			    isAvailable = false;
			    break;
		    }
	    }
	    return isAvailable;
    }
}
