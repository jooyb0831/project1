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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(qSkill.GetComponent<Skill>().isStart || qSkill.GetComponent<Skill>().isWorking)
            {
                return;
            }
            qSkill.GetComponent<Skill>().isStart = true;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(iSkill.GetComponent<Skill>().isStart || iSkill.GetComponent<Skill>().isWorking)
            {
                return;
            }
            iSkill.GetComponent<Skill>().isStart = true;
        }
    }
}
