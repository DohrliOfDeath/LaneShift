using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentLine;
    
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
        {
            MoveToLine(currentLine - 1);
            currentLine--;
        }
        else if (SwipeInput.swipedRight && currentLine < 2 && PlayerIsOnLine(currentLine))
        {
            MoveToLine(currentLine + 1);
            currentLine++;
        }
    }

    void MoveToLine(int line)
    {
        Debug.Log("MovedToLine: " + line);
        if (line == 0)
            transform.position = new Vector2(CameraWidth * -WidthFactor, transform.position.y);
        else if (line == 2)
            transform.position = new Vector2(CameraWidth * WidthFactor, transform.position.y);
        else
            transform.position = new Vector2(0, transform.position.y);
    }

    bool PlayerIsOnLine(int line)
    {
        return ((transform.position.x == 0 && line == 1) 
                || (transform.position.x.ToString() == (CameraWidth * WidthFactor).ToString() && line == 2)
                || (transform.position.x.ToString() == (CameraWidth * -WidthFactor).ToString() && line == 0));
    }
}
