using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private Animator enemyAnimator;

    private bool targetDectected = false;

    public int currentHealth = 20;

    public int damage = 10;

    private AudioSource doAttackAudio;

    private AudioSource AtkSwordAudio;

    private AudioSource VictoryAudio;

    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 3)
        {
            doAttackAudio = sources[0];
            AtkSwordAudio = sources[1];
            VictoryAudio = sources[2];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDectected)
        {
            enemyAnimator.SetBool("doMove", true);
        }
        else
        {
            enemyAnimator.SetBool("doMove", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.CompareTag("Player"))
            {
                PlayerManager playerHealth = collision.GetComponent<PlayerManager>();
                if (playerHealth != null)
                {
                    if (playerHealth.hasSword)
                    {
                        TakeDamage(10);
                    }
                    else  
                    {
                        enemyAnimator.SetTrigger("doAttack");
                        doAttackAudio.Play();
                        playerHealth.TakeDamage(damage);
                        Debug.Log(" Player has been hit by Enemy for " + damage + " damage ");
                    }
                }

            }
        }
    }



    public void TakeDamage(int damage)
    {
        AtkSwordAudio.Play();
        currentHealth -= damage;
        Debug.Log(" Enemy took " + damage + " damage from sword ");
        
    
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    
    void Die()
    {
        if (isDead) return;

        isDead = true;

        if (VictoryAudio != null)
        {
            if (VictoryAudio.isPlaying) VictoryAudio.Stop();
            VictoryAudio.Play();
        }

        enemyAnimator.SetTrigger("doDie");
        Debug.Log("Enemy Died!!");
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
