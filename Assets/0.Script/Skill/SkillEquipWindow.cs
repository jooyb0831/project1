using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEquipWindow : MonoBehaviour
{
    private SkillSystem sksystem;
    [SerializeField] Transform Qslot;
    [SerializeField] Transform Islot;
    public GameObject temp_skill;
    public GameObject skillIcon;
    [SerializeField] GameObject skill_Q;
    [SerializeField] GameObject skill_I;
    [SerializeField] Transform qSlot_inGame;
    [SerializeField] Transform iSlot_inGame;
    [SerializeField] GameObject clearWindow;
    // Start is called before the first frame update
    void Start()
    {
        sksystem = GameManager.Instance.SkSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQEquipBtn()
    {
        if(Qslot.GetComponent<ActiveSkillSlot>().isFilled)
        {
            skill_Q.GetComponent<SkillUISample>().isEquiped = false;
            skill_Q = null;
            Destroy(Qslot.GetChild(0).gameObject);
        }
        skill_Q = temp_skill;
        GameObject obj = Instantiate(skillIcon, Qslot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill_Q.GetComponent<SkillUISample>().skill.data.SkillIcon;
        obj.transform.SetAsFirstSibling();
        Qslot.GetComponent<ActiveSkillSlot>().isFilled = true;
        temp_skill.GetComponent<SkillUISample>().isEquiped = true;
        
        obj.GetComponent<SkillIcon>().skill = skill_Q;

        GameObject obj2 = Instantiate(skill_Q.GetComponent<SkillUISample>().skillqicon.gameObject, qSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill_Q.GetComponent<SkillUISample>().skill;
        obj2.GetComponent<SkillQuickIcon>().skillui = skill_Q.GetComponent<SkillUISample>();
        sksystem.qSkill = skill_Q.GetComponent<SkillUISample>().skill.gameObject;

        gameObject.SetActive(false);
    }               

    public void OnIEquipBtn()
    {
        if(Islot.GetComponent<ActiveSkillSlot>().isFilled)
        {
            skill_I.GetComponent<SkillUISample>().isEquiped = false;
            skill_I = null;
            Destroy(Islot.GetChild(0).gameObject);
        }
        skill_I = temp_skill;
        GameObject obj = Instantiate(skillIcon, Islot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill_I.GetComponent<SkillUISample>().skill.data.SkillIcon;
        obj.transform.SetAsFirstSibling();
        Islot.GetComponent<ActiveSkillSlot>().isFilled = true;
        temp_skill.GetComponent<SkillUISample>().isEquiped = true;
        
        obj.GetComponent<SkillIcon>().skill = skill_I;
        GameObject obj2 = Instantiate(skill_I.GetComponent<SkillUISample>().skillqicon.gameObject, iSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill_I.GetComponent<SkillUISample>().skill;
        obj2.GetComponent<SkillQuickIcon>().skillui = skill_I.GetComponent<SkillUISample>();
        sksystem.iSkill = skill_I.GetComponent<SkillUISample>().skill.gameObject;


        gameObject.SetActive(false);
    }
    
    public void OnExitBtn()
    {
        gameObject.SetActive(false);
    }

    public void OnQSkillSlotClicked()
    {
        if(!Qslot.GetComponent<ActiveSkillSlot>().isFilled)
        {
            return;
        }
        clearWindow.SetActive(true);
        clearWindow.GetComponent<SkillClearWindow>().isQslot = true;
    }


    public void OnISkillSlotClicked()
    {
        if(!Islot.GetComponent<ActiveSkillSlot>().isFilled)
        {
            return;
        }
        clearWindow.SetActive(true);
        clearWindow.GetComponent<SkillClearWindow>().isQslot = false;
    }
    public void ClearQSkill()
    {
        skill_Q.GetComponent<SkillUISample>().isEquiped = false;
        Qslot.GetComponent<ActiveSkillSlot>().isFilled = false;
        skill_Q = null;
        sksystem.qSkill = null;
        Destroy(Qslot.GetChild(0).gameObject);
        Destroy(qSlot_inGame.GetChild(0).gameObject);
    }

    public void ClearISkill()
    {
        skill_I.GetComponent<SkillUISample>().isEquiped = false;
        Islot.GetComponent<ActiveSkillSlot>().isFilled = false;
        skill_I = null;
        sksystem.iSkill = null;
        Destroy(Islot.GetChild(0).gameObject);
        Destroy(iSlot_inGame.GetChild(0).gameObject);
    }

}
