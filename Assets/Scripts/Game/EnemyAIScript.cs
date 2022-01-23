using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    private Transform target;
    public float speed = 1f;
    private Animator animator;
    private bool facingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = this.GetComponent<Animator>();
        Flip(0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Vector2.Distance(transform.position, target.position));
        if (Vector2.Distance(transform.position,target.position) <= 15 && Vector2.Distance(transform.position, target.position) > 3)
        {
            animator.SetFloat("speed", .5f);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }else{
            animator.SetFloat("speed", 0f);
        }

        
        if(Vector2.Distance(transform.position, target.position) <= 3)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void Flip(float horizontal)
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
