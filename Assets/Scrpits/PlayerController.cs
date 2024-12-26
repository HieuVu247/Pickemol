using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions; // Tham chiếu đến Input Action Asset
    private InputAction moveAction;
    public float fixedYPosition = 0f; // Độ y cố định

    private Rigidbody2D rb;
    private ScoreManager scoreManager;

    private void OnEnable()
    {
        // Lấy hành động di chuyển từ Input Action Asset
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Ngăn chặn quay

        scoreManager = FindObjectOfType<ScoreManager>(); // Tìm ScoreManager
    }

    private void Update()
    {
        // Lấy vị trí con trỏ chuột
        Vector2 mousePosition = moveAction.ReadValue<Vector2>();
        // Chuyển đổi vị trí con trỏ chuột từ màn hình sang thế giới
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Cập nhật vị trí của nhân vật với y cố định
        Vector2 targetPosition = new Vector2(worldPosition.x, fixedYPosition);
        rb.MovePosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Beer"))
        {
            Debug.Log("Va chạm với bia!");
            Destroy(collision.gameObject); // Hủy đối tượng bia khi có va chạm
            scoreManager.AddScore(100); // Tăng 100 điểm
        }
    }
}