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
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(bombSprite, 0.2f);
        BlowUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BlowUp()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.red, 0.3f).SetLoops(6, LoopType.Yoyo)
            .OnComplete(() =>
            {
                blowArea.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                sa.SetSprite(explosionSprites, 0.2f, false, DestroyBomb);
            });
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
