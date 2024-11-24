using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject pala;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject youWinText;
    [SerializeField] private GameObject pressSpace;
    [SerializeField] private GameObject pressTab;
    private Rigidbody2D rb;
    private bool canIShoot = true;
    private int lifes = 3;
    private int score = 00;
    private int currentScene;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        pressSpace.SetActive(true);
        pressTab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ball movement start when space pressed
        if (Input.GetKeyDown(KeyCode.Space) && canIShoot)
        {
            pressSpace.SetActive(false);
            pressTab.SetActive(true);
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(currentScene);
        } 
        if (lifes == 0)
        {
            Time.timeScale = 0;
            GameOver();
        }
        if (score >= 42)
        {
            Time.timeScale = 0;
            YouWin();
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
            score++;
            scoreText.text = "Score: " + score;
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
        lifes--;
        lifeText.text = "Lifes: " + lifes;
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(currentScene);
        }
    }
    private void YouWin()
    {
        youWinText.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(currentScene);
        }
    }
}
