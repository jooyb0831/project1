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
    // Start is called before the first frame update
    void Start()
    {
        sksystem = GameManager.Instance.SkSystem;
        sksystem.SetSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetQSkill(Skill skill)
    {

        GameObject obj = Instantiate(skillIcon, Qslot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.GetComponent<SkillIcon>().skill = skill.gameObject;
        obj.transform.SetAsFirstSibling();
        //SkillEquipWindow.Instance.skill_Q = skill.gameObject;
        Qslot.GetComponent<ActiveSkillSlot>().isFilled = true;
        GameObject obj2 = Instantiate(skillQuickIcon, qSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        //obj2.GetComponent<SkillQuickIcon>().skillui = skill_Q.GetComponent<SkillUISample>();
    }

    public void SetISkill(Skill skill)
    {
        GameObject obj = Instantiate(skillIcon, Islot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.GetComponent<SkillIcon>().skill = skill.gameObject;
        obj.transform.SetAsFirstSibling();
        //SkillEquipWindow.Instance.skill_I = skill.gameObject;
        Islot.GetComponent<ActiveSkillSlot>().isFilled = true;
        GameObject obj2 = Instantiate(skillQuickIcon, iSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        //obj2.GetComponent<SkillQuickIcon>().skillui = skill_Q.GetComponent<SkillUISample>();
    }
}
