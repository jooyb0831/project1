using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    // Sprite를 관리해주는 스크립트

    [System.Serializable]
    public class PlayerSprite
    {
        public List<Sprite> idleSprites;
        public List<Sprite> walkSprites;
        public List<Sprite> attackSprites;
        public List<Sprite> runAttackSprites;
        public List<Sprite> jumpSprites;
        public List<Sprite> hurtSprites;
        public List<Sprite> slideSprites;
        public List<Sprite> sitSprites;
        public List<Sprite> sitAttackSprites;
        public List<Sprite> deadSprites;
        public List<Sprite> ladderIdleSprites;
        public List<Sprite> updownSprites;
        public List<Sprite> bombSprites;

    }
    public PlayerSprite[] playerSprite;

    [System.Serializable]
    public class PlayerBulletSprite
    {
        public List<Sprite> pBulletSprites;
    }
    public PlayerBulletSprite[] pBulletSprite;

    [System.Serializable]
    public class EnemySprite
    {
        public List<Sprite> idleSprite;
        public List<Sprite> moveSprite;
        public List<Sprite> deadSprite;
        public List<Sprite> attackSprite;
    }
    public EnemySprite[] enemySprite;

    public List<Sprite> coinSprites;


    [System.Serializable]
    public class EnemyBulletSprites
    {
        public List<Sprite> eBullet2Sprites;
    }

    public EnemyBulletSprites[] enemyBulletSprites;

    [System.Serializable]
    public class BossSprite
    {
        public List<Sprite> idleSprite;
        public List<Sprite> walkSprite;
        public List<Sprite> attack1Sprites;
        public List<Sprite> attack2Sprites;
        public List<Sprite> hitSprite;
        public List<Sprite> deadSprite;
    }
    public BossSprite[] bossSprite;

    public List<Sprite> boxSprites;

    public List<Sprite> merchantSprites;

    public List<Sprite> enchantNPCSprites;

    public List<Sprite> questNPCSprites;

    public List<Sprite> fireBallSprites;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
