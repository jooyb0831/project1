using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatEnchantUI : MonoBehaviour
{
    [SerializeField] protected PlayerData pd;
    [SerializeField] protected EnchantSystem enchtSystem;
    [SerializeField] protected TMP_Text curTxt;
    [SerializeField] protected TMP_Text upTxt;
    [SerializeField] protected TMP_Text reqLvTxt;
    [SerializeField] protected TMP_Text coinTxt;
    [SerializeField] protected TMP_Text crystalTxt;

    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        pd = GameManager.Instance.PlayerData;
        enchtSystem = GameManager.Instance.EnchtSystem;
    }

    
}
