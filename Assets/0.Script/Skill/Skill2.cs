using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill
{
    [SerializeField] Shield shield;
    [SerializeField] float delay;
    [SerializeField] float coolTimer;
    [SerializeField] bool isOn = false;

    [SerializeField] float duration;
    [SerializeField] float shieldTimer;
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
            shield = Pooling.Instance.GetPool(DicKey.shield, p.transform).GetComponent<Shield>();
            isOn = true;
        }
        shield.transform.position = p.transform.position;
        shieldTimer += Time.deltaTime;
        if (shieldTimer >= duration)
        {
            shieldTimer = 0;
            Pooling.Instance.SetPool(DicKey.shield, shield.gameObject);
            isStart = false;
            isOn = false;
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
            isWorking = false;
            coolTimer = 0;
        }
    }
}
