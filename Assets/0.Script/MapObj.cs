using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapObj : MonoBehaviour
{
    private Player p;
    [SerializeField] GameObject textObj;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, p.transform.position);

        if(dist<1.5f)
        {
            textObj.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                MoveScene();
            }
        }
        else
        {
            textObj.SetActive(false);
        }
    }

    void MoveScene()
    {
        SceneChanger.Instance.MapSelect();
    }
}
