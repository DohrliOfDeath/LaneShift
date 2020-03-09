using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GodMode = false;
    private void Start()
    {
    }
 
    private void Update()
    {
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
