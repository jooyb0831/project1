using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Skill[] skills;
    public GameObject qSkill;
    public GameObject iSkill;
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
            if(qSkill == null)
            {
                Debug.Log("장착된 스킬이 없습니다.");
                return;
            }
            if(qSkill.GetComponent<Skill>().isStart || qSkill.GetComponent<Skill>().isWorking)
            {
                return;
            }
            qSkill.GetComponent<Skill>().isStart = true;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (iSkill == null)
            {
                Debug.Log("장착된 스킬이 없습니다.");
                return;
            }
            if (iSkill.GetComponent<Skill>().isStart || iSkill.GetComponent<Skill>().isWorking)
            {
                return;
            }
            iSkill.GetComponent<Skill>().isStart = true;
        }
    }

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
}
