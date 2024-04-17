using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveBricks : MonoBehaviour
{
    public float health = 10f; //enemy's base health

    public SpriteRenderer brickSprite;
    public Sprite sDamaged;

    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D actor)
    {
        if (actor.gameObject.CompareTag("Ball"))
        {
            health -= actor.relativeVelocity.magnitude;
            brickSprite.sprite = sDamaged;
            if (health <= 0)
            {
                Die();
            }
        }
        
        
    }

    void Die()
    {
        Destroy(gameObject); //destroys the brick
    }
}
