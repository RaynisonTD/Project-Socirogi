using UnityEngine;

public class DOT : MonoBehaviour
{
    [SerializeField] private BaseEnemy _baseEnemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(DoDamage), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoDamage()
    {
        _baseEnemy.TakeDamage(1);
    }
}
