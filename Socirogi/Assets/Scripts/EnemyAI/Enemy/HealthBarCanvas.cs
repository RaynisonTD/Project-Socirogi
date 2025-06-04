using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyAI.Enemy
{
    public class EnemyHealthBarCanvas : MonoBehaviour
    {
        public EnemyBehavior behavior;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);
        private Camera mainCam;
        public TMP_Text NameText;
        public String enemy;

        void Start()
        {
            behavior = GetComponentInParent<EnemyBehavior>();
            mainCam = Camera.main;
        
        
            enemy = transform.root.name;
            string enemySplit = enemy.Split(' ')[0];
            NameText.text = enemySplit; 
        }

        void Update()
        {
            // dit zorgt ervoor dat de hp bar up to date blijft
            healthSlider.value = behavior.health;

            // zet die hp bar boven zijn kop anders zit ie in het lichaam
            transform.position = behavior.transform.position + offset;

            // dit zorgt ervoor dat ie naar de camera kijkt en niet meedraait met de enemy
            Vector3 direction = (mainCam.transform.position - transform.position).normalized;

        
        }
        void LateUpdate()
        {
            //dit zou er ook voor meoten zorgen dat het naar de camera blijft staren
            transform.rotation = mainCam.transform.rotation;
        }
    }
}