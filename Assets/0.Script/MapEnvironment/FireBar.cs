using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBar : MonoBehaviour
{
    private SpriteAnimation sa;
    public int damage = 10;
    [SerializeField] List<Sprite> fireSprites; 
    // Start is called before the first frame update
    void Start()
    {

        sa=GetComponent<SpriteAnimation>();
        sa.SetSprite(fireSprites, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
