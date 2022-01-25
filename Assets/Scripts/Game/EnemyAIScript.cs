using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    private Transform target;
    public float speed = 1f;
    private Animator animator;
    private float facingRight = 1;
    public static int EnemyHealthLevel;
    public TMPro.TextMeshProUGUI enemyTextHealth;
    public GameObject PanelHealthCounter;
	private int HowManyHits = 0;
    public GameObject player;
    public GameObject PanelGameEnd;


    public static bool dead = false;
    //bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        //activate game End panel
        PanelGameEnd.SetActive(false);
        EnemyHealthLevel = 100;
        //Hide HealthLevelCount Holder
        PanelHealthCounter.SetActive(false);
        target = player.transform;

        animator = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
       
        if (!dead)
        {
            //Debug.Log(Vector2.Distance(transform.position, target.position));
            if (Vector2.Distance(transform.position, target.position) <= 18 && Vector2.Distance(transform.position, target.position) > 3.5f)
            {

                PanelHealthCounter.SetActive(true);
                animator.SetFloat("speed", .5f);
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {

                PanelHealthCounter.SetActive(false);
                animator.SetFloat("speed", 0f);
            }


            if (Vector2.Distance(transform.position, target.position) <= 3.5f)
            {
                PanelHealthCounter.SetActive(true);
                animator.SetBool("Attack", true);
            }
            else
            {
                animator.SetBool("Attack", false);
            }

            Vector2 toTarget = (target.position - transform.position);
            if (toTarget.x < 0)
            {
                Flip(0);
            }
            else
            {
                Flip(1);
            }

            HandleHealth();
        }

    }

    private void HandleHealth()
    {
        enemyTextHealth.text = EnemyHealthLevel.ToString();        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // reset number of hits
            if (HowManyHits == 2)
            {
                
                //Player take damange take damage
                if (CharacterBehaviourScript.PlayerHealthLevel > 0)
                {
                    CharacterBehaviourScript.PlayerHealthLevel -= 10;
                }
                else
                {
                    
                    CharacterBehaviourScript.PlayerHealthLevel = 0;
                    
                }
                
                //after reset Hits
                HowManyHits = 0;
                //Debug.Log("Player Damaged");
            }
            else
            {
                HowManyHits += 1;
            }

            if (CharacterBehaviourScript.PlayerHealthLevel == 0)
            {

                CharacterBehaviourScript.dead = true;
                //set Player Score to zero
                collision.gameObject.GetComponent<CharacterBehaviourScript>().playerTextHealth.text = 0.ToString();
                // rotate enemy to the other side to prevent further collisions
                Vector3 theScale = collision.gameObject.transform.localScale;
                theScale.x *= -1;
                collision.gameObject.transform.localScale = theScale;
                //Play dead animation then destroy enemy
                collision.gameObject.GetComponent<Animator>().Play("Dead - Idle - Bat");
                Destroy(collision.gameObject, 2.0f);
                Time.timeScale = 0;
                PanelGameEnd.SetActive(true);

            }
        }
    }

     void Flip(float horizontal)
    {
        if(facingRight != horizontal)
        {
            facingRight = horizontal;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
       
    }
}
