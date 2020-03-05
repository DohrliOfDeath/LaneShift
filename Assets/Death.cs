using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Obstacles;
    private void Start()
    {
    }
 
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.parent.name == "Obstacles")
            Debug.Log("Death");
    }
}
