using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0 : Skill
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletArea;
    [SerializeField] float delay;
    [SerializeField] float coolTimer;
    [SerializeField] float timer;
    [SerializeField] int cnt = 0;
    [SerializeField] bool setPos = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.SkillTitle = JsonData.Instance.skillData.sData[0].skilltitle;
        data.SkillExplain = JsonData.Instance.skillData.sData[0].skillexplain;
        data.Index = JsonData.Instance.skillData.sData[0].index;
        data.CoolTime = JsonData.Instance.skillData.sData[0].cooltime;
        data.Damage = JsonData.Instance.skillData.sData[0].damage;
        data.NeedLevel = JsonData.Instance.skillData.sData[0].needlevel;
        data.SkillLevel = JsonData.Instance.skillData.sData[0].skilllevel;
        
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (isStart)
        {
            SkillAct();
            isWorking = true;
        }
        if (isWorking)
        {
            CoolTimeCheck();
        }
    }

    /// <summary>
    /// 스킬 작동
    /// </summary>
    void SkillAct()
    {
        if(!setPos)
        {
            bulletArea.transform.position = SetPosition();
        }
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            FireBullet();
            timer = 0;
            cnt++;
        }
        if (cnt >= 10)
        {
            cnt = 0;
            timer = delay;
            isStart = false;
            setPos = false;
        }
    }

    /// <summary>
    /// 쿨타임 체크
    /// </summary>
    void CoolTimeCheck()
    {
        coolTimer += Time.deltaTime;
        if (coolTimer >= data.CoolTime)
        {
            coolTimer = 0;
            isWorking = false;
        }
    }

    /// <summary>
    /// 총알발사(생성)
    /// </summary>
    public void FireBullet()
    {
        Vector3 randPos = Return_RandomPosition();
        Instantiate(bullet, randPos, Quaternion.identity);
    }

    /// <summary>
    /// bulletArea 내에서 랜덤 포지션으로 생성하는 코드
    /// </summary>
    /// <returns></returns>
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = bulletArea.transform.position;
        float range_X = bulletArea.GetComponent<BoxCollider2D>().bounds.size.x;
        float range_Y = bulletArea.GetComponent<BoxCollider2D>().bounds.size.y;


        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    /// <summary>
    /// bulletArea의 위치
    /// </summary>
    /// <returns></returns>
    public Vector2 SetPosition()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }
        setPos = true;
        Transform pos = p.skillPos.transform.GetChild(0);
        float x = pos.position.x;
        float y = pos.position.y;
        Vector2 vec = new(x, y);
        return vec;
    }

}
