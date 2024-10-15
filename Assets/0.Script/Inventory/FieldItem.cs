using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string itemTitle;
    public Sprite[] itemSprites;
    public ItemType itemType;
    public Sprite invenIcon;
    public Sprite invenBGSprite;
    public int price;
    public int count;
    public int itemNumber;
}
public class FieldItem : MonoBehaviour
{
    [SerializeField] public ItemData itemData;
    private Player p;
    bool isFind = false;

    SpriteAnimation sa;
    [SerializeField] List<Sprite> itemSprites;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.Player;

        if(itemSprites!=null)
        {
            sa.SetSprite(itemSprites, 0.2f);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (isFind == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * 5f);

            float dist = Vector2.Distance(p.transform.position, transform.position);
            if (dist < 0.2f)
            {
                Inventory.Instance.GetItem(itemData);
                Destroy(gameObject);
            }
        }

        else
        {
            float dist = Vector2.Distance(p.transform.position, transform.position);
            if (dist <= 1f)
            {
                isFind = true;
            }
        }
    }
}
