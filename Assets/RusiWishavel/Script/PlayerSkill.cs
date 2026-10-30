using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("강제 유도")]
    public float attackspeed; //유도되어 날아가는 속도
    [Header("시간 정지")]
    public float stoptime;
    [Header("감속장")]
    public float slowattack;

    public float LastUsedTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Forcedinducion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
    }
}
