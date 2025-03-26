using Player;
using UnityEngine;

public class AttackOrigin : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerController.OnAttackInput += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("button pressed");
            
        }
    }
}
