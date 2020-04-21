using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{
    [FormerlySerializedAs("LineMaterial")] public Material lineMaterial;
    [FormerlySerializedAs("LineSpeed")] public float lineSpeed;
    public Color color1;
    public Color color2;
    public Vector3 linePosition1 ;
    public Vector3 linePosition2;
    public float score = 0.0f;
    public Text scoreText;
    public ObstacleGenerator obstacleGenerator;
    
    private float _cameraHeight;
    private float _cameraWidth;
    private float _heightFactor;
    private float _widthFactor;
    private float _lineLength;
    private bool _wasLastLineReversed;
    private Color[] _allColors =
    {
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow
    }; //Color.black, Color.clear, Color.white,
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraHeight = Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Camera.main.aspect;
        Debug.Log("Height: " + _cameraHeight + "    Width: " + _cameraWidth);
        color1 = Color.blue;
        color2 = Color.yellow;
        
        _heightFactor = 10.0f;
        _widthFactor = 0.7f;
        _lineLength = _heightFactor * _cameraHeight - _cameraHeight;
        DrawLine(new Vector2(0, _cameraHeight * _heightFactor), 
            new Vector2(0, _heightFactor * (_cameraHeight + _lineLength)));
        
        DrawLine(new Vector2(_cameraWidth * _widthFactor, _cameraHeight * _heightFactor), 
            new Vector2(_cameraWidth * _widthFactor, _heightFactor * (_cameraHeight + _lineLength)));
        
        DrawLine(new Vector2(_cameraWidth * -_widthFactor, _cameraHeight * _heightFactor),
            new Vector2(_cameraWidth * -_widthFactor, _heightFactor * (_cameraHeight + _lineLength)));
    }

    // Update is called once per frame
    void Update()
    {
        MoveLines();
        
        score += obstacleGenerator.obstacleSpeed/30;
        scoreText.text = Math.Round(score, 0).ToString();
        
        if (transform.GetChild(0).position.y < _cameraHeight + 1 && transform.childCount <= 3)
        {
            color2 = color1;
            System.Random rnd = new System.Random();
            color1 = _allColors[rnd.Next(0, _allColors.Length)];
            Debug.Log("Color1: " + color1 + "    Color2: " + color2);
            int parentSize = transform.childCount;
            for (int i = 0; i < parentSize; i++)
                DrawLine(new Vector2(transform.GetChild(i).position.x, transform.GetChild(i).position.y + _lineLength),
                    transform.GetChild(i).position);
        }

        if (transform.GetChild(0).position.y < -_cameraHeight - 1 && transform.childCount >= 3)
        {
            for (int i = 0; i < 3; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
        
    }
    
    void DrawLine(Vector2 start, Vector2 end)
    {
        GameObject myLine = new GameObject("Line " + gameObject.transform.childCount);
        myLine.transform.SetParent(transform);
        myLine.transform.position = start;
        
        // LineRenderer
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = color1;
        lr.endColor = color2;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void MoveLines()
    {
        foreach (Transform child in transform)
        {
            child.Translate(Vector2.down * lineSpeed * Time.deltaTime);
            LineRenderer lr = child.gameObject.GetComponent<LineRenderer>();
            linePosition1 = child.position; //set for colors of obstacles
            linePosition2 = child.position + Vector3.down * _lineLength;
            lr.SetPosition(0, linePosition1);
            lr.SetPosition(1, linePosition2);
        }
    }
}
