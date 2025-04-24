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

    #region 클래스 및 컴포넌트 변수
    protected Player p;
    protected PlayerData pd;
    protected Inventory inven;
    protected GameUI gameUI;
    protected SpriteAnimation sa;
    #endregion

    //아이템 찾았는지 여부 체크할 Bool값
    private bool isFind = false;

    //아이템의 이동속도 변수
    private float speed;

    //아이템의 이미지 Sprite 리스트
    [SerializeField] protected List<Sprite> itemSprites;

    //인벤토리가 찼는지를 받는 변수 Bool값
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
    /// 아이템 이동
    /// </summary>
    void ItemMove()
    {
        //거리 계산
        float dist = Vector2.Distance(p.transform.position, transform.position);

        //거리가 1.5미만일 경우 
        if (dist <= 1.5f)
        {
            //인벤이 가득 차지 않았을 경우 아이템이 찾아진 것으로 체크
            isFind = !isFull;
        }

        //아이템이 찾아진 경우 speed 5, 아닐 경우 0(정지)
        speed = isFind ? 5f : 0f;

        //아이템 이동
        transform.position = Vector2.MoveTowards(transform.position,
                                                p.transform.position,
                                                Time.deltaTime * speed);

        //거리가 0.2미만이면 인벤토리에 추가
        if (dist < 0.2f)
        {
            inven.GetItem(itemData);
        }
    }

    /// <summary>
    /// 아이템이 가득 찼는지 여부 받는 함수
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
