using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float basicjumpForce = 120f;
    public float jumpForce;
    public float jumpSpeed;
    public float TrampolinJumpForce;
    public Animator animator;
    float moveHorizontal = 0f;
    private int numofJumps;
    private int maxJumps = 1;
    public Text GemCountText;
    private int GemCount;
    public float CherryCount;
    public Text CherryCountText;
    public Text BlueFireCountText;
    private int BlueFireCount;
    private float startPosX;
    private float startPosY;
    Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public GameObject deathMenuUI;
    public GameObject gameOverUI;
    public GameObject gzMenuUI;
    public AudioManager audiomanager;
    public AudioSource gametheme;
    void Start()
    {
        Time.timeScale = 1f;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        GemCount = 0;
        rb = GetComponent<Rigidbody2D>();
        SetCountText();
    }
    void Update()
    {
        //Set variables only for level 3
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_3"))
        {
            jumpForce = 85f;
        }
        if (CherryCount < 0)
        {
            gameOverUI.SetActive(true);
            deathMenuUI.SetActive(false); 
            CherryCountText.text = "0/5";
        }
        //z.axis locked
        transform.rotation = Quaternion.identity;

        //moving horizontal
        moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;
        
        //player face forward and backward
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0){
            characterScale.x = -3; 
        }
        if (Input.GetAxis("Horizontal") > 0){
            characterScale.x = 3;
        }
        transform.localScale = characterScale;

        //Crouch speed, boxcollider disable and crouch animation
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("isCrouching", true);
            transform.rotation = Quaternion.identity;
            GetComponent<BoxCollider2D>().enabled = false;  
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("isCrouching", false);
            GetComponent<BoxCollider2D>().enabled = true; 
        }
        if(isGrounded == true)
        {
            numofJumps = 0; 
        }
        Jump();
    }
    void FixedUpdate()
    {
        //groundcheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        rb.velocity = new Vector2 (speed * moveHorizontal, rb.velocity.y);  
    }
    //Jump method
    void Jump(){
        if (Input.GetButtonDown("Jump") && numofJumps < maxJumps){
            rb.AddForce(new Vector2 (rb.velocity.x, jumpForce)); 
            numofJumps = numofJumps + 1;  
            audiomanager.Play("Jump");
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        //GreenFire trigger
        if(other.gameObject.CompareTag("GreenFire"))
        {
            audiomanager.Play("GreenFire");
            speed = 2.25f;
            jumpForce = 165;  
        }
        //BlueFire trigger
        if(other.gameObject.CompareTag("BlueFire"))
        {
            audiomanager.Play("BlueFire");
            other.gameObject.SetActive(false);
            BlueFireCount = BlueFireCount + 1;
            speed = 1.5f;
            jumpForce = 120;
            SetCountText();  
        }
        //RedFireHit
        if(other.gameObject.CompareTag("RedFire"))
        {
            //sound
            audiomanager.Play("RedFire");
            gametheme.Pause();
            //deathmenu
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
            //all other stuff
            speed = 1.5f;
            CherryCount = CherryCount - 1;
            SetCountText();
            transform.position = new Vector2(startPosX, startPosY);
        }
        //Gem collecting
        if(other.gameObject.CompareTag("Gem"))
        {
            FindObjectOfType<AudioManager>().Play("Gem");
            other.gameObject.SetActive(false);
            GemCount = GemCount + 1;
            SetCountText();
        }
        //Cherry collecting
        if(other.gameObject.CompareTag("Cherry"))
        {
            other.gameObject.SetActive(false);
            CherryCount = CherryCount + 1;
            SetCountText();
        }
        //Door triggerEnter
        if(other.gameObject.CompareTag("Door"))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_3"))
            {
                if(GemCount == 100 && BlueFireCount == 15)
                {
                    Time.timeScale = 0f;
                    gzMenuUI.SetActive(true);
                    audiomanager.Play("WinningSong");
                }
            }
            else if(GemCount == 100)
            {
                Time.timeScale = 0f;
                gzMenuUI.SetActive(true);
                audiomanager.Play("WinningSong");
            }      
        }
        //Door trigger for level 2
        if(other.gameObject.CompareTag("Finish"))
        {
            if(GemCount == 100 && BlueFireCount == 20)
            {
                Time.timeScale = 0f;
                gzMenuUI.SetActive(true);
                audiomanager.Play("WinningSong");
            }  
        }
        //level 1 teleports
        if(other.gameObject.CompareTag("Teleportin")){
            transform.position = new Vector3(-1.47f, 4.59f, 0f);
        }
        if(other.gameObject.CompareTag("TeleportOut")){
            transform.position = new Vector2(startPosX, startPosY);
        }
        //level 2 teleports
        if(other.gameObject.CompareTag("TeleportRightUp")){
            transform.position = new Vector3(26.50f, 3.8f, 0f);
        }
        if(other.gameObject.CompareTag("TeleportRightDown")){
            transform.position = new Vector3(26.5f, -7.3f, 0f);
        }
        if(other.gameObject.CompareTag("TeleportLeftUp")){
            transform.position = new Vector3(-8.55f, 3.50f, 0f);
        }
        if(other.gameObject.CompareTag("TeleportLeftDown")){
            transform.position = new Vector3(-8.55f, -7.15f, 0f);
        }
    }
   
    //CountText for gem, cherry and bluefire
    void SetCountText()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_3"))
        {
            GemCountText.text = GemCount.ToString() + "/100";
            CherryCountText.text = CherryCount.ToString() + "/5";
            BlueFireCountText.text = BlueFireCount.ToString() + "/15";
        }
        else
        {
            GemCountText.text = GemCount.ToString() + "/100";
            CherryCountText.text = CherryCount.ToString() + "/5";
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2"))
        {
            BlueFireCountText.text = BlueFireCount.ToString() + "/20";
        }   
    } 
    void OnCollisionEnter2D(Collision2D other){
         
        //Spike collision
        if(other.gameObject.CompareTag("Spike"))
        {
            //sound
            audiomanager.Play("PlayerDeath");
            gametheme.Pause();
            //deathmenu
            Time.timeScale = 0f;
            deathMenuUI.SetActive(true);
            //all other stuff
            speed = 1.5f;
            CherryCount = CherryCount - 1;
            SetCountText();
            transform.position = new Vector2(startPosX, startPosY);       
        }
            //trampolin collision - kinda tricky mechanic
            if(other.gameObject.CompareTag("Trampolin")){
            rb.AddForce(new Vector2 (rb.velocity.x, TrampolinJumpForce));
            transform.rotation = Quaternion.identity;
            audiomanager.Play("Shroom");
            isGrounded = false;  
        }
      }
   }

