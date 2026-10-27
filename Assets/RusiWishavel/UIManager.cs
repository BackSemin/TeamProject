using UnityEngine;
using UnityEngine.UI; // 유니티 기본 Text 컴포넌트를 사용하기 위해 필수입니다.

public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public UpgradeManager upgradeManager;

    [Header("UI Text Displays")]
    public Text goldText;
    public Text moveSpeedText;
    public Text attackPowerText;
    public Text skillText;

    [Header("UI Cost Texts")]
    public Text moveSpeedCostText;
    public Text attackPowerCostText;
    public Text skillCostText;

    [Header("Skill Button Label")]
    public Text skillButtonText; // 버튼에 표시되는 글자 (배우기 / 강화)

    public void UpdateAllUI()
    {
        // PlayerStats나 UpgradeManager가 연결되어 있지 않은 경우 오류 방지
        if (playerStats == null || upgradeManager == null) return;

        // 1. 기본 재화 및 스탯 표기
        if (goldText != null)
            goldText.text = $"Gold: {playerStats.gold}";

        if (moveSpeedText != null)
            moveSpeedText.text = $"이속: {playerStats.moveSpeed} (Lv.{playerStats.moveSpeedLevel})";

        if (attackPowerText != null)
            attackPowerText.text = $"공격력: {playerStats.attackPower} (Lv.{playerStats.attackLevel})";

        // 2. 비용 표기
        if (moveSpeedCostText != null)
            moveSpeedCostText.text = $"{upgradeManager.moveSpeedCost} G";

        if (attackPowerCostText != null)
            attackPowerCostText.text = $"{upgradeManager.attackPowerCost} G";

        // 3. 스킬 습득 여부에 따른 UI 상태 처리
        if (!upgradeManager.isSkillLearned)
        {
            if (skillText != null) skillText.text = "스킬: 미습득";
            if (skillButtonText != null) skillButtonText.text = "스킬 배우기";
            if (skillCostText != null) skillCostText.text = $"{upgradeManager.learnSkillCost} G";
        }
        else
        {
            if (skillText != null) skillText.text = $"스킬 데미지: {playerStats.skillDamage} (Lv.{playerStats.skillLevel})";
            if (skillButtonText != null) skillButtonText.text = "스킬 강화";
            if (skillCostText != null) skillCostText.text = $"{upgradeManager.skillUpgradeCost} G";
        }
    }
}