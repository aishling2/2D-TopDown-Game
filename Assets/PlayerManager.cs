using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public float PlayerSpeed = 1;

    private Rigidbody2D rb;

    private Vector2 movement;

    private Animator animator;

    public GameObject SwordPlayer;

    public bool hasSword = false;

    public int maxHealth = 50;

    private int currentHealth;

    private AudioSource BuyAudio;

    private AudioSource DefeatAudio;



    public void EquipSword()
    {
        BuyAudio.Play();
        hasSword = true;
        SwordPlayer.SetActive(true);
        Debug.Log("Sword Equipped");
    }


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();


        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            BuyAudio = sources[0];
            DefeatAudio = sources[1];
        }


        if (SwordPlayer != null)
        {
            SwordPlayer.SetActive(false);
        }

        currentHealth = maxHealth;

    }

    
    void Update()
    {
        // only one animation is active at one time, conditions set to true until triggered 
        animator.SetBool("up", false);
        animator.SetBool("down", false);
        animator.SetBool("left", false);
        animator.SetBool("right", false);
        
        // player idle movement
        if (movement == Vector2.zero)
        {
            animator.SetBool("Idle", true);
        }

        // triggering player movement based on direction
        if (movement.y > 0)
        {
            animator.SetBool("up", true);
        }
        else if (movement.y < 0)
        {
            animator.SetBool("down", true);
        }
        else if (movement.x > 0)
        {
            animator.SetBool("right", true);
        }
        else if (movement.x < 0)
        {
            animator.SetBool("left", true);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * PlayerSpeed * Time.fixedDeltaTime));
    }

    void OnMove(InputValue MovePosistion ){

        movement = MovePosistion.Get<Vector2>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DefeatAudio.Play();
        Debug.Log("Player died!!!");
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
