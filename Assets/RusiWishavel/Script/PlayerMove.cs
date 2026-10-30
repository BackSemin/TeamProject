using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;

    [Header("이동 관련 설정")]
    public float moveSpeed = 5f;      // W 키 누를 때 직진 속도
    public float rotateSpeed = 50f;   // A/D 키 누를 때 회전 속도 (도/초)
    public float brakePower = 10f;     // S 키 누를 때 감속 및 후진 가속도 (2~5 추천)

    [Header("플레이어 정보")]
    public float hp = 10f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. A, D 키 회전
        float rotateInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, 0, -rotateInput * rotateSpeed * Time.deltaTime);

        // 2. 매 프레임마다 체력 및 씬 전환 체크 함수 호출
        CheckGameOver();
    }

    void FixedUpdate()
    {
        // W 키 (전진)
        if (Input.GetKey(KeyCode.W))
        {
            rigid.linearVelocity = transform.up * moveSpeed;
        }
        // S 키 (천천히 감속 후 자연스러운 후진)
        else if (Input.GetKey(KeyCode.S))
        {
            // 목표 속도를 '후진 방향(-transform.up)'으로 지정합니다.
            // 속도가 줄어들다가 0을 거쳐 자연스럽게 뒤로 움직이게 됩니다.
            Vector2 reverseTarget = -transform.up * (moveSpeed * 0.5f); // 후진 속도는 전진의 절반
            rigid.linearVelocity = Vector2.MoveTowards(rigid.linearVelocity, reverseTarget, brakePower * Time.fixedDeltaTime);
        }
        // 아무 키도 안 누름 (자연 관성 정지)
        else
        {
            rigid.linearVelocity = Vector2.MoveTowards(rigid.linearVelocity, Vector2.zero, 2f * Time.fixedDeltaTime);
        }
    }

    // 씬 전환 및 게임 오버 체크 함수
    void CheckGameOver()
    {
        // 정확히 0f가 아닐 수 있으므로 <= 0f로 비교합니다.
        if (hp <= 0f)
        {
            Debug.Log("게임 오버");
            SceneManager.LoadScene("MenuScene");
        }
    }
}