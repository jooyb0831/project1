using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill2UI : SkillUISample
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        skill = sksystem.skills[2];
        SetUI();
        if(skill.isSet)
        {
            isEquiped = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(pData == null)
        {
            pData = GameManager.Instance.PlayerData;
            return;
        }

        if(skill.data.Unlocked)
        {
            isUnlock = true;
        }
        else
        {
            if (skill.data.NeedLevel <= pData.Level)
            {
                isUnlock = true;
                skill.data.Unlocked = true;
                unlockCover.SetActive(false);
            }
        }
        if (isUnlock)
        {
            unlockCover.SetActive(false);
        }


        if (isEquiped)
        {
            usingCover.SetActive(true);
        }
        else if (!isEquiped)
        {
            usingCover.SetActive(false);
        }

    }
    void SetUI()
    {
        skillTitleTxt.text = $"Lv.{skill.data.SkillLevel} {skill.data.SkillTitle}";
        skillExplainTxt.text = skill.data.SkillExplain;
        skillExplainTxt2.text = $"��Ÿ�� : {skill.data.CoolTime}��\n���ݷ� : {skill.data.Damage}";
        icon.sprite = skill.data.SkillIcon;
        unlockTxt.text = $"���� �ʿ� : {skill.data.NeedLevel}";
    }
}
