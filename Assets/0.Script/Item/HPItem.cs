using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [SerializeField] Player p;
    [SerializeField] PlayerData pd;
    [SerializeField] GameObject text;
    public bool isFind = false;
    [SerializeField] int plusHP;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
            return;
        }
        if(pd == null)
        {
            pd = GameManager.Instance.PlayerData;
            return;
        }
        float dist = Vector2.Distance(transform.position, p.transform.position);
        if (dist < 2f)
        {
            text.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(pd.HP == pd.MAXHP)
                {
                    Debug.Log("�̹� ü���� ���� á���ϴ�");
                    return;
                }
                else
                {
                    if (pd.MAXHP - pd.HP >= plusHP)
                    {
                        pd.HP += plusHP;
                    }
                    else if (pd.MAXHP - pd.HP < plusHP)
                    {
                        pd.HP = pd.MAXHP;
                    }
                    Destroy(gameObject);
                }
                
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
