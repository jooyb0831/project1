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
}
