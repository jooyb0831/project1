using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenItemOption : MonoBehaviour
{
    public InvenItem item;
    [SerializeField] DumpWindowUI dumpWindowUI;

    [SerializeField]private GameObject canvas;
    private GraphicRaycaster rayCaster;
    private PointerEventData pointEventData;
    private EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.parent.gameObject;
        rayCaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if(Input.GetMouseButton(0))
        {
            Debug.Log(hit.collider.name);
            
            if(hit.collider!=null)
            {
                if (!hit.collider.CompareTag("InvenItemMenu"))
                {
                    Debug.Log("hit");
                    gameObject.SetActive(false);
                    if (hit.collider == null)
                    {
                        return;
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
            
        }
        */
        
        if(Input.GetMouseButtonDown(0))
        {
            pointEventData = new PointerEventData(eventSystem);
            pointEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            rayCaster.Raycast(pointEventData, results);
            GameObject hitObj = results[0].gameObject;

            if(!hitObj.CompareTag("InvenItemMenu"))
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void OnUseBtn()
    {
        Inventory.Instance.UseItem(item);
        transform.SetParent(item.transform);
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }
    
    public void OnEquipBtn()
    {
        Inventory.Instance.ItemEquip(item);
        transform.SetParent(item.transform);
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }

    public void OnExitBtn()
    {
        transform.SetParent(item.transform);
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }

    public void OnDeleteBtn()
    {
        DumpWindowUI dumpUI = Instantiate(dumpWindowUI, transform);
        dumpUI.SetItem(item);
        dumpUI.transform.SetParent(transform.parent);
        dumpUI.transform.localPosition = Vector2.zero;
        Destroy(gameObject);
    }

}
