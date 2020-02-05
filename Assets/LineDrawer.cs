using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Material LineMaterial;
    public float LineSpeed;
    public Color color1;
    public Color color2;
    public Vector3 linePosition1 ;
    public Vector3 linePosition2;
    
    private float CameraHeight;
    private float CameraWidth;
    private float HeightFactor;
    private float WidthFactor;
    private float LineLength;
    private bool WasLastLineReversed;
    private Color[] allColors = new[] {Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow}; //Color.black, Color.clear, Color.white,
    
    // Start is called before the first frame update
    void Start()
    {
        CameraHeight = Camera.main.orthographicSize;
        CameraWidth = CameraHeight * Camera.main.aspect;
        Debug.Log(CameraHeight + "    " + CameraWidth);
        color1 = Color.blue;
        color2 = Color.yellow;
        
        HeightFactor = 10.0f;
        WidthFactor = 0.7f;
        LineLength = HeightFactor * CameraHeight - CameraHeight;

        DrawLine(new Vector2(0, CameraHeight * HeightFactor), new Vector2(0, HeightFactor * (CameraHeight + LineLength)));
        DrawLine(new Vector2(CameraWidth * WidthFactor, CameraHeight * HeightFactor), new Vector2(CameraWidth * WidthFactor, HeightFactor * (CameraHeight + LineLength)));
        DrawLine(new Vector2(CameraWidth * -WidthFactor, CameraHeight * HeightFactor), new Vector2(CameraWidth * -WidthFactor, HeightFactor * (CameraHeight + LineLength)));
    }

    // Update is called once per frame
    void Update()
    {
        MoveLines();
        
        if (transform.GetChild(0).position.y < CameraHeight + 1 && transform.childCount <= 3)
        {
            color2 = color1;
            System.Random rnd = new System.Random();
            color1 = allColors[rnd.Next(0, allColors.Length)];
            Debug.Log("Color1: " + color1 + "    Color2: " + color2);
            
            int childNumber = transform.childCount;
            for (int i = 0; i < childNumber; i++)
                DrawLine(new Vector2(transform.GetChild(i).position.x, transform.GetChild(i).position.y + LineLength), new Vector2(transform.GetChild(i).position.x, transform.GetChild(i).position.y));
        }

        if (transform.GetChild(0).position.y < -CameraHeight - 1 && transform.childCount >= 3)
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
        lr.material = LineMaterial;
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
            child.Translate(Vector2.down * LineSpeed * Time.deltaTime);
            LineRenderer lr = child.gameObject.GetComponent<LineRenderer>();
            linePosition1 = child.position; //set for colors of obstacles
            linePosition2 = child.position + Vector3.down * LineLength;
            lr.SetPosition(0, linePosition1);
            lr.SetPosition(1, linePosition2);
        }
    }
}
