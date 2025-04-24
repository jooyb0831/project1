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

    #region Ŭ���� �� ������Ʈ ����
    protected Player p;
    protected PlayerData pd;
    protected Inventory inven;
    protected GameUI gameUI;
    protected SpriteAnimation sa;
    #endregion

    //������ ã�Ҵ��� ���� üũ�� Bool��
    private bool isFind = false;

    //�������� �̵��ӵ� ����
    private float speed;

    //�������� �̹��� Sprite ����Ʈ
    [SerializeField] protected List<Sprite> itemSprites;

    //�κ��丮�� á������ �޴� ���� Bool��
    private bool isFull = false;

    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        sa = GetComponent<SpriteAnimation>();
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        gameUI = GameManager.Instance.GameUI;

        itemData.obj = this.gameObject;
        if (itemSprites.Count >= 1)
        {
            sa.SetSprite(itemSprites, 0.2f);
        }
    }

    void Update()
    {
        ItemMove();
    }

    /// <summary>
    /// ������ �̵�
    /// </summary>
    void ItemMove()
    {
        //�Ÿ� ���
        float dist = Vector2.Distance(p.transform.position, transform.position);

        //�Ÿ��� 1.5�̸��� ��� 
        if (dist <= 1.5f)
        {
            //�κ��� ���� ���� �ʾ��� ��� �������� ã���� ������ üũ
            isFind = !isFull;
        }

        //�������� ã���� ��� speed 5, �ƴ� ��� 0(����)
        speed = isFind ? 5f : 0f;

        //������ �̵�
        transform.position = Vector2.MoveTowards(transform.position,
                                                p.transform.position,
                                                Time.deltaTime * speed);

        //�Ÿ��� 0.2�̸��̸� �κ��丮�� �߰�
        if (dist < 0.2f)
        {
            inven.GetItem(itemData);
        }
    }

    /// <summary>
    /// �������� ���� á���� ���� �޴� �Լ�
    /// </summary>
    /// <param name="invenFull"></param>
    public void InvenFull(bool invenFull)
    {
        isFull = invenFull;
        if (invenFull)
        {
            gameUI.fullInvenObj.SetActive(true);
            gameUI.fullInvenObj.GetComponent<FullInvenObj>().Act(0);
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
