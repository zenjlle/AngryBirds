using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Rigidbody2D rb; //the current player's(ball) rigid body
    public Rigidbody2D hook; //rigid body attached on the hook game object

    public float releaseTime = 0.15f; //release time upon mouse up
    public float maxDragDistance; //max drag distance from the hook

    public bool isPressed = false; //checks of the ball game object os presses or interacted

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
    }
}
