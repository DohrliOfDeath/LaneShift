using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public ParticleSystem ps;
    public int powerUpCounter;
    public Text powerUpText;
    public Text pressSpace;
    public LineDrawer lines;
    
    public bool GodMode = false;
    private void Start()
    {
        ps.Stop();
        pressSpace.GetComponent<Text>().enabled = false;
    }
 
    private void Update()
    {
        powerUpCounter = Convert.ToInt32(powerUpText.text);
        if (Math.Abs(lines.score % 10) < 0.1f && powerUpCounter > 0)
            powerUpText.text =  (--powerUpCounter).ToString();
        
        if  (Input.GetKeyDown (KeyCode.Space) && powerUpCounter == 0)
        {
            Camera.main.gameObject.GetComponent<UIPositionChanger>().CamShake();
            ps.Play();
            foreach (Transform child in GameObject.Find("Obstacles").transform)
                Destroy(child.gameObject);
            powerUpText.text =  (powerUpCounter = 10).ToString();
            pressSpace.GetComponent<Text>().enabled = false;
        } 
        else if (powerUpCounter == 0 && !pressSpace.GetComponent<Text>().enabled)
        {
            Debug.Log("SPACE");
            pressSpace.GetComponent<Text>().enabled = true;
        }
        else
            ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.name == "Obstacles" && !GodMode)
        {             
            Debug.Log("Death");
            SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
        }
    }
}
