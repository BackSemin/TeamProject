using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Target Stats")]
    public PlayerStats playerStats;

    [Header("Upgrade Costs")]
    public int moveSpeedCost = 100;
    public int attackPowerCost = 150;
    public int skillUpgradeCost = 200;
    public int learnSkillCost = 500; // 스킬 습득 비용

    [Header("Upgrade Increment Values")]
    public float moveSpeedIncrease = 1f;
    public float attackPowerIncrease = 5f;
    public float skillDamageIncrease = 15f;
    public float skillCooldownDecrease = 0.3f;

    [Header("Skill Status")]
    public bool isSkillLearned = false; // 스킬 습득 여부

    // UI 화면 갱신용 참조
    public UIManager uiManager;

    private void Start()
    {
        // 시작 시 UI 화면 초기화
        if (uiManager != null) uiManager.UpdateAllUI();
    }

    // 1. 이동 속도 업그레이드
    public void OnClickUpgradeMoveSpeed()
    {
        if (playerStats.SpendGold(moveSpeedCost))
        {
            playerStats.UpgradeMoveSpeed(moveSpeedIncrease);
            moveSpeedCost = Mathf.RoundToInt(moveSpeedCost * 1.3f); // 비용 30% 증가

            if (uiManager != null) uiManager.UpdateAllUI();
        }
    }

    // 2. 기본 공격력 업그레이드
    public void OnClickUpgradeAttackPower()
    {
        if (playerStats.SpendGold(attackPowerCost))
        {
            playerStats.UpgradeAttackPower(attackPowerIncrease);
            attackPowerCost = Mathf.RoundToInt(attackPowerCost * 1.3f);

            if (uiManager != null) uiManager.UpdateAllUI();
        }
    }

    // 3. 스킬 배우기 또는 스킬 업그레이드
    public void OnClickSkillButton()
    {
        // [CASE A] 스킬을 아직 배우지 않은 경우 -> 스킬 습득
        if (!isSkillLearned)
        {
            if (playerStats.SpendGold(learnSkillCost))
            {
                isSkillLearned = true;
                Debug.Log("[스킬 습득] 새로운 스킬을 배웠습니다!");

                if (uiManager != null) uiManager.UpdateAllUI();
            }
        }
        // [CASE B] 이미 스킬을 배운 경우 -> 스킬 스탯 업그레이드
        else
        {
            if (playerStats.SpendGold(skillUpgradeCost))
            {
                playerStats.UpgradeSkill(skillDamageIncrease, skillCooldownDecrease);
                skillUpgradeCost = Mathf.RoundToInt(skillUpgradeCost * 1.4f);

                if (uiManager != null) uiManager.UpdateAllUI();
            }
        }
    }
}