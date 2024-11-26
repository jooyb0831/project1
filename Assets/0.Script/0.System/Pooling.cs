using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DicKey
{
    pBullet,
    pMissile,
    eBullet,
    eBullet2,
    fireball,
    boss2Bullet,
    bomb,
    fallSnowBall
}
public class Pooling : Singleton<Pooling>
{
    private Queue<PBullet> pbQueue = new Queue<PBullet>();
    private Queue<EBullet> ebQueue = new Queue<EBullet>();
    private Queue<EBullet2> eb2Queue = new Queue<EBullet2>();
    private Queue<Missile> pMsQueue = new Queue<Missile>();
    private Queue<FireBall> fireBallQueue = new Queue<FireBall>();
    private Queue<Boss2Bullet> boss2BulletQueue = new Queue<Boss2Bullet>();
    private Queue<Bomb> bombQueue = new Queue<Bomb>();
    private Queue<FallSnowBall> snowBallQueue = new Queue<FallSnowBall>();

    public PBullet pBullet;
    [SerializeField] EBullet eBullet;
    [SerializeField] EBullet2 eBullet2;
    [SerializeField] Missile pMissile;
    [SerializeField] FireBall fireball;
    [SerializeField] Boss2Bullet boss2Bullet;
    [SerializeField] Bomb bomb;
    [SerializeField] FallSnowBall fallSnowBall;

    private Dictionary<DicKey, Queue<GameObject>> pool = new Dictionary<DicKey, Queue<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        pool.Add(DicKey.pBullet, new Queue<GameObject>());
        pool.Add(DicKey.eBullet, new Queue<GameObject>());
        pool.Add(DicKey.eBullet2, new Queue<GameObject>());
        pool.Add(DicKey.pMissile, new Queue<GameObject>());
        pool.Add(DicKey.fireball, new Queue<GameObject>());
        pool.Add(DicKey.boss2Bullet, new Queue<GameObject>());
        pool.Add(DicKey.bomb, new Queue<GameObject>());
        pool.Add(DicKey.fallSnowBall, new Queue<GameObject>());
        
    }

    public void SetPool(DicKey key, GameObject obj)
    {
        switch(key)
        {
            case DicKey.pBullet:
                {
                    obj.GetComponent<PBullet>().Initialize();
                }
                break;
            case DicKey.eBullet:
                {
                    obj.GetComponent<EBullet>().Initialize();
                }
                break;
            case DicKey.eBullet2:
                {
                    obj.GetComponent<EBullet2>().Initialize();
                }
                break;
            case DicKey.pMissile:
                {
                    obj.GetComponent<Missile>().Initialize();
                }
                break;
            case DicKey.fireball:
                {
                    obj.GetComponent<FireBall>().Initialize();
                }
                break;
            case DicKey.boss2Bullet:
                {
                    obj.GetComponent<Boss2Bullet>().Initialize();
                }
                break;
            case DicKey.bomb:
                {
                    obj.GetComponent<Bomb>().Initialize();
                }
                break;
            case DicKey.fallSnowBall:
                {
                    obj.GetComponent<FallSnowBall>().Initialize();
                }
                break;
        }
        obj.gameObject.SetActive(false);
        pool[key].Enqueue(obj);
        Debug.Log("s");
    }

    public GameObject GetPool(DicKey key, Transform trans = null)
    {
        GameObject obj = null;
        if(pool[key].Count == 0)
        {
            switch(key)
            {
                case DicKey.pBullet:
                    {
                        obj = Instantiate(pBullet, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.eBullet:
                    {
                        obj = Instantiate(eBullet, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.eBullet2:
                    {
                        obj = Instantiate(eBullet2, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.fireball:
                    {
                        obj = Instantiate(fireball, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.bomb:
                    {
                        obj = Instantiate(bomb, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.fallSnowBall:
                    {
                        obj = Instantiate(fallSnowBall, trans.position, Quaternion.identity).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
            }
        }
        obj = pool[key].Dequeue();
        obj.transform.localPosition = trans.position;
        //obj.transform.localRotation = trans.rotation;
        obj.SetActive(true);
        return obj;
    }

    public GameObject GetPool(DicKey key, Transform trans, Quaternion rotate)
    {
        GameObject obj = null;
        if(pool[key].Count == 0)
        {
            switch(key)
            {
                case DicKey.pMissile:
                    {
                        obj = Instantiate(pMissile, trans.position, rotate).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.boss2Bullet:
                    {
                        obj = Instantiate(boss2Bullet, trans.position, rotate).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
            }
        }
        obj = pool[key].Dequeue();
        obj.transform.localPosition = trans.position;
        obj.SetActive(true);
        return obj;
    }
}
