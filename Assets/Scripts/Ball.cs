using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject pala;
    private bool canIShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ball movement start when space pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //0. stop the ball, if moving
            rb.velocity = Vector2.zero;
            //1. quits parent
            transform.SetParent(null);
            //2. sets ball as dynamic (physics)
            rb.isKinematic = false;
            //3. applies impulse
            rb.AddForce(new Vector2(1, 1).normalized * 6, ForceMode2D.Impulse);
            //4. now you cannot shoot
            canIShoot = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            ResetBall();
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void ResetBall()
    {
        //0. stop the ball, if moving
        rb.velocity = Vector2.zero;
        //1 . set as kinematic to supress physics
        rb.isKinematic = true;
        //2. parent the ball
        transform.SetParent(pala.transform);
        //3. set position of the ball
        transform.localPosition = new Vector3(0, 1, 0);
        //4. you can shoot again
        canIShoot = true;
    }
}
