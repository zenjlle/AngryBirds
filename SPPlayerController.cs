using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 initPos;
    private Rigidbody2D rigidbody;
    private Vector3 forceAtPlayer;
    public float forceFactor;

    public GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    public int number;

    public GameObject nextBall; //reference a new active ball game object



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trajectoryDots = new GameObject[number];
        this.gameObject.transform.position = new Vector3(-5, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { //click
            startPos = gameObject.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }

        }
        if (Input.GetMouseButton(0))
        { //drag
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            gameObject.transform.position = endPos;
            forceAtPlayer = endPos - startPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        { //leave
            rigidbody.gravityScale = 1;
            rigidbody.velocity = new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor);
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
            StartCoroutine(Release());
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.gravityScale = 0;
            rigidbody.velocity = Vector2.zero;
            gameObject.transform.position = initPos;

        }
    }

    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(endPos.x, endPos.y) + //X0
                new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime + //ut
                0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }

    IEnumerator Release()
    {

        yield return new WaitForSeconds(2f);
        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 0;
            yield return new WaitForSeconds(2f);
            Debug.Log("Retrying");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}