using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    // 감지할 키 (인스펙터 창에서 원하는 키로 변경 가능)
    [SerializeField] private KeyCode destroyKey = KeyCode.Space;

    void Update()
    {
        // 지정한 키를 누른 순간 실행
        if (Input.GetKeyDown(destroyKey))
        {
            DestroyAllEnemyBullets();
        }
    }

    private void DestroyAllEnemyBullets()
    {
        // "EnemyBullet" 태그를 가진 모든 게임 오브젝트 검색
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        // 검색된 오브젝트 파괴
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }

        Debug.Log($"총 {enemyBullets.Length}개의 EnemyBullet을 제거했습니다.");
    }
}