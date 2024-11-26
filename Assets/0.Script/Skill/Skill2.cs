using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill
{
    [SerializeField] Shield shield;
    [SerializeField] float delay;
    [SerializeField] float coolTimer;
    [SerializeField] float timer;
    [SerializeField] bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.SkillTitle = JsonData.Instance.skillData.sData[2].skilltitle;
        data.SkillExplain = JsonData.Instance.skillData.sData[2].skillexplain;
        data.Index = JsonData.Instance.skillData.sData[2].index;
        data.CoolTime = JsonData.Instance.skillData.sData[2].cooltime;
        data.Damage = JsonData.Instance.skillData.sData[2].damage;
        data.NeedLevel = JsonData.Instance.skillData.sData[2].needlevel;
        data.SkillLevel = JsonData.Instance.skillData.sData[2].skilllevel;
        base.Init();
    }


    void SkillAct()
    {
        if(!isOn)
        {
            Instantiate(shield, p.transform);
            isOn = true;
        }
        else
        {
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            SkillAct();
            isWorking = true;
        }

        if(isWorking)
        {
            CoolTimeCheck();
        }
    }

    void CoolTimeCheck()
    {
        coolTimer += Time.deltaTime;
        if (coolTimer >= data.CoolTime)
        {
            coolTimer = 0;
            isWorking = false;
        }
    }
}
