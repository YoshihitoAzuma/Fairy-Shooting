using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage2BossEnemyManager : MonoBehaviour
{

    public GameObject bossEnemy;

    public Stage2FireMissile stage2FireMissile;
    

    void Start()
    {
        bossEnemy.SetActive(false);
        //Debug.Log("ボスは非表示");
    }
    // IEnumerator CreateBossEnemy(float delay)
    // {
    //     if (delay != 0)
    //         yield return new WaitForSeconds(delay);
    //     if (Player.instance != null)
    //         BossEnemyCreate();
    //         //Debug.Log("CreateBossEnemy"+BossEnemy);
    // }

    
    // public void BossEnemyCreate()
    // {
    //     // HPスレイダーを表示
    //     Slider.SetActive(true);
    //     enemyHPSlider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();
    //     enemyHPSlider.maxValue = health;
    //     enemyHPSlider.value = health;

    //     //bossEnemy.SetActive(true);
    //     // 座標を固定
    //     //transform.position = new Vector3(0f, 800f, 0f) * Time.deltaTime;
    //     transform.Translate(-6.2f,-5f,0);

    //     var child = GameObject.Find("FireMissile").transform;
    //     child.transform.Translate(0,-6.2f,0);
    //     //child.position = new Vector3(0f, 500f, 0f)* Time.deltaTime;
    //     Debug.Log("ステージ2のボス登場");
    // }
    
    void Update()
    {
        if(bossEnemy.activeInHierarchy)
        {
            //Debug.Log("ボスのショット");
            stage2FireMissile.BossShoot();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        // ボスにプレイヤーが激突した場合
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(10);
            //Debug.Log("プレイヤーがボスに激突");
        }

    }

    // public void GetDamage(int damage) 
    // {
    //     health -= damage;           
    //     //Debug.Log("ボスのHPは"+health);
    //     // 敵のHPを更新
    //     enemyHPSlider.value = health;
    //     if (health <= 0) 
    //         Destruction();
    // }
    // void Destruction()                           
    // {
    //     // エフェクト
    //     Instantiate(destructionVFX, transform.position, Quaternion.identity); 
    //     // 爆破の音を鳴らす
    //     SoundManager.instance.PlaySound("explosion"); //playing sound
    //     Destroy(gameObject);
    //     Debug.Log("ボスは破壊された");

    //     // ゲームクリア
    //     gameController.GameClear();

    // }
}
