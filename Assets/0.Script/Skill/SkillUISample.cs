using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUISample : MonoBehaviour
{
    protected SkillSystem sksystem;
    protected PlayerData pData;
    public Skill skill;
    [SerializeField] protected GameObject equipWindow;
    [SerializeField] protected TMP_Text skillTitleTxt;
    [SerializeField] protected TMP_Text skillExplainTxt;
    [SerializeField] protected TMP_Text skillExplainTxt2;
    [SerializeField] protected Image icon;
    [SerializeField] protected GameObject unlockCover;
    [SerializeField] protected TMP_Text unlockTxt;
    [SerializeField] protected GameObject usingCover;
    public bool isUnlock = false;
    public bool isEquiped = false;
    public GameObject skillUIicon;
    
    public SkillQuickIcon skillqicon;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        pData = GameManager.Instance.PlayerData;
        sksystem = GameManager.Instance.SkSystem;

    }

    /// <summary>
    /// 스킬 클릭 버튼을 눌렀을 때 작동하는 함수
    /// </summary>
    public void OnClickBtn()
    {
        if(!isUnlock)
        {
            return;
        }
        equipWindow.SetActive(true);
        equipWindow.GetComponent<SkillEquipWindow>().skillIcon = skillUIicon;
        /*
        equipWindow.GetComponent<SkillEquipWindow>().icon_Q.GetComponent<Image>().sprite = skill.data.SkillIcon;
        equipWindow.GetComponent<SkillEquipWindow>().icon_I.GetComponent<Image>().sprite = skill.data.SkillIcon;
        */
        equipWindow.GetComponent<SkillEquipWindow>().temp_skill = this.gameObject;
        equipWindow.transform.position = transform.position;
    }
}
