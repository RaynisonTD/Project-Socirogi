using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;  
    public Camera cam;


    void Start()
    {
        StartCoroutine(FindCameraNextFrame());
    }
//ja
    private IEnumerator FindCameraNextFrame()
    {
        Debug.Log("ja");
        yield return null;
        if (Camera.main != null)
        {
            cam = Camera.main;
        }
    }



    public void SetHealth(float current, float max)
    {
        healthSlider.value = current / max;  
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }

}