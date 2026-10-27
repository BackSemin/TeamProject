using UnityEngine;

/// <summary>
/// 발사된 스킬 투사체에 부착하여 직진 이동 및 적 충돌 처리를 담당하는 스크립트입니다.
/// </summary>
public class SkillProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    public float lifeTime = 4f;

    /// <summary>
    /// PlayerCombat 스크립트에서 스킬이 생성될 때 데미지와 속도를 설정해 주는 메서드입니다.
    /// </summary>
    public void Initialize(float damageAmount, float moveSpeed)
    {
        damage = damageAmount;
        speed = moveSpeed;

        // 일정 시간 후 스킬 오브젝트 자동 파괴
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // 바라보는 전방(Forward) 방향으로 직진 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Enemy"인 경우
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"[스킬 명중] 적에게 {damage} 데미지를 부여했습니다!");

            // 적 스크립트에 데미지를 전달하는 로직 (예시)
            // other.GetComponent<Enemy>()?.TakeDamage(damage);

            // 충돌 후 스킬 투사체 삭제
            Destroy(gameObject);
        }
    }
}