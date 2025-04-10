using UnityEngine;
using UnityEngine.UI;
using Stats;

public class EnemyHealthBarCanvas : MonoBehaviour
{
    [SerializeField] private EnemyStatsComponent statsComponent;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        if (statsComponent == null)
            statsComponent = GetComponentInParent<EnemyStatsComponent>();
    }

    void Update()
    {
        // Update health bar waarde
        healthSlider.value = statsComponent.realTimeStats.health / statsComponent.realTimeStatsMax.health;

        // Fix de positie boven het hoofd
        transform.position = statsComponent.transform.position + offset;

        // Kijk naar camera, maar alleen horizontaal
        Vector3 direction = (mainCam.transform.position - transform.position).normalized;
        direction.y = 0f; // Voorkomt kantelen
        direction.x = 0f;
        direction.z = 0f;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}