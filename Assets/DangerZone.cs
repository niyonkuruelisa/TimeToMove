using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    public GameObject PanelGameEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collided"); 
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
