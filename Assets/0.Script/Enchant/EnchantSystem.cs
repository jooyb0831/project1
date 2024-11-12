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
        public int CyrstalNum { get; set; } = 1;

    }
    public int SpeedEnchantLV { get; set; }
    public int BasicAttackEnchantLV { get; set; }
    public int AttackSpeedEnchantLV { get; set; }

    public HPEnchantData HPdata = new HPEnchantData();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HPEnchant()
    {
        data.HPdata.EnchantLV += 1;
        data.HPdata.NextHP += 10;
        data.HPdata.NeedLv += 1;
        data.HPdata.Gold += 30;

        if(data.HPdata.EnchantLV > 5)
        {
            data.HPdata.CyrstalNum = 2;
        }
    }
}
