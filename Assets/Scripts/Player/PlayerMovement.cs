using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    
    public CharacterController2D controller;

    public LayerMask ladder;

    public ParticleSystem dust;

    public HeartSystem playerHealth;

    public GameManager gameManager;

    public GameObject gameOver;

    
    
    

    public AudioClip _jumpAudio;
    public AudioClip _hurtAudio;
    public AudioClip _EnemyDeath;
    private AudioSource _sound;

    public float runSpeed = 40f;
    public float climbSpeed = 10f;
    public float jumpEnemyForce = 10f;
    public float hurtForce = 40f;
  
    private Rigidbody2D body;
    private Collider2D collider;
    // biến để xác định số lượng các item thu thập được
    
    private int cherries = 0;
    
    private int gems = 0;
    [SerializeField]
    private Text cherryText;
    [SerializeField]
    private Text gemText;
    [SerializeField]
    private Text pointText;

    // các biến hỗ trợ việc di chuyển và trạng thái của nhân vật
    float horizontalMove = 0f;

    float verticalMove = 0f;

    bool IsClimbing = false;
    bool onLadder = false;

    bool jump = false;

    bool crouch = false;

    bool grounded = false;

    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool falling = false;



    // 2 biến bool dùng để xác định điều kiện di chuyển
    private bool moveUpLadder=false;
    private bool moveDownLadder=false;// Di chuyển climb trên thang
    private bool moveLeft;
    private bool moveRight;



    // Project run over here===================================================
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        _sound = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {

        playerHealth.health = 3;
    }
    void Update()
    {
        PlayerMove();
        PlayerMoveWithButton();
        PlayerDead();
        MatchPoint();
        State();
        Climb();
        
    }
    // Phần xử lý di chuyển bằng Button
    //=======================================================================================
    public void PlayerMoveWithButton()
    {
        if (moveLeft == true )
        {
            horizontalMove = runSpeed * (-1f); // x axis = -1 để di chuyển về bên trái
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); // xét biến float trong chuyển động animation nên luôn >0    
            if (grounded == true)
            {
                animator.SetBool("Grounded", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("OnAir", false);
            }
            else if (grounded == false)
            {
                animator.SetBool("Grounded", false);
            }
        }
        else if (moveRight == true )
        {
            horizontalMove = runSpeed; // x axis = 1 để di chuyển về bên phải
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (grounded == true)
            {
                animator.SetBool("Grounded", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("OnAir", false);
            }
            else if (grounded == false)
            {
                animator.SetBool("Grounded", false);
            }
        }
        else if (crouch == true && grounded == true )
        {
            animator.SetBool("IsCrouching", true);
        }
        else if(moveUpLadder==true)
        {
            verticalMove = climbSpeed;// > 0 để di chuyển về phía trên
            IsClimbing = true;
           // if (onLadder == false)
            //{
                animator.SetFloat("ClimbSpeed", Mathf.Abs(climbSpeed));
                animator.SetBool("IsClimbing", true);
            //}
        }
        else if(moveDownLadder==true)
        {
            verticalMove = climbSpeed * (-1f);// <0 để di chuyển về phía dưới
            IsClimbing = true;
           // if (onLadder == false)
            //{
                animator.SetFloat("ClimbSpeed", Mathf.Abs(climbSpeed));
                animator.SetBool("IsClimbing", true);
            //}
        }
    }
    public void PlayerMoveJump()
    {
        if (isJumping == false)
        {
            if (_jumpAudio != null)
            {
                _sound.PlayOneShot(_jumpAudio);
            }
        }
        isJumping = true;
        jump = true;
        grounded = false;
        animator.SetBool("Grounded", false);
        animator.SetBool("IsJumping", true);
        animator.SetBool("OnAir", true);
    }
    public void PlayerMoveCrouch()
    {
        crouch = true;
        grounded = true;

    }

    public void PlayerMoveUpOnLadder()
    {
        //animator.SetBool("IsClimbing", true);
        moveUpLadder = true;
        moveDownLadder = false;
    }
    public void PlayerMoveDownOnLadder()
    {
        //animator.SetBool("IsClimbing", true);
        moveDownLadder = true;
        moveUpLadder = false;
    }
    public void StopClimb()
    {
        body.velocity = new Vector2(0f,0f);
        moveDownLadder = false;
        moveUpLadder = false;
        //IsClimbing = false;
        animator.SetFloat("ClimbSpeed", Mathf.Abs(0));
        animator.Rebind();
    }
    public void StopCrouch()
    {
        crouch = false;

        animator.SetBool("IsCrouching", false);
    }
    public void StopMoving()
    {
        
        moveLeft = false;
        moveRight = false;
    }
    public void PlayerMoveLeft()
    {
        this.moveLeft = true;
        this.moveRight = false;
    }
    public void PlayerMoveRight()
    {
        this.moveRight = true;
        this.moveLeft = false;
    }
    // Xử lý hiệu ứng
    //==========================================================================


    // Tính điểm

    private void MatchPoint()
    {
        pointText.text = ((cherries + gems) * 50).ToString();
    }
    private void PlayerDead()
    {
        if(playerHealth.health<1)
        {
            gameOver.SetActive(true);
            
            this.gameObject.SetActive(false);
        }
    }
    
    private void CreatDustCloud()//  Hiệu ứng tạo khói
    {
        dust.Play();
        
    }
    //==========================================================================
    // Xử lý trạng thái
    private void State()
    {
        if (body.velocity.y < 0f)
            falling = true;
        else 
            falling = false;
    }

    //==========================================================================
    // Hàm xử lý di chuyển climb
    public void Climb()
    {
        if (collider.IsTouchingLayers(ladder))
        {
            onLadder = true;
            if (IsClimbing == true)
            {
                body.gravityScale = 0f;
            }
            if (moveDownLadder == true)
            {
                body.velocity = new Vector2(body.velocity.x, verticalMove);
               
            }
            else if (moveUpLadder == true)
            {
                body.velocity = new Vector2(body.velocity.x, verticalMove);
              
            }
        }
        else
        {
            body.gravityScale = 2f;
            animator.SetFloat("ClimbSpeed", Mathf.Abs(0));
        }
        
    }

    // Hàm xử lý di chuyển bằng keyboard
    //==========================================================================
    public void PlayerMove()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // tốc độ của nhân vật
        

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        

        if(Input.GetButtonUp("Horizontal"))
        {
            animator.SetFloat("Speed", Mathf.Abs(0));
            animator.SetBool("Grounded", false);
            
        }
        if(Input.GetButtonDown("Horizontal"))
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (jump == true)
            {
                //grounded = false;
                animator.SetBool("Grounded", false);
                
            }
            else if (jump == false )
            {
                grounded = true;
                animator.SetBool("Grounded", true);
                
            }
            if (isJumping == false)
                CreatDustCloud();
            
            
        }
        
        if (Input.GetButtonDown("Jump"))
        {

            if(isJumping==false)
            {
                if (_jumpAudio != null)
                {
                    _sound.PlayOneShot(_jumpAudio);
                }
            }
            isJumping = true;
            grounded = false;
            jump = true;
            animator.SetBool("Grounded", false);
            animator.SetBool("IsJumping", true);
            animator.SetBool("OnAir", true);
            
            if (isJumping == false)
            {
                CreatDustCloud();
                
            }
        }
        
        if (Input.GetButtonDown("Crouch"))
        {
            //grounded = true;
            crouch = true;
            animator.SetBool("IsCrouching", crouch);
            animator.SetBool("Grounded", true);

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", crouch);
        }
    }
    // xử lý events
    //========================================================================
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        //animator.SetBool("IsCrouching", isCrouching);
    }

    //=======================================================================
    void FixedUpdate()
    {
        // di chuyển nhân vật
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);//
        jump = false;
        
        verticalMove = 0f;

        animator.SetBool("Hurt", false);
    }
    // Functions xử lý va chạm =====================================================
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    { 
    // xử lý khi chạm mặt đất thì chuyển động nhảy ngừng
    
        if (collision.gameObject.tag == "Floor")
        {
            falling = false;
            grounded = true;
            jump = false;
            isJumping = false;
            IsClimbing = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("Grounded", true);
            animator.SetBool("OnAir", false);
            animator.SetBool("IsClimbing", false);
            CreatDustCloud();
        }
     // Xử lý va chảm với Prop là gai nền và gai trần
        if(collision.gameObject.tag=="Hurt Ground")
        {
            if (_hurtAudio != null)
            {
                _sound.PlayOneShot(_hurtAudio);
            }
            playerHealth.health -= 1;
            animator.SetBool("Hurt", true);
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                body.velocity = new Vector2(-hurtForce/4, hurtForce/4);
            }
            else
            {
                body.velocity = new Vector2(hurtForce/4, hurtForce/4);
            }
        }

     // xử lý va chạm với enemy

        if(collision.gameObject.tag=="Eagle"|| collision.gameObject.tag == "Opossum"||collision.gameObject.tag=="Enemy")
        {
            if (falling == true)
            {
                if (_EnemyDeath != null)
                {
                    _sound.PlayOneShot(_EnemyDeath);
                }
                body.velocity = new Vector2(0, jumpEnemyForce);
               
            }
            else if (falling == false)
            {
                if (_hurtAudio != null)
                {
                    _sound.PlayOneShot(_hurtAudio);
                }
                animator.SetBool("Hurt", true);
                animator.SetBool("IsJumping", false);
                playerHealth.health -= 1;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    body.velocity = new Vector2(-hurtForce, animator.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(hurtForce, animator.velocity.y);
                }

            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;
            animator.SetBool("Grounded", false);
        }
    }
    // Xử lý va chạm với object trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag=="Bullet")
        {
            if (_hurtAudio != null)
            {
                _sound.PlayOneShot(_hurtAudio);
            }
            animator.SetBool("Hurt", true);
            animator.SetBool("IsJumping", false);
            playerHealth.health -= 1;
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                body.velocity = new Vector2(-hurtForce, animator.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(hurtForce, animator.velocity.y);
            }
        }
        if(collision.gameObject.tag=="Cherry")
        {
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        if (collision.gameObject.tag == "Gem")
        {
            gems += 1;
            gemText.text = gems.ToString();
        }
    }

}
