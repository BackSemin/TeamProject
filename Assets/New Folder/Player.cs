using UnityEngine;

// 플레이어의 2D 이동 및 방향 전환을 담당하는 클래스
public class PlayerMove : MonoBehaviour
{
    // [SerializeField]를 사용하여 인스펙터 창에서 이동 속도를 조절할 수 있도록 설정
    [SerializeField] private float moveSpeed = 5f;

    // 물리 이동 및 2D 이미지 연산을 위한 컴포넌트 변수
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    // 입력받은 방향 벡터를 저장할 변수
    private Vector2 moveInput;

    // 게임 시작 시 컴포넌트를 가져옴 (초기화)
    void Awake()
    {
        // 오브젝트에 붙어있는 Rigidbody2D와 SpriteRenderer 컴포넌트를 참조
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 매 프레임마다 호출 (입력 처리용)
    void Update()
    {
        // 방향키 또는 WASD 입력을 즉시 가져옴 (-1, 0, 1 중 하나)
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // 대각선 이동 시 속도가 빠르지 않도록 대각선 길이를 1로 정규화(.normalized)
        moveInput = new Vector2(moveHorizontal, moveVertical).normalized;

        // 좌우 입력이 있을 때만 스프라이트 이미지의 반전(좌/우 바라보기)을 처리
        if (moveHorizontal != 0)
        {
            // 왼쪽 이동 중이면 true(좌우 반전), 오른쪽 이동 중이면 false
            spriteRenderer.flipX = moveHorizontal < 0;
        }
    }

    // 물리 연산 주기에 맞춰 고정적으로 호출 (물리 이동 처리용)
    void FixedUpdate()
    {
        // Rigidbody2D의 속도(linearVelocity)를 변경하여 실제 캐릭터를 물리적으로 이동
        rigid.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}