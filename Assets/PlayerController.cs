using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentLine;
    private Vector2? MovingTo;
    private float CameraHeight;
    private float CameraWidth;
    private float WidthFactor = 0.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentLine = 1;
        CameraHeight = Camera.main.orthographicSize;
        CameraWidth = CameraHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
        if (SwipeInput.swipedLeft && currentLine > 0 && PlayerIsOnLine(currentLine))
            MoveToLine(--currentLine);
        else if (SwipeInput.swipedRight && currentLine < 2 && PlayerIsOnLine(currentLine))
            MoveToLine(++currentLine);
        

        if (MovingTo != null) // for smooth transition
        {
            Debug.Log("Should be Moving");
            transform.position = Vector2.MoveTowards(transform.position, MovingTo ?? new Vector2(), 0.5f);
            if (MovingTo == transform.position)
                MovingTo = null;
        }
    }

    void MoveToLine(int line)
    {
        Debug.Log("MovedToLine: " + line);
        if (line == 0)
            MovingTo = new Vector2(CameraWidth * -WidthFactor, transform.position.y);
        else if (line == 2)
            MovingTo = new Vector2(CameraWidth * WidthFactor, transform.position.y);
        else
            MovingTo = new Vector2(0, transform.position.y);
    }

    bool PlayerIsOnLine(int line)
    {
        return ((transform.position.x == 0 && line == 1) 
                || (transform.position.x.ToString() == (CameraWidth * WidthFactor).ToString() && line == 2)
                || (transform.position.x.ToString() == (CameraWidth * -WidthFactor).ToString() && line == 0));
    }
}
