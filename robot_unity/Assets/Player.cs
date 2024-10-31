using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    private bool isGrounded; // 地面に接しているかどうか
    private bool isJumping; // ジャンプ中かどうか
    private GameObject playerrig; // PlayerのGameObject

    private Rigidbody2D rb; // Rigidbody2D コンポーネント

    //向きを保存
    private Quaternion player_rotation = Quaternion.Euler(-89.98f, 0, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D コンポーネントを取得
        playerrig = GameObject.Find("playerrig"); // PlayerのGameObjectを取得
    }

    void Update()
    {
        Move(); // 移動処理
        Jump(); // ジャンプ処理
    }
    void LateUpdate()
    {
        float move = Input.GetAxis("Horizontal"); // 水平方向の入力を取得

        // プレイヤーの向きを変える
        playerrig.transform.rotation = player_rotation;
    }
    // プレイヤーの移動処理
    void Move()
    {
        float move = Input.GetAxis("Horizontal"); // 水平方向の入力を取得
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y); // 速度を設定

        if (move > 0) // 右に移動中
        {
            player_rotation = Quaternion.Euler(-89.98f, 90, 0);
        }
        else if (move < 0) // 左に移動中
        {
            player_rotation = Quaternion.Euler(-89.98f, -90, 0);
        }

        // アニメーション移行
        if (move != 0) // 移動中かつ地面に接している場合
        {
            // プレイヤーの足元にレイキャストを飛ばし、地面との距離を測る
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
            isGrounded = hit.collider != null; // 地面に接しているかどうか
            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("isWalking", true);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
    }

    // プレイヤーのジャンプ処理
    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && isGrounded) // スペースキーまたはゲームパッドのジャンプボタンが押され、かつ地面に接している場合
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // ジャンプ力を加える
            GetComponent<Animator>().SetTrigger("jump"); // ジャンプアニメーション
            GetComponent<Animator>().SetBool("isGrounded", false);
        }

    }

    // 地面に接触したときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面に接触した場合
        {
            isGrounded = true; // 地面に接しているフラグを立てる
            GetComponent<Animator>().SetBool("isGrounded", true);
        }
    }

    // 地面から離れたときの処理
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面から離れた場合
        {
            // isGrounded = false; // 地面に接しているフラグを下げる
        }
    }
}
