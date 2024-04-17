using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    public float health = 4f; //enemy's base health
    public static int EnemiesAlive = 0; //variable that store and count how many enemies are present

    void Start()
    {
        EnemiesAlive++; //counts how many game objects are present in the scene
    }

    private void OnCollisionEnter2D(Collision2D actor)
    {
        // the enemy will die if the enemy is hit with a velocity.magnitude greater than 4
        if (actor.relativeVelocity.magnitude > health)
        {
            Die();
        }
    }

    void Die()
    {
        EnemiesAlive--; //subtract a defeated enemy from the total count

        //this conditon will check if there are enemies left
        if(EnemiesAlive <= 0)
        {
            SceneManager.LoadScene("Lvl" + (gameManager.levelAt + 1));
        }
        Destroy(gameObject); //destroys the game object where the enemy script is attached
    }

}
