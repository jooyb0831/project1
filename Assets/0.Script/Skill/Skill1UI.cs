using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill1UI :  SkillUISample
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        skill = sksystem.skills[1];
        SetUI();
        if (skill.isSet)
        {
            isEquiped = true;
            /*
            if(skill.slotIdx == 1)
            {
                SkillUISystem.Instance.SetQSkill(skill);
            }
            else if(skill.slotIdx== 2)
            {
                SkillUISystem.Instance.SetISkill(skill);
            }
            */
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (pData == null)
        {
            pData = GameManager.Instance.PlayerData;
            return;
        }

        if (skill.data.NeedLevel == pData.Level)
        {
            isUnlock = true;
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
        skillExplainTxt2.text = $"쿨타임 : {skill.data.CoolTime}초\n공격력 : {skill.data.Damage}";
        icon.sprite = skill.data.SkillIcon;
        unlockTxt.text = $"레벨 필요 : {skill.data.NeedLevel}";
    }
}
