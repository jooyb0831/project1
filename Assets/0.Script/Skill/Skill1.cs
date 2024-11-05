using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletArea;
    [SerializeField] float dealy;
    [SerializeField] float coolTimer;
    [SerializeField] float timer;
    [SerializeField] bool isDir = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
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
    public override void Init()
    {
        data.SkillTitle = JsonData.Instance.skillData.sData[1].skilltitle;
        data.SkillExplain = JsonData.Instance.skillData.sData[1].skillexplain;
        data.Index = JsonData.Instance.skillData.sData[1].index;
        data.CoolTime = JsonData.Instance.skillData.sData[1].cooltime;
        data.Damage = JsonData.Instance.skillData.sData[1].damage;
        data.NeedLevel = JsonData.Instance.skillData.sData[1].needlevel;
        data.SkillLevel = JsonData.Instance.skillData.sData[1].skilllevel;

        base.Init();
    }

    void SkillAct()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        bulletArea.transform.position = p.firePos.position;
        GameObject obj = Instantiate(bullet, bulletArea.transform);
        if (!isDir)
        {
            if (p.transform.localScale.x < 0)
            {
                obj.GetComponent<Skill1Bullet>().isRight = false;
            }
            isDir = true;
        }
        obj.transform.SetParent(null);
        isStart = false;
    }

    void CoolTimeCheck()
    {
        coolTimer += Time.deltaTime;
        if (coolTimer >= data.CoolTime)
        {
            coolTimer = 0;
            isWorking = false;
            isDir = false;
        }
    }



}
