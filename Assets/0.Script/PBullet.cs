using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float time;
    [SerializeField] List <Sprite> last;
    private SpriteAnimation sa;
    public bool isRight = true;
    
    private List<Sprite> pBulletSprite;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        SpriteManager.PlayerBulletSprite pBulletSprite = SpriteManager.Instance.pBulletSprite[0];
        this.pBulletSprite = pBulletSprite.pBulletSprites;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight == true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (isRight == false)
        {
            transform.localScale = new Vector2(-5f, 5f);
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        
        sa.SetSprite(pBulletSprite, 0.1f, false);
        
        //Invoke("LastSprite", 0.25f);
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Pooling.Instance.SetPool(DicKey.pBullet, gameObject);
            timer = 0;
        }
        
    }

    void LastSprite()
    {
        sa.isStop = true;
        transform.GetComponent<SpriteRenderer>().sprite = last[0];
    }

    public void Initialize()
    {
        isRight = true;
        transform.localScale = new Vector2(5f, 5f);
        timer = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Boss3>())
        {
            collision.GetComponent<Boss3>().BossHit();
        }
    }
}
