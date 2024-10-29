using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    private bool isGrounded; // 地面に接しているかどうか

    private Rigidbody2D rb; // Rigidbody2D コンポーネント

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D コンポーネントを取得
    }

    void Update()
    {
        Move(); // 移動処理
        Jump(); // ジャンプ処理
    }

    // プレイヤーの移動処理
    void Move()
    {
        float move = Input.GetAxis("Horizontal"); // 水平方向の入力を取得
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y); // 速度を設定
    }

    // プレイヤーのジャンプ処理
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // スペースキーが押され、かつ地面に接している場合
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // ジャンプ力を加える
        }
    }

    // 地面に接触したときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面に接触した場合
        {
            isGrounded = true; // 地面に接しているフラグを立てる
        }
    }

    // 地面から離れたときの処理
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面から離れた場合
        {
            isGrounded = false; // 地面に接しているフラグを下げる
        }
    }
}
