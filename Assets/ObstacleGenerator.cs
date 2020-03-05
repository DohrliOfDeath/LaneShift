using System;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

	public GameObject Obstacle1; //Prefab
	public GameObject Obstacles;
	public float ObstacleSpeed;

	private float xCoord;

    // Start is called before the first frame update
    void Start()
    {
	    xCoord = 0.66f * Camera.main.orthographicSize * Camera.main.aspect; // 1.65f on my phone
    }

    // Update is called once per frame
    void Update()
    {
	    if (Obstacles.transform.childCount <= 10)
		    Generator();
	    Destroyer();
	    
	    int i = 0;
	    foreach (Transform child in Obstacles.transform) //stuff for the different obstacles is here
	    {
		    ColorChanger(child);
		    child.Translate(Vector2.down * (ObstacleSpeed * Time.deltaTime));
		    if (!child.name.Contains("obstacle"))
			    child.name = "obstacle " + i++;
	    }
    }

    void Destroyer()
    {
	    foreach (Transform child in Obstacles.transform)
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
			    xPosition = -xCoord;
			    rotation *= Quaternion.Euler(0, 180f, 0);
				break;
		    case 1: //mid left
			    xPosition = -xCoord / 16.5f;
			    break;
		    case 2: //mid right
			    xPosition = xCoord / 16.5f;
			    rotation *= Quaternion.Euler(0, 180f, 0);
			    break;
		    case 3: //right
			    xPosition = xCoord;
			    break;
	    }
	    bool isAvailable = true;
	    foreach (Transform obstacle in Obstacles.transform)
		    if (Math.Abs(obstacle.position.y - yPosition) < 1.0f && Math.Abs(obstacle.position.x - xPosition) < 0.05f)
			    isAvailable = false;
	    
	    if (isAvailable)
			Instantiate(Obstacle1, new Vector3(xPosition, yPosition, 1.0f), rotation, Obstacles.transform);
    }
    
    void ColorChanger(Transform child)
    {
	    var obstaclePosition = Math.Abs(1 / (Math.Abs(GetComponent<LineDrawer>().linePosition1.y) + 
	                                           Math.Abs(GetComponent<LineDrawer>().linePosition2.y)) * 
	                                      (Math.Abs(GetComponent<LineDrawer>().linePosition2.y) + 
	                                       Math.Abs(child.position.y)) - 0.1f);
	    
	    child.GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<LineDrawer>().color2,
		    GetComponent<LineDrawer>().color1, obstaclePosition);
    }
}
