using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    [Tooltip("Whether the projectile is using 'pooling', or not")]
    public bool isPooled;

    private GameObject bossDisplay;

    // 弾の衝突判定
    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        // 敵の弾がプレイヤーに当たった場合
        if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
                Destruction();
        }
        // プレイヤーの弾が敵に当たった場合
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            //Debug.Log("プレイヤーは雑魚敵に"+damage+"与えた。");
            if (destroyedByCollision)
                Destruction();
        }
        //★ プレイヤーの弾がボスに当たった場合
        else if (!enemyBullet && collision.tag == "BossEnemy")
        {
            var activeSceneName = SceneManager.GetActiveScene().name;
            bossDisplay = GameObject.Find("BossDisplay");
            
            // ★★★シーンにより、ボスにダメージを与えるスクリプトを可変にする
            //if(activeSceneName == "Stage1_Scene")
            //{
                if (bossDisplay != null)
                {
                    bossDisplay.GetComponent<BossDisplay>().GetDamage(damage);
                }
                
                //collision.GetComponent<BossDisplay>().GetDamage(damage);
            //}


            if (destroyedByCollision)
                Destruction();
        }
    }

    void Destruction()  //if the object is using 'pooling' disactivate it. If it isn't destroy it
    {
        if (isPooled)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}


