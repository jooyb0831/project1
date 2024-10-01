using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet2 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float time;
    [SerializeField] List<Sprite> eBullet2Sprites;
    private SpriteAnimation sa;
    public bool isRight = false;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        SpriteManager.EnemyBulletSprites eBulletSprite = SpriteManager.Instance.enemyBulletSprites[0];
        this.eBullet2Sprites = eBulletSprite.eBullet2Sprites;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight == true)
        {
            transform.localScale = new Vector2(-5f, 5f);
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (isRight == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        sa.SetSprite(eBullet2Sprites, 0.05f);


        
        timer += Time.deltaTime;
        if(timer>=time)
        {
            Pooling.Instance.SetPool(DicKey.eBullet2, gameObject);
            timer = 0;

        }
        
    }

    public void Initialize()
    {
        timer = 0;
        transform.localScale = new Vector2(5f, 5f);
        isRight = false;
    }

}
