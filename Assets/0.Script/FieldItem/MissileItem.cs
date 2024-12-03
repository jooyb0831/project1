using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItem : FieldItem
{
    [SerializeField] GameObject arrow;
    float arrowSpeed = 50f;
    float deg;
    float circleR = 0.4f;
    bool isUp = true;
    GameObject arrowObj = null;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }
    public override void Using()
    {
        base.Using();
        if(arrowObj == null)
        {
            // 화살표 생성
            arrowObj = Instantiate(arrow, p.transform);
            arrowObj.transform.rotation = Quaternion.identity;
        }
    }

    public void FireReady()
    {

        // 발사 준비
        if (isUp)
        {
            deg += Time.deltaTime * arrowSpeed;
            if (arrowObj.transform.rotation == Quaternion.Euler(0, 0, 90f))
            {
                isUp = false;
            }
        }
        else
        {
            deg -= Time.deltaTime * arrowSpeed;
            if (arrowObj.transform.rotation == Quaternion.Euler(0, 0, 0))
            {
                isUp = true;
            }
        }
        float rad = deg * Mathf.Deg2Rad;
        float x = circleR * Mathf.Cos(rad);
        float y = circleR * Mathf.Sin(rad);
        arrowObj.transform.localPosition = new Vector2(x, y);
        arrowObj.transform.eulerAngles = new Vector3(0, 0, deg);
    }

    public void Fire()
    {
        // 발사
        p.state = Player.State.Idle;
        float bulletSpeed = 2f;
        float power = 5f;
        Vector2 dir = arrowObj.transform.rotation * new Vector2(bulletSpeed, 0) * power;
        GameObject obj = Pooling.Instance.GetPool(DicKey.pMissile, arrowObj.transform, arrowObj.transform.rotation).gameObject;

        if (p.transform.localScale.x < 0)
        {
            obj.GetComponent<Missile>().Shoot(-dir);
        }
        else
        {
            obj.GetComponent<Missile>().Shoot(dir);
        }
        
        Destroy(arrowObj);
        Initialize();
    }

    void Initialize()
    {
        isUp = true;
        deg = 0;
    }
}
