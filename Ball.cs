using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D col;

    public Vector3 pos { get { return transform.position; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    //Activates the Rigidbody is the object is moving
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    //Deactivates the Rigidbody is the object is moving
    public void DesactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
}
