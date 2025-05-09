using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUISystem : Singleton<SkillUISystem>
{
    private SkillSystem sksystem;
    [SerializeField] Transform Qslot;
    [SerializeField] Transform Islot;
    public GameObject skillIcon;
    [SerializeField] GameObject skill_Q;
    [SerializeField] GameObject skill_I;
    [SerializeField] Transform qSlot_inGame;
    [SerializeField] Transform iSlot_inGame;
    [SerializeField] GameObject skillQuickIcon;
    [SerializeField] SkillUISample[] skilluis;
    [SerializeField] SkillEquipWindow skEqwindow;
    // Start is called before the first frame update
    void Start()
    {
        sksystem = GameManager.Instance.SkSystem;
        SetSkillUIs();
        sksystem.SetSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkillUIs()
    {
        for(int i = 0; i<skilluis.Length; i++)
        {
            sksystem.skillUIs[i] = skilluis[i];
        }
        sksystem.SetSkillUI();
       
    }
    public void SetQSkill(Skill skill)
    {

        GameObject obj = Instantiate(skillIcon, Qslot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.GetComponent<SkillIcon>().skill = skill.gameObject;
        obj.transform.SetAsFirstSibling();
        skEqwindow.skill_Q = skill.ui.gameObject;
        Qslot.GetComponent<ActiveSkillSlot>().isFilled = true;
        GameObject obj2 = Instantiate(skillQuickIcon, qSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        obj2.GetComponent<SkillQuickIcon>().skillui = skill.ui;
    }

    public void SetISkill(Skill skill)
    {
        GameObject obj = Instantiate(skillIcon, Islot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.GetComponent<SkillIcon>().skill = skill.gameObject;
        obj.transform.SetAsFirstSibling();
        skEqwindow.skill_I = skill.ui.gameObject;
        Islot.GetComponent<ActiveSkillSlot>().isFilled = true;
        GameObject obj2 = Instantiate(skillQuickIcon, iSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        obj2.GetComponent<SkillQuickIcon>().skillui = skill.ui;
    }

}
