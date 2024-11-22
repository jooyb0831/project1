using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public int damage;
    private SpriteAnimation sa;
    [SerializeField] Transform blowArea;
    [SerializeField] List<Sprite> explosionSprites;
    [SerializeField] List<Sprite> bombSprite;
    [SerializeField] bool isStart = true;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(bombSprite, 0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            BlowUp();
        }
        else
        {
            return;
        }
    }

    void BlowUp()
    {
        isStart = false;
        GetComponent<SpriteRenderer>().DOColor(Color.red, 0.3f).SetLoops(6, LoopType.Yoyo)
            .OnComplete(() =>
            {
                blowArea.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                sa.SetSprite(explosionSprites, 0.2f, false, DestroyBomb);
            });
    }

    void DestroyBomb()
    {
        blowArea.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Pooling.Instance.SetPool(DicKey.bomb, gameObject);
    }

    public void Initialize()
    {
        GetComponent<SpriteRenderer>().sprite = bombSprite[0];
        sa.SetSprite(bombSprite, 0.2f);
        transform.localPosition = Vector2.zero;
        isStart = true;
    }
}
