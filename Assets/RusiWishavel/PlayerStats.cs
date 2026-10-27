using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("1. Movement Stats")]
    [Tooltip("플레이어 이동 속도")]
    public float moveSpeed = 5f;
    public int moveSpeedLevel = 1;

    [Header("2. Attack Stats")]
    [Tooltip("기본 공격력")]
    public float attackPower = 10f;
    [Tooltip("기본 공격 쿨타임 (초)")]
    public float attackCooldown = 0.5f;
    public int attackLevel = 1;

    [Header("3. Skill Stats")]
    [Tooltip("스킬 데미지")]
    public float skillDamage = 25f;
    [Tooltip("스킬 쿨타임 (초)")]
    public float skillCooldown = 3f;
    [Tooltip("스킬 투사체 속도")]
    public float skillProjectileSpeed = 15f;
    public int skillLevel = 1;

    [Header("4. Currency")]
    [Tooltip("보유 골드")]
    public int gold = 1000;

    // ==========================================
    // 업그레이드 메서드 (외부 UI / 이벤트에서 호출)
    // ==========================================

    /// <summary>
    /// 이동 속도를 증가시킵니다.
    /// </summary>
    public void UpgradeMoveSpeed(float amount)
    {
        moveSpeed += amount;
        moveSpeedLevel++;
        Debug.Log($"[업그레이드] 이동 속도: {moveSpeed} (Lv.{moveSpeedLevel})");
    }

    /// <summary>
    /// 기본 공격력을 증가시킵니다.
    /// </summary>
    public void UpgradeAttackPower(float amount)
    {
        attackPower += amount;
        attackLevel++;
        Debug.Log($"[업그레이드] 기본 공격력: {attackPower} (Lv.{attackLevel})");
    }

    /// <summary>
    /// 스킬 데미지를 증가시키고 쿨타임을 줄입니다.
    /// </summary>
    /// <param name="damageAmount">스킬 데미지 증가량</param>
    /// <param name="cooldownReduction">쿨타임 감소량 (초)</param>
    public void UpgradeSkill(float damageAmount, float cooldownReduction)
    {
        skillDamage += damageAmount;
        // 쿨타임이 0.5초 이하로 떨어지지 않도록 최소 제한 설정
        skillCooldown = Mathf.Max(0.5f, skillCooldown - cooldownReduction);
        skillLevel++;
        Debug.Log($"[업그레이드] 스킬 데미지: {skillDamage}, 쿨타임: {skillCooldown}초 (Lv.{skillLevel})");
    }

    /// <summary>
    /// 골드를 소비합니다. (구매 성공 시 true 반환)
    /// </summary>
    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log($"[골드 소비] 남은 골드: {gold}");
            return true;
        }

        Debug.Log("골드가 부족합니다!");
        return false;
    }
}