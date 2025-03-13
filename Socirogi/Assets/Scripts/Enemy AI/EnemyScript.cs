
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxSpeed;
    private float Speed;
    
    private Collider[] hitColliders;
    private RaycastHit[] hits;
    
    public float SightRange;
    public float DetectionRange;
    
    public Rigidbody rb;
    public GameObject target;

    private bool seePlayer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    target = hitCollider.gameObject;
                    seePlayer = true;
                }
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position), out hits, SightRange))
            {
                if (hits.collider.tag != "Player")
                {
                    seePlayer = false;
                }
                else
                {
                    
                    var Heading = target.transform.position - transform.position;
                    var distance = Heading.magnitude;
                    var direction = Heading / distance;
                    
                    Vector3 Move = new Vector3(direction.x * Speed, 0, direction.z * Speed );
                    rb.linearVelocity = Move;
                    transform.forward = Move;
                }
            }
        }
    }
}
