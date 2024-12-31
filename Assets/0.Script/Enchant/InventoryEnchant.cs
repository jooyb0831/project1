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
        SetData();
   
    }
    public void SetData()
    {
        curTxt.text = $"{enchtSystem.data.InvenEnData.CurSlotNum}";
        upTxt.text = $"{enchtSystem.data.InvenEnData.NextSlotNum}";
        reqLvTxt.text = $"필요 레벨 : {enchtSystem.data.InvenEnData.NeedLv}";
        coinTxt.text = $"{enchtSystem.data.InvenEnData.Gold}/{pd.Coin}";
        int crystalCnt = Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.CrystalIdx);
        crystalTxt.text = $"{enchtSystem.data.InvenEnData.CrystalNum}/{crystalCnt}";
        int itemCnt = Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.NeedItemIdx);
        itemTxt.text = $"{enchtSystem.data.InvenEnData.NeedItemNum}/{itemCnt}";
    }


    public void OnClicked()
    {
        if (enchtSystem.data.InvenEnData.Gold <= pd.Coin && enchtSystem.data.InvenEnData.NeedLv <= pd.Level
            && Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.CrystalIdx) >= enchtSystem.data.InvenEnData.CrystalNum
            && Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.NeedItemIdx) >= enchtSystem.data.InvenEnData.NeedItemNum
            && enchtSystem.data.InvenEnData.CurSlotNum<10)
        {
            //인벤토리 늘리는 코드
            Inventory.Instance.inventoryData.curInvenNums = enchtSystem.data.InvenEnData.NextSlotNum;
            
            //인벤토리 세팅
            InventoryUI.Instance.Init();

            //아이템 및 재화 사용처리
            pd.Coin -= enchtSystem.data.InvenEnData.Gold;
            Inventory.Instance.Enchant(enchtSystem.data.InvenEnData.CrystalIdx, enchtSystem.data.InvenEnData.CrystalNum);
            Inventory.Instance.Enchant(enchtSystem.data.InvenEnData.NeedItemIdx, enchtSystem.data.InvenEnData.NeedItemNum);

            //다음단계 세팅
            enchtSystem.InvenEnchant();
            SetData();
        }

        else
        {
            if(enchtSystem.data.InvenEnData.Gold > pd.Coin)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(4);
                return;
            }

            else if(enchtSystem.data.InvenEnData.NeedLv > pd.Level)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(5);
                return;
            }
            
            else if(Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.CrystalIdx) < enchtSystem.data.InvenEnData.CrystalNum
                || Inventory.Instance.ItemCheck(enchtSystem.data.InvenEnData.NeedItemIdx) < enchtSystem.data.InvenEnData.NeedItemNum)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(6);
                return;
            }

            else if(enchtSystem.data.InvenEnData.CurSlotNum >= 10)
            {
                GameUI.Instance.fullInvenObj.SetActive(true);
                GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(8);
                return;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (EnchantUI.Instance.window.activeSelf)
        {
            SetData();
        }
    }
}
