using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : Singleton<StatUI>
{ 
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] TMP_Text speedTxt;
    [SerializeField] TMP_Text goldTxt;


    private PlayerData pd;

    public int Level
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            levelTxt.text = $"Lv. {pd.Level}";
        }
    }
    public int HP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            hpTxt.text = $"HP : {pd.MAXHP}";
        }
    }
    public int Speed
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            speedTxt.text = $"SPEED : {pd.Speed}";
        }
    }

    public int Gold
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            goldTxt.text = $"GOLD : {pd.Coin}";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        levelTxt.text = $"Lv. {pd.Level}";
        hpTxt.text = $"HP : {pd.MAXHP}";
        speedTxt.text = $"SPEED : {pd.Speed}";
        goldTxt.text = $"GOLD : {pd.Coin}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
