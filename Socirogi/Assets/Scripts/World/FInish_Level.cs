using UnityEngine;

public class Finish_Level : MonoBehaviour
{
    public GameObject canvas;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Finish();
        }
    }

    
    private void Finish()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
}
