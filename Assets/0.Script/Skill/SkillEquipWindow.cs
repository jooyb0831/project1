using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEquipWindow : Singleton<SkillEquipWindow>
{ 
    private SkillSystem sksystem;
    [SerializeField] Transform Qslot;
    [SerializeField] Transform Islot;
    public GameObject temp_skill;
    public GameObject skillIcon;
    public GameObject skill_Q;
    public GameObject skill_I;
    [SerializeField] Transform qSlot_inGame;
    [SerializeField] Transform iSlot_inGame;
    [SerializeField] GameObject clearWindow;
    [SerializeField] GameObject skillQuickIcon;
    // Start is called before the first frame update
    void Start()
    {
        sksystem = GameManager.Instance.SkSystem;

    }

    public void OnQEquipBtn()
    {
        SkillSet(Qslot, qSlot_inGame, ref skill_Q, 1);
    }
    
    public void OnIEquipBtn()
    {
        SkillSet(Islot, iSlot_inGame, ref skill_I, 2);
    }

    /// <summary>
    /// 스킬과 UI세팅
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="quickSlot"></param>
    /// <param name="skillObj"></param>
    /// <param name="idx"></param>
    void SkillSet(Transform slot, Transform quickSlot, ref GameObject skillObj, int idx)
    {
        ActiveSkillSlot activeSlot = slot.GetComponent<ActiveSkillSlot>();
        SkillUISample skillUI;
        Skill skill;

        //이미 스킬이 있다면 기존 스킬 삭제
        if (activeSlot.isFilled)
        {
            skillUI = skillObj.GetComponent<SkillUISample>();
            skill = skillUI.skill;
            skillUI.isEquiped = false;
            skill.isSet = false;
            skill.slotIdx = 0;
            Destroy(slot.GetChild(0).gameObject);
        }

        //스킬과 UI 세팅
        skillObj = temp_skill;
        skillUI = skillObj.GetComponent<SkillUISample>();
        skill = skillUI.skill;

        //스킬 UI 생성(메뉴)
        GameObject obj = Instantiate(skillIcon, slot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.transform.SetAsFirstSibling();

        slot.GetComponent<ActiveSkillSlot>().isFilled = true;
        temp_skill.GetComponent<SkillUISample>().isEquiped = true;
        skill.isSet = true;
        skill.slotIdx = idx;
        obj.GetComponent<SkillIcon>().skill = skillObj;


        //게임 화면(퀵)에 UI 생성
        GameObject obj2 = Instantiate(skillUI.skillqicon.gameObject, quickSlot);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        obj2.GetComponent<SkillQuickIcon>().skillui = skillUI;


        //스킬 시스템에 스킬 등록
        if (idx == 1)
        {
            sksystem.qSkill = skill.gameObject;
        }
        else if (idx == 2)
        {
            sksystem.iSkill = skill.gameObject;
        }

        //창 닫기
        gameObject.SetActive(false);

    }

    public void OnExitBtn()
    {
        gameObject.SetActive(false);
    }

    public void OnQSkillSlotClicked()
    {
        ClearWindowActive(Qslot, 1);
    }


    public void OnISkillSlotClicked()
    {
        ClearWindowActive(Islot, 2);
    }


    /// <summary>
    /// ClaerWindow불러오기
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="idx"></param>
    void ClearWindowActive(Transform slot, int idx)
    {
        if(!slot.GetComponent<ActiveSkillSlot>().isFilled)
        {
            return;
        }
        clearWindow.SetActive(true);

        if(idx ==1)
        {
            clearWindow.GetComponent<SkillClearWindow>().isQslot = true;
        }
        else if (idx ==2)
        {
            clearWindow.GetComponent<SkillClearWindow>().isQslot = false;
        }

    }

    public void ClearQSkill()
    {
        SkillClear(ref skill_Q, Qslot, qSlot_inGame, 1);
    }

    public void ClearISkill()
    {
        SkillClear(ref skill_I, Islot, iSlot_inGame, 2);
    }

    /// <summary>
    /// 스킬 삭제(클리어)
    /// </summary>
    /// <param name="skillObj"></param>
    /// <param name="slot"></param>
    /// <param name="quickSlot"></param>
    /// <param name="idx"></param>
    void SkillClear(ref GameObject skillObj, Transform slot, Transform quickSlot, int idx)
    {
        SkillUISample skillUI = skillObj.GetComponent<SkillUISample>();
        Skill skill = skillUI.skill;

        skillUI.isEquiped = false;
        slot.GetComponent<ActiveSkillSlot>().isFilled = false;
        skill.isSet = false;
        skill.slotIdx = 0;

        if(sksystem == null)
        {
            sksystem = GameManager.Instance.SkSystem;
        }

        if (idx == 1)
        {
            sksystem.qSkill = null;
        }
        else if(idx == 2)
        {
            sksystem.iSkill = null;
        }

        skillObj = null;

        Destroy(slot.GetChild(0).gameObject);
        Destroy(quickSlot.GetChild(0).gameObject);

    }

}
