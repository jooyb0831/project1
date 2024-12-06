using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantData
{
    public class HPEnchantData
    {
        public int EnchantLV { get; set; } = 1;
        public int NextHP { get; set; } = 40;
        public int NeedLv { get; set; } = 3;
        public int Gold { get; set; } = 15;

        public int CrystalIdx { get; set; } = 0;
        public int CrystalNum { get; set; } = 1;
    }

    public class ATKEnchantData
    {
        public int BasicAttackEnchantLV { get; set; } = 1;
        public int NextATK { get; set; }
        public int NeedLv { get; set; } = 4;
        public int Gold { get; set; } = 20;
        public int CrystalIdx { get; set; } = 1;
        public int CrystalNum { get; set; } = 1;
    }

    public class InvenEnchatData
    {
        public int CurSlotNum { get; set; }
        public int NextSlotNum { get; set; }
        public int NeedLv { get; set; } = 7;
        public int Gold { get; set; } = 15;
        public int CrystalIdx { get; set; } = 0;
        public int CrystalNum { get; set; } = 2;
        public int NeedItemIdx { get; set; } = 4;
        public int NeedItemNum { get; set; } = 1;
    }

    public int SpeedEnchantLV { get; set; }
    public int BasicAttackEnchantLV { get; set; }
    public int AttackSpeedEnchantLV { get; set; }

    public HPEnchantData HPdata = new HPEnchantData();

    public ATKEnchantData ATKdata = new ATKEnchantData();

    public InvenEnchatData InvenEnData = new InvenEnchatData();
}
public class EnchantSystem : Singleton<EnchantSystem>
{
    private PlayerData pd;
    public int HP;
    public EnchantData data = new EnchantData();


    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
        data.ATKdata.BasicAttackEnchantLV = pd.AttackDamage;
        data.ATKdata.NextATK = data.ATKdata.BasicAttackEnchantLV += 3;
        data.InvenEnData.CurSlotNum = Inventory.Instance.inventoryData.curInvenNums;
        data.InvenEnData.NextSlotNum = data.InvenEnData.CurSlotNum + 1;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log($"{data.InvenEnData.CurSlotNum}");
        }
    }

    public void HPEnchant()
    {
        data.HPdata.EnchantLV += 1;
        data.HPdata.NextHP += 10;
        data.HPdata.NeedLv += 1;
        data.HPdata.Gold += 25;

        if(data.HPdata.EnchantLV > 5)
        {
            data.HPdata.CrystalNum = 2;
        }
    }

    public void ATKEnchant()
    {
        data.ATKdata.BasicAttackEnchantLV += 1;
        data.ATKdata.NextATK += 3;
        data.ATKdata.NeedLv += 1;
        data.ATKdata.Gold += 30;

        if(data.ATKdata.BasicAttackEnchantLV >5)
        {
            data.ATKdata.CrystalNum = 2;
        }
    }

    public void InvenEnchant()
    {
        data.InvenEnData.CurSlotNum += 1;
        data.InvenEnData.NextSlotNum += 1;
        data.InvenEnData.NeedLv += 2;
        data.InvenEnData.Gold += 10;
        data.InvenEnData.NeedItemNum += 1;
    }
}
