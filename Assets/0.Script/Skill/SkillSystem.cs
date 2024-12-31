using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Skill[] skills;
    public SkillUISample[] skillUIs;
    public GameObject qSkill;
    public GameObject iSkill;
    public SkillUISample qSkillUI;
    public SkillUISample iSkillUI;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(qSkill);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            UseSkill(iSkill);
        }
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <param name="skillObj"></param>
    void UseSkill(GameObject skillObj)
    {
        Skill skill = skillObj.GetComponent<Skill>();
        if (skillObj == null)
        {
            return;
        }
        if (skill.GetComponent<Skill>().isStart || skill.GetComponent<Skill>().isWorking)
        {
            return;
        }
        skill.GetComponent<Skill>().isStart = true;
    }


    /// <summary>
    /// 스킬 세팅
    /// </summary>
    public void SetSkill()
    {
        if(qSkill!=null)
        {
            if (qSkill.GetComponent<Skill>().isSet)
            {
                SkillUISystem.Instance.SetQSkill(qSkill.GetComponent<Skill>());
            }
        }

        if(iSkill!=null)
        {
            if (iSkill.GetComponent<Skill>().isSet)
            {
                SkillUISystem.Instance.SetISkill(iSkill.GetComponent<Skill>());
            }
        }

    }

    /// <summary>
    /// 스킬UI 세팅
    /// </summary>
    public void SetSkillUI()
    {
        for(int i = 0; i<skills.Length; i++)
        {
            skills[i].ui = skillUIs[i];
        }
    }
}
