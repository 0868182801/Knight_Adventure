using UnityEngine;

public class PlayerCotroller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // SerializeField hiển thị biến private trong unity
    [SerializeField] private float jumpForce = 15f; // Lực nhảy

    // Kiểu DL LayerMask ktra và giới hạn các thao tác va chạm (collision), raycast, hoặc trigger
    [SerializeField] private LayerMask groundLayer; // Xác định đối tượng có vai trò là mặt đất để player đứng lên nhảy
    [SerializeField] private Transform groundCheck; // Đặt dưới chân nhân vật để kiểm tra va chạm với mặt đất
    private Animator animator;
    private bool isGrounded;    // Xác định player có đứng trên mặt đất hay k
    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();    // Tham chiếu đến thành phần animator trong game
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsGameOver() || gameManager.IsGameWin()) return;    // Nếu gameover --> thoát k gọc các pương thức bên dưới
        HandleMovement();
        HanldeJump();
        UpdateAnimation();
    }
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");  // Lấy giá trị nhập từ bàn phím (A/D hoặc phím mũi tên trái/phải)
        //Horizontal di chuyển theo phương ngang, Horizontal trả về giá trị {-1;1}
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);    // Di chuyển ngang --> x thay đổi, y giữ 

        // Lật nhân vật theo hướng di chuyển
        if(moveInput > 0) transform.localScale = new Vector3(1,1,1);  // Khi nhấn phím D hoặc mũi tên phải ---> moveInput dao động từ 0 đến 1
        else if (moveInput < 0) transform.localScale = new Vector3(-1,1,1);  // Khi nhấn A or mũi tên trái ---> moveInput dao động từ -1 đến 0
    }

    private void HanldeJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded) // Trong Unity mặc định phím space = nhảy, ánh xạ "Jump" với phím Space
        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Chỉ thay đổi y
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);  // Ktra xem có Collider nào trong vòng tròn không
        /*
        OverlapCircle(Vị trí tâm, bán kính, Ktra va chạm với các đối tượng thuộc layerMask này) --> Vẽ vòng tròn quanh điểm groundCheck 
        */
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;   // Khi player k có trên mặt đất (isGrounded = true) --> chuyển sang animation jump
        animator.SetBool("isRunning", isRunning);   // Phải nhập đúng tên đã đặt trong animator
        animator.SetBool("isJumping", isJumping); 
    }
}
