using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [SerializeField] Player p;
    [SerializeField] PlayerData pd;
    public bool isFind = false;
    [SerializeField] int plusHP;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.player;
        pd = GameManager.Instance.playerData;
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.player;
            return;
        }
        if(pd == null)
        {
            pd = GameManager.Instance.playerData;
            return;
        }
        float dist = Vector2.Distance(transform.position, p.transform.position);
        if (dist < 2f)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(pd.HP == pd.MAXHP)
                {
                    Debug.Log("이미 체력이 가득 찼습니다");
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
    }
}
