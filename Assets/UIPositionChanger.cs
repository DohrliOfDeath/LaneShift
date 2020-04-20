using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class UIPositionChanger : MonoBehaviour
{
    [FormerlySerializedAs("Scorebar")] public GameObject scorebar;
    private float _xMod;
    private float _yMod;
    public Animator camAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        camAnim = gameObject.GetComponent<Animator>();
        _xMod = Camera.main.orthographicSize * Camera.main.aspect;
        _yMod = Camera.main.orthographicSize; 
        scorebar.transform.position = new Vector2(0.0f, _yMod * 0.94f); //4.7 on my phone
    }
    public void CamShake() => camAnim.SetTrigger("isShake");
}
