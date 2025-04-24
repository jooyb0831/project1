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
    public Transform questArea;
    private PlayerData pd;

    public GameObject gameOverUI;

    public GameObject fullInvenObj;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;

        lvTxt.text = $"Lv.{pd.Level}";
        hpBarImg.fillAmount = (float)((float)pd.HP / (float)pd.MAXHP);
        hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        expBarImg.fillAmount = ((float)pd.EXP / pd.MAXEXP);
        expTxt.text = $"{pd.EXP}/{pd.MAXEXP}";
        coinTxt.text = pd.Coin.ToString();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        BossUI.Instance.UIOff();
        gameOverUI.GetComponent<GameOverUI>().Active();
    }

    public int MAXHP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        }
    }

    public int HP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
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
                pd = GameManager.Instance.PlayerData;
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
                pd = GameManager.Instance.PlayerData;
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
                pd = GameManager.Instance.PlayerData;
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
                pd = GameManager.Instance.PlayerData;
                return;
            }
            coinTxt.text = pd.Coin.ToString();
        }
    }

    public void OnQuitBtn()
    {
        Time.timeScale = 1;
        SceneChanger.Instance.Title();
    }
}
