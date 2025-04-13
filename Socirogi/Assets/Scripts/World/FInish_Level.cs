using UnityEngine;

public class FInish_Level : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject canvas;
    private bool isShowing = false;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Finish();
        }
    }
    
    void Start()
    {
        
    }

    
    private void Finish()
    {
        if (canvas != null)
        {
            isShowing = true;
            canvas.SetActive(true);
        }
    }
}
