using UnityEngine;

// 플레이어의 2D 이동 및 방향 전환을 담당하는 클래스
public class PlayerMove : MonoBehaviour
{
    // [추가] PlayerStats 스크립트를 참조할 변수
    private PlayerStats stats;

    // 물리 이동 및 2D 이미지 연산을 위한 컴포넌트 변수
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    // 무게, 체력, 저장공간 (필요 시 PlayerStats로 옮겨서 연동하셔도 됩니다)
    public float mass; // 무게
    public float hp; // 체력
    public float bagsize; // 가방 사이즈

    // 입력받은 방향 벡터를 저장할 변수
    private Vector2 moveInput;

    // 게임 시작 시 컴포넌트를 가져옴 (초기화)
    void Awake()
    {
        // [추가] 같은 오브젝트에 붙어있는 PlayerStats 컴포넌트를 참조
        stats = GetComponent<PlayerStats>();

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
        // PlayerStats가 정상적으로 연결되어 있는지 확인
        float currentSpeed = (stats != null) ? stats.moveSpeed : 5f;

        // [수정] moveSpeed 대신 stats.moveSpeed를 적용하여 실제 캐릭터를 물리적으로 이동
        rigid.linearVelocity = new Vector2(moveInput.x * currentSpeed, moveInput.y * currentSpeed);
    }
}