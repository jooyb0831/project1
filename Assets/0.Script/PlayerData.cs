using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int MAXHP { get; set; } = 30;
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }

    public float Speed { get; set; }

    public float RunSpeed { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        MAXHP = 30;
        HP = MAXHP;
        Speed = 4f;
        RunSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
