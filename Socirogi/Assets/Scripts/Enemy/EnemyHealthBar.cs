using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;  
    
    public void SetHealth(float current, float max)
    {
        healthSlider.value = current / max;  
    }

}