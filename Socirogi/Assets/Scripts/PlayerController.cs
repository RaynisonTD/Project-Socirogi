using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if(hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

       float x = Input.GetAxis("Horizontal");
       float y = Input.GetAxis("Vertical");
       Vector3 moveDir = new Vector3(x, 0, y);
       rb.linearVelocity = moveDir * speed;

        if (rb.linearVelocity.magnitude > 0.01f) // Kleine drempel om minuscule bewegingen te negeren
        {
            animator.SetFloat("Speed", 0);
        }else
        {
            animator.SetFloat("Speed", 20f);
        }

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if(x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    } 
}
