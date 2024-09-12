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
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        
        sa.SetSprite(pBulletSprite, 0.1f, false);
        Invoke("LastSprite", 0.25f);
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Destroy(gameObject);
        }
    }

    void LastSprite()
    {
        sa.isStop = true;
        transform.GetComponent<SpriteRenderer>().sprite = last[0];
    }
}
