using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : Singleton<BossUI>
{
    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text bossNameTxt;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] GameObject obj;
    public EnemyBoss boss;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUI()
    {
        bossNameTxt.text = boss.data.BossName;
        hpBarImg.fillAmount = ((float)boss.data.CURHP / boss.data.MAXHP);
        hpTxt.text = $"{boss.data.CURHP} / {boss.data.MAXHP}";
    }

    public void UIOn()
    {
        obj.SetActive(true);
    }

    public void UIOff()
    {
        obj.SetActive(false);
    }

    public int HP
    {
        set
        {
            if(boss == null)
            {
                return;
            }
            hpBarImg.fillAmount = ((float)boss.data.CURHP / boss.data.MAXHP);
            hpTxt.text = $"{boss.data.CURHP} / {boss.data.MAXHP}";
        }
    }

}
