using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public int usage;
    public FieldItem fieldItem;
}
public class FieldItem : MonoBehaviour
{
    public ItemData itemData;
    protected Player p;
    protected PlayerData pd;
    protected SpriteAnimation sa;
    bool isFind = false;
    [SerializeField] protected List<Sprite> itemSprites;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;

        if(itemSprites.Count >=1)
        {
            sa.SetSprite(itemSprites, 0.2f);
        }
    }

    public virtual void Init()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;

        if (itemSprites.Count >= 1)
        {
            sa.SetSprite(itemSprites, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector2.Distance(p.transform.position, transform.position);

        if(dist<=1.5f)
        {
            isFind = true;
        }
        if (isFind)
        {
            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * 5f);
        }
        else
        {
            return;
        }

        if (dist < 0.2f)
        {
            Inventory.Instance.GetItem(itemData);
            if(!isFull)
            {
                Destroy(gameObject);
            }
            else
            {
                return;
            }    
        }
    }

    public void ItemMoves()
    {

    }

    bool isFull = false;
    public void InvenFull()
    {
        bool isFull = true;
        Debug.Log("인벤토리가 가득 찼습니다.");
    }

    public virtual void Using()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        if (pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }
    }
}
