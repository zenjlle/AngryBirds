using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        trajectory.Hide();
    }

    Camera cam;
    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    public float maxDragDistance;
    public bool isDragDone;


    private void Start()
    {
        cam = Camera.main;
        ball.DesactivateRb();

        trajectory.Show();
    }

    private void Update()
    {

        if (isDragDone ==  false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                OnDragStart();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDragEnd();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
        
    }

    //deactivate the rigidbody and record where the mouse starts holding
    void OnDragStart()
    {
        isDragDone = false;
        ball.DesactivateRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    //while dragging, constantly update the end point in reference to the start point, while calculating the distance
    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        distance = Mathf.Min(distance, maxDragDistance);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);

        trajectory.UpdateDots(ball.pos, force);
    }

    //once the player lets go of the mouse, push the ball
    void OnDragEnd()
    {
        ball.ActivateRb();

        ball.Push(force);

        trajectory.Hide();

        isDragDone = true;

        FindObjectOfType<SpringJoint2D>().enabled = false;
    }
}
