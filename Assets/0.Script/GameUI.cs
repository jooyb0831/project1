using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text hpTxt;

    [SerializeField] TMP_Text lvTxt;
    [SerializeField] Image expBarImg;
    [SerializeField] TMP_Text expTxt;

    [SerializeField] TMP_Text coinTxt;

    private PlayerData pd;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.playerData;

        lvTxt.text = $"Lv.{pd.Level}";
        hpBarImg.fillAmount = (float)((float)pd.HP / (float)pd.MAXHP);
        hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        expBarImg.fillAmount = ((float)pd.EXP / pd.MAXEXP);
        expTxt.text = $"{pd.EXP}/{pd.MAXEXP}";
        coinTxt.text = pd.Coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int HP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.playerData;
                return;
            }
            hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        }
    }
    

    public int EXP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.playerData;
                return;
            }
            expBarImg.fillAmount = ((float)pd.EXP / pd.MAXEXP);
            expTxt.text = $"{pd.EXP}/{pd.MAXEXP}";
        }
    }

    public int MAXEXP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.playerData;
                return;
            }
            expBarImg.fillAmount = ((float)pd.EXP / pd.MAXEXP);
            expTxt.text = $"{pd.EXP}/{pd.MAXEXP}";
        }
    }

    public int Lv
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.playerData;
                return;
            }
            lvTxt.text = $"Lv.{pd.Level}";
        }
    }

    public int Coin
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.playerData;
                return;
            }
            coinTxt.text = pd.Coin.ToString();
        }
    }
}
