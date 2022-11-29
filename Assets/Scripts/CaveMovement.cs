using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveMovement : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] GameObject hearts;
    [SerializeField] string leavingScene;

    float horizontalInput = 0;
    float verticalInput = 0;
    Rigidbody rb;
    Animator animator;
    SpriteRenderer sr;

    int lives = 3;

    private bool onExit;

    public bool running;

    public string sceneToLoad = "";

    public FMODUnity.EventReference cameraFlashSound;
    


   [SerializeField] float jumpForce;
    bool canJump;
    bool jumpInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        canJump = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
    }

    public bool CheckExit()
    {
        return onExit;
    }

    private void FixedUpdate()
    {
        if (transform.position.x + transform.localScale.x/2 > Camera.main.orthographicSize * Camera.main.aspect || transform.position.x - transform.localScale.x / 2 < -Camera.main.orthographicSize * Camera.main.aspect)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            float newX =  Mathf.Clamp(transform.position.x,-(Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2), Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2);
            transform.position = new Vector3(newX, transform.position.y, 0);
        }
        else
        {
            Vector3 targetVelocity = new Vector3(horizontalInput * velocity, rb.velocity.y, 0);
            rb.velocity = canJump ? Vector3.Lerp(rb.velocity, targetVelocity, 0.3f) : Vector3.Lerp(rb.velocity, targetVelocity, 0.05f);
        }

        
        if (canJump && jumpInput)
        {
            Debug.Log("JUmp");
            rb.AddForce(new Vector3(0, jumpForce, 0));
            canJump = false;
        }

        animator.SetBool(running ? "run" : "walk", rb.velocity.x > 0.02f);
        sr.flipX = rb.velocity.x < 0;
    }

    public void CameraFlash()
    {
        GameManager.FMODPlayStatic(cameraFlashSound, Vector3.zero, Vector3.zero);
    }

    public void Leave()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }


    public void loseLife()
    {
        lives--;

        for(int x = 0; x < hearts.transform.childCount; x++)
        {
            if (hearts.transform.GetChild(x).gameObject.activeSelf)
            {
                hearts.transform.GetChild(x).gameObject.SetActive(false);
                break;
            }
        }

        if(lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Exit"))
        {
            //SceneManager.LoadScene(leavingScene);

            onExit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Exit"))
        {
            //SceneManager.LoadScene(leavingScene);

            onExit = false;
        }
    }
}
