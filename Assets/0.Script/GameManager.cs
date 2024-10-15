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
    public Player Player
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
    public PlayerData PlayerData
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

    private QuestManager qm;
    public QuestManager QuestManager
    {
        get
        {
            if(qm == null)
            {
                qm = FindAnyObjectByType<QuestManager>();
            }
            return qm;
        }

    }

    private GameUI gui;
    public GameUI GameUI
    {
        get
        {
            if(gui == null)
            {
                gui = FindAnyObjectByType<GameUI>();
            }
            return gui;
        }
    }

    private MenuUI mui;
    public MenuUI MUI
    {
        get
        {
            if(mui == null)
            {
                mui = FindAnyObjectByType<MenuUI>();
            }
            return mui;
        }
    }

    private Inventory inven;
    public Inventory Inventory
    {
        get
        {
            if(inven = null)
            {
                inven = FindAnyObjectByType<Inventory>();
            }
            return inven;
        }
    }
}
