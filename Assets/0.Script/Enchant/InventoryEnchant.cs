using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryEnchant : StatEnchantUI
{
    [SerializeField] TMP_Text itemTxt;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
   
    }
    public void SetData()
    {
        curTxt.text = $"{enchtSystem.data.InvenEnData.BasicSlotNum}";
        upTxt.text = $"{enchtSystem.data.InvenEnData.NextSlotNum}";
        reqLvTxt.text = $"필요 레벨 : {enchtSystem.data.InvenEnData.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.InvenEnData.Gold}/{pd.Coin}";
        int crystalCnt = Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.CrystalIdx);
        crystalTxt.text = $"{enchtSystem.data.InvenEnData.CrystalNum}/{crystalCnt}";
        int itemCnt = Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.NeedItemIdx);
        itemTxt.text = $"{enchtSystem.data.InvenEnData.NeedItemNum} / {itemCnt}";
    }


    public void OnClicked()
    {
        if (enchtSystem.data.InvenEnData.Gold <= pd.Coin && enchtSystem.data.InvenEnData.NeedLv <= pd.Level
            && Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.CrystalIdx) >= enchtSystem.data.InvenEnData.CrystalNum
            && Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.NeedItemIdx) >= enchtSystem.data.InvenEnData.NeedItemNum)
        {
          //Inventory.Instance.
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
