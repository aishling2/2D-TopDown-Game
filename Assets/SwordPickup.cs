using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public Sprite BSIdleSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.EquipSword();
        }
        
         Destroy(gameObject);
        }
    }
}


