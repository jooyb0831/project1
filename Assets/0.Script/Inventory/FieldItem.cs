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
    public GameObject obj;
}
public class FieldItem : MonoBehaviour
{
    public ItemData itemData;
    protected Player p;
    protected PlayerData pd;
    protected SpriteAnimation sa;
    bool isFind = false;
    float speed;
    [SerializeField] protected List<Sprite> itemSprites;
    bool isFull = false;

    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        itemData.obj = this.gameObject;
        if (itemSprites.Count >= 1)
        {
            sa.SetSprite(itemSprites, 0.2f);
        }
    }

    void Update()
    {

        float dist = Vector2.Distance(p.transform.position, transform.position);

        if (dist<=1.5f)
        {
            if(!isFull)
            {
                isFind = true;
            }
            else
            {
                isFind = false;
            }
        }

        speed = isFind ? 5f :0f;
        transform.position = Vector2.MoveTowards(transform.position, p.transform.position, Time.deltaTime * speed);

        if (dist < 0.2f)
        {
            Inventory.Instance.GetItem(itemData);
        }


    }

    public void InvenFull(bool invenFull)
    {
        isFull = invenFull;
        if (invenFull)
        {
            GameUI.Instance.fullInvenObj.SetActive(true);
            GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(0);
        }
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
