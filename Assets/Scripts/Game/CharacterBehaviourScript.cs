using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Player Script//
//controls: 
//A, D, Left, Right to move
//Left Alt to attack
//Space to jump
//Z is to see dead animation

public class CharacterBehaviourScript : MonoBehaviour
{
	//variable for how fast player runs//
	private float speed = 10f;

	private bool facingRight = true;
	private Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private int HowManyHits = 0;

	public static int PlayerHealthLevel;
	public TMPro.TextMeshProUGUI playerTextHealth;


	private float move = 0;

	//variable for how high player jumps//
	[SerializeField]
	private float jumpForce = 3000f;

	public Rigidbody2D rb { get; set; }

	bool dead = false;
	bool attack = false;

	void Start()
	{
		PlayerHealthLevel = 100;
		GetComponent<Rigidbody2D>().freezeRotation = true;
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();

	}




	private void HandleHealth()
    {
		playerTextHealth.text = PlayerHealthLevel.ToString();
	}
	public void OnHitKeyDown()
	{
        if (!dead)
        {
			attack = true;
			GetComponent<EdgeCollider2D>().enabled = true;
			anim.SetBool("Attack", true);
			anim.SetFloat("Speed", 0);
		}

	}
	public void OnHitKeyUp()
	{
		if (!dead)
		{

			GetComponent<EdgeCollider2D>().enabled = false;
			attack = false;
			anim.SetBool("Attack", false);
		}

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Enemy")
		{
			// reset number of hits

			if (HowManyHits == 1)
			{
				//Enemy take damange take damage

				
                if (EnemyAIScript.EnemyHealthLevel > 0)
                {
					EnemyAIScript.EnemyHealthLevel -= 20;
				}
                else
                {
					EnemyAIScript.EnemyHealthLevel = 0;
				}
				//after reset Hits
				HowManyHits = 0;
				//Debug.Log("Enemy Damaged");
			}
			else
			{
				HowManyHits += 1;
			}
		}
	}


    //movement//
    void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		float horizontal = move;
		if (!dead && !attack)
		{
			anim.SetFloat("vSpeed", rb.velocity.y);
			anim.SetFloat("Speed", Mathf.Abs(horizontal));
			rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
		}
		if (horizontal > 0 && !facingRight && !dead && !attack)
		{
			Flip(horizontal);
		}

		else if (horizontal < 0 && facingRight && !dead && !attack)
		{
			Flip(horizontal);
		}

		HandleHealth();
		HandleInput();
	}


	private void TouchMovement()
    {

		float horizontal = move;
		if (!dead && !attack)
		{
			anim.SetFloat("vSpeed", rb.velocity.y);
			anim.SetFloat("Speed", Mathf.Abs(horizontal -1));
			rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
		}
		if (horizontal > 0 && !facingRight && !dead && !attack)
		{
			Flip(horizontal);
		}

		else if (horizontal < 0 && facingRight && !dead && !attack)
		{
			Flip(horizontal);
		}
	}

	//Move With UI Controls
	public void OnMoveLeftButtonClicked()
    {
		move = -1;
	}
	public void OnMoveRightButtonClicked()
	{
		move = 1;
	}
	public void StopMovement()
    {
		move = 0;
	}
	public void OnJumpButtonClicked()
	{
        if (grounded && !dead)
        {
            if (rb.velocity.y == 0)
            {
				anim.SetBool("Ground", false);
				rb.AddForce(new Vector2(0, jumpForce));
			}
			
		}
	}
	//attacking and jumping//
	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt) && !dead)
		{
			attack = true;
			anim.SetBool("Attack", true);
			anim.SetFloat("Speed", 0);

		}
		if (Input.GetKeyUp(KeyCode.LeftAlt))
		{
			attack = false;
			anim.SetBool("Attack", false);
		}

		if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead)
		{
			anim.SetBool("Ground", false);
			rb.AddForce(new Vector2(0, jumpForce));
		}

		//dead animation for testing//
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (!dead)
			{
				anim.SetBool("Dead", true);
				anim.SetFloat("Speed", 0);
				dead = true;
			}
			else
			{
				anim.SetBool("Dead", false);
				dead = false;
			}
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