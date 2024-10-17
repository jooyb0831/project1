using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    [SerializeField] Transform endZone;

    private SpriteAnimation sa;

    [SerializeField] List<Sprite> explosionSprites;
    [SerializeField] List<Sprite> sprite;
    public int damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(sprite, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == endZone)
        {
            // 리지드바디끄기
            float x = transform.position.x;
            transform.position = new Vector2(x, endZone.localPosition.y);
            sa.SetSprite(explosionSprites, 0.2f, false, Delete);
            
        }

    }

    void Delete()
    {
        Destroy(gameObject);
    }

}
