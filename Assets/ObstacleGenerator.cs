using System;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

	public GameObject Obstacle1;

	public GameObject Obstacles;
	public float ObstacleSpeed;

	private float xCoord;

    // Start is called before the first frame update
    void Start()
    {
	    xCoord = 0.66f * Camera.main.orthographicSize * Camera.main.aspect;
	    // following will be later moved to a automatic generating method: 
	    Instantiate(Obstacle1, new Vector3(1.65f, 1.0f, 1.0f), Quaternion.identity, Obstacles.transform); // right lane
	    /*Instantiate(Obstacle1, new Vector3(-1.65f, 1.0f, 1.0f), Quaternion.identity * Quaternion.Euler(0, 180f, 0), Obstacles.transform); //left lane
	    Instantiate(Obstacle1, new Vector3(-0.1f, 1.0f, 1.0f), Quaternion.identity, Obstacles.transform); //mid lane 1
	    Instantiate(Obstacle1, new Vector3(0.1f, 1.0f, 1.0f), Quaternion.identity * Quaternion.Euler(0, 180f, 0), Obstacles.transform); //mid lane 2
	    */
	    int i = 0;
		foreach (Transform child in Obstacles.transform)
			child.name = "obstacle " + i++;
    }

    // Update is called once per frame
    void Update()
    {
	    if (Obstacles.transform.childCount <= 10)
		    Generator();
	    Destroyer();
	    
	    foreach (Transform child in Obstacles.transform)
	    {
		    ColorChanger(child);
		    child.Translate(Vector2.down * (ObstacleSpeed * Time.deltaTime));
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
