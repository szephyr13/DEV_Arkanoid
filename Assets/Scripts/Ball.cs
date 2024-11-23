using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
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
            //1. quits parent
            transform.SetParent(null);
            //2. sets ball as dynamic (physics)
            rb.isKinematic = false;
            //3. applies impulse
            rb.AddForce(new Vector2(1, 1).normalized * 6, ForceMode2D.Impulse);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
        }
    }
}
