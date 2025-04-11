using UnityEngine;

public class FInish_Level : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject canvas;
    private bool isShowing = false;
    public AudioClip clip;
    private AudioSource audioSource;
    
    
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
        audioSource = GetComponent<AudioSource>();
    }

    
    private void Finish()
    {
        if (canvas != null)
        {
            isShowing = true;
            canvas.SetActive(true);

            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position); // Wordt alleen hier afgespeeld
            }
        }
    }
}
