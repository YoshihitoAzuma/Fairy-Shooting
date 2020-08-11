using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossEnemyManager : MonoBehaviour
{
    
    public GameObject bossEnemy;

    
    void Start()
    {
        bossEnemy.SetActive(false);
    }

    
    void Update()
    {
        //currentTime += Time.deltaTime;
        //if(bossEnemy.activeInHierarchy)
        //{
            
            //Debug.Log("time"+currentTime);
            //if(currentTime >= span){
            //Debug.Log("ボスのショット");
            //stage3FireMissile.BossShoot();
            //currentTime = 0f;
            //}
        //}

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

}