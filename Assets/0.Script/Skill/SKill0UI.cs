using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SKill0UI : SkillUISample
{

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (pData == null)
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

        if(isUnlock)
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

    public override void Init()
    {
        base.Init();
        skill = sksystem.skills[0];
        if (skill.isSet)
        {
            isEquiped = true;
        }
        SetUI();
        
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
