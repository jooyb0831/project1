using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Player p;
    private SceneType type;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        type = SceneChanger.Instance.sceneType;
    }

    // Update is called once per frame
    void Update()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }
        Vector3 vec = p.transform.position;
        vec.z = -10f;
        float y = p.transform.position.y + 1.5f;

        if (type.Equals(SceneType.Ship))
        {

            float clampX = Mathf.Clamp(vec.x, -14.08f, 10.15f);
            transform.position = new Vector3(clampX, y, vec.z);
        }
        else
        {
            float x = p.transform.position.x;
            transform.position = new Vector3(x, y, vec.z);
        }


    }
}
