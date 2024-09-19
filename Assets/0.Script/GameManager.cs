using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Player p;
    public Player player
    {
        get
        {
            if(p == null)
            {
                p = FindAnyObjectByType<Player>();
            }
            return p;
        }
    }

    private PlayerData pData;
    public PlayerData playerData
    {
        get
        {
            if(pData == null)
            {
                pData = FindAnyObjectByType<PlayerData>();
            }
            return pData;
        }
    }

    private Quest q;
    public Quest quest
    {
        get
        {
            if(q == null)
            {
                q = FindAnyObjectByType<Quest>();
            }
            return q;
        }

    }
}
