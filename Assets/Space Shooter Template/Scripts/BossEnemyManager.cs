using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyManager : MonoBehaviour
{
    public int health;

    public GameObject destructionVFX;
    public GameObject bossEnemy;
    public GameController gameController;

    public FireMissile fireMissile;

    public GameObject Slider;

    // 敵のHPスライダー
    private Slider enemyHPSlider;

    void Start()
    {
        bossEnemy.SetActive(false);
        
        //if (!bossEnemy.activeSelf){
            //bossEnemy.SetActive(false);
            //Debug.Log("ボスは非表示");
           // StartCoroutine(CreateBossEnemy(10.0f)); 
        //}

    }

    // Update is called once per frame
    // public void BossEnemyCreate()
    // {
    //     bossEnemy.SetActive(true);
        
    //     // HPスレイダーを表示
    //     Slider.SetActive(true);

    //     enemyHPSlider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();
    //     enemyHPSlider.maxValue = health;
    //     enemyHPSlider.value = health;

    //     bossEnemy.SetActive(true);
    //     // 座標を固定
    //     //transform.position = new Vector3(0f, 800f, 0f) * Time.deltaTime;
    //     transform.Translate(-6.2f,-5f,0);

    //     var child = GameObject.Find("FireMissile").transform;
    //     //child.transform.Translate(0,-6.2f,0);
    //     //child.position = new Vector3(0f, 500f, 0f)* Time.deltaTime;
    //     Debug.Log("ボス登場");
    // }

    // IEnumerator CreateBossEnemy(float delay)
    // {
    //     if (delay != 0)
    //         yield return new WaitForSeconds(delay);
    //     if (Player.instance != null)
    //         BossEnemyCreate();
    //         //Debug.Log("CreateBossEnemy"+BossEnemy);
    // }

    
    void Update()
    {
        if(bossEnemy.activeInHierarchy)
        {
            //Debug.Log("ボスのショット");
            fireMissile.BossShoot();
        }

        //transform.position += new Vector3(-650f, 0f, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        // ボスにプレイヤーが激突した場合
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(10);

        }

    }

    // 呼び出し元はProjectile.cs
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
