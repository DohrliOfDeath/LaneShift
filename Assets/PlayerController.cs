using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _currentLine;
    private Vector2? _movingTo;
    private float _cameraHeight;
    private float _cameraWidth;
    private float _widthFactor = 0.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentLine = 1;
        _cameraHeight = Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
        if (SwipeInput.SwipedLeft && _currentLine > 0 && PlayerIsOnLine(_currentLine))
            MoveToLine(--_currentLine);
        else if (SwipeInput.SwipedRight && _currentLine < 2 && PlayerIsOnLine(_currentLine))
            MoveToLine(++_currentLine);
        

        if (_movingTo != null) // for smooth transition
        {
            transform.position = Vector2.MoveTowards(transform.position, _movingTo ?? new Vector2(), 0.5f);
            if (_movingTo == transform.position)
                _movingTo = null;
        }
    }

    void MoveToLine(int line)
    {
        Debug.Log("MovedToLine: " + line);
        if (line == 0)
            _movingTo = new Vector2(_cameraWidth * -_widthFactor, transform.position.y);
        else if (line == 2)
            _movingTo = new Vector2(_cameraWidth * _widthFactor, transform.position.y);
        else
            _movingTo = new Vector2(0, transform.position.y);
    }

    bool PlayerIsOnLine(int line)
    {
        float xPos = transform.position.x; // for efficiency reasons
        return (Math.Abs(xPos) < 0.1f && line == 1
                || Math.Abs(xPos - (_cameraWidth * _widthFactor)) < 0.1f && line == 2
                || Math.Abs(xPos - (_cameraWidth * -_widthFactor)) < 0.1f && line == 0);
    }
}
