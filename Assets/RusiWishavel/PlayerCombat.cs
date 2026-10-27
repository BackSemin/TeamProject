using UnityEngine;

/// <summary>
/// 플레이어의 기본 공격 및 스킬 발사를 담당하는 전투 제어 클래스입니다.
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [Header("참조 스크립트")]
    [Tooltip("플레이어의 공격력, 스킬 데미지, 쿨타임 스탯 정보를 가져올 참조입니다.")]
    private PlayerStats stats;

    [Tooltip("스킬 습득 여부(!isSkillLearned)를 확인하기 위한 업그레이드 매니저 참조입니다.")]
    public UpgradeManager upgradeManager;

    [Header("스킬 발사체 설정")]
    [Tooltip("E키를 눌렀을 때 생성하여 발사할 스킬 프리팹입니다.")]
    public GameObject skillPrefab;

    [Tooltip("스킬 프리팹이 생성될 위치(플레이어의 손 또는 지팡이 끝 등)입니다.")]
    public Transform skillSpawnPoint;

    // 다음 공격이 가능한 시각(Time.time 기준)을 기록하는 내부 변수입니다.
    private float nextAttackTime = 0f;
    private float nextSkillTime = 0f;

    /// <summary>
    /// 게임 오브젝트 생성 직후 스크립트 초기화를 진행합니다.
    /// </summary>
    private void Awake()
    {
        // 동일한 플레이어 오브젝트에 붙어있는 PlayerStats 컴포넌트를 가져옵니다.
        stats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// 매 프레임마다 플레이어의 공격 키 입력을 감지합니다.
    /// </summary>
    private void Update()
    {
        // 기본 공격 입력 처리
        HandleBasicAttack();

        // 스킬 공격 입력 처리
        HandleSkill();
    }

    /// <summary>
    /// 기본 공격(스페이스바) 입력을 감지하고 쿨타임을 체크합니다.
    /// </summary>
    private void HandleBasicAttack()
    {
        // 스페이스바를 눌렀고, 현재 시간이 다음 공격 가능 시간 이상일 때 실행합니다.
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime)
        {
            // 실제 기본 공격 동작을 실행합니다.
            PerformBasicAttack();

            // 현재 시간 + PlayerStats에 설정된 기본 공격 쿨타임으로 다음 공격 가능 시간을 갱신합니다.
            nextAttackTime = Time.time + stats.attackCooldown;
        }
    }

    /// <summary>
    /// 실제 기본 공격(근접 데미지 판정 등)을 처리하는 로직입니다.
    /// </summary>
    private void PerformBasicAttack()
    {
        // PlayerStats에서 현재 업그레이드된 기본 공격력을 불러와 출력합니다.
        Debug.Log($"[기본 공격] 데미지: {stats.attackPower}");

        // TODO: 여기에 OverlapSphere 또는 Raycast를 사용한 적 피격 판정 로직을 구현합니다.
    }

    /// <summary>
    /// 스킬 공격(E키) 입력을 감지하고, 스킬 습득 여부 및 쿨타임을 체크합니다.
    /// </summary>
    private void HandleSkill()
    {
        // 1. UpgradeManager가 연결되어 있고, 아직 스킬을 배우지 않았다면 스킬을 사용하지 않고 리턴합니다.
        if (upgradeManager != null && !upgradeManager.isSkillLearned)
        {
            return;
        }

        // 2. E키를 눌렀고, 스킬 쿨타임이 끝났다면 스킬을 실행합니다.
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextSkillTime)
        {
            // 실제 스킬 발사 동작을 실행합니다.
            PerformSkill();

            // 현재 시간 + PlayerStats에 설정된 스킬 쿨타임으로 다음 스킬 가능 시간을 갱신합니다.
            nextSkillTime = Time.time + stats.skillCooldown;
        }
    }

    /// <summary>
    /// 스킬 투사체를 생성하고 스탯(데미지, 속도)을 주입하여 발사하는 로직입니다.
    /// </summary>
    private void PerformSkill()
    {
        // 스킬 프리팹과 생성 위치가 인스펙터에 정상적으로 할당되어 있는지 확인합니다.
        if (skillPrefab != null && skillSpawnPoint != null)
        {
            // 지정한 위치(skillSpawnPoint)와 회전값으로 스킬 투사체 게임 오브젝트를 씬에 생성합니다.
            GameObject skillObj = Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);

            // 생성된 스킬 오브젝트에서 SkillProjectile 컴포넌트를 가져옵니다.
            SkillProjectile projectile = skillObj.GetComponent<SkillProjectile>();

            // 스크립트가 존재하면 PlayerStats의 스킬 데미지와 투사체 속도를 스킬 오브젝트에 전달합니다.
            if (projectile != null)
            {
                projectile.Initialize(stats.skillDamage, stats.skillProjectileSpeed);
            }
        }
        else
        {
            Debug.LogWarning("SkillPrefab 또는 SkillSpawnPoint가 인스펙터에 할당되지 않았습니다!");
        }
    }
}