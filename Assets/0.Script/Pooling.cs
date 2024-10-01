using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DicKey
{
    pBullet,
    eBullet2
}
public class Pooling : Singleton<Pooling>
{
    private Queue<PBullet> pbQueue = new Queue<PBullet>();
    private Queue<EBullet2> eb2Queue = new Queue<EBullet2>();

    public PBullet pBullet;
    [SerializeField] EBullet2 eBullet2;

    private Dictionary<DicKey, Queue<GameObject>> pool = new Dictionary<DicKey, Queue<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        pool.Add(DicKey.pBullet, new Queue<GameObject>());
        pool.Add(DicKey.eBullet2, new Queue<GameObject>());
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
                case DicKey.eBullet2:
                    {
                        obj = Instantiate(eBullet2, trans.position, Quaternion.identity).gameObject;
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
}
