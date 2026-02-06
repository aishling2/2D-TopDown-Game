using UnityEngine;

public class enemyArgoZone : MonoBehaviour
{

    public GameObject target;

    public GameObject enemy;

    public float movementSpeed;

    private Rigidbody2D enemyRigidBody;
    private Vector2 calculatedDirection;
    private bool targetDectected = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (targetDectected && enemyRigidBody != null)
        {
            calculatedDirection = (target.transform.position - enemy.transform.position).normalized;

            enemyRigidBody.linearVelocity = calculatedDirection * movementSpeed;
        }
        else if (enemyRigidBody != null )
        {
            enemyRigidBody.linearVelocity = new Vector2(0, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals(target.name))
        {
            //Debug.Log("Enter: " + collision.name);
            targetDectected = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name.Equals(target.name))
        {

            //Debug.Log("Exit: " + collision.name);
            targetDectected = false;
        }
    }

}
