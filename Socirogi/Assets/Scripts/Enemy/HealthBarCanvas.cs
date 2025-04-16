using UnityEngine;
using UnityEngine.UI;
using Stats;
using TMPro;
using System;
public class EnemyHealthBarCanvas : MonoBehaviour
{
    [SerializeField] private EnemyStatsComponent statsComponent;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);
    private Camera mainCam;
    public TMP_Text NameText;
    public String enemy;

    void Start()
    {
        mainCam = Camera.main;

        if (statsComponent == null)
            statsComponent = GetComponentInParent<EnemyStatsComponent>();
        
        enemy = transform.root.name;
        string enemySplit = enemy.Split(' ')[0];
        NameText.text = enemySplit; 
    }

    void Update()
    {
        // dit zorgt ervoor dat de hp bar up to date blijft
        healthSlider.value = statsComponent.realTimeStats.health / statsComponent.realTimeStatsMax.health;

        // zet die hp bar boven zijn kop anders zit ie in het lichaam
        transform.position = statsComponent.transform.position + offset;

        // dit zorgt ervoor dat ie naar de camera kijkt en niet meedraait met de enemy
        Vector3 direction = (mainCam.transform.position - transform.position).normalized;

        
    }
    void LateUpdate()
    {
        //dit zou er ook voor meoten zorgen dat het naar de camera blijft staren
        transform.rotation = mainCam.transform.rotation;
    }
}