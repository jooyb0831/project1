using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] List<Sprite> sprite;
    [SerializeField] bool isOpen = false;
    private SpriteAnimation sa;
    private Player p;
    private List<Sprite> boxSprites;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.player;
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(sprite, 0.2f);
        boxSprites = SpriteManager.Instance.boxSprites;

    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.player;
        }

        float dist = Vector2.Distance(transform.position, p.transform.position);

        if(dist<2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = true;
                OpenBox();
            }
        }

    }

    void OpenBox()
    {
        sa.SetSprite(boxSprites, 0.2f, false, DestroyBox);
        item.transform.SetParent(null);
        item.gameObject.SetActive(true);
       
    }

    void DestroyBox()
    {
        Destroy(gameObject);
    }
}
