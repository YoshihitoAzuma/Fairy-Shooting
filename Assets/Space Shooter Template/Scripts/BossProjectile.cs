using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage;
    public bool isPooled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ボスの弾がプレイヤーに衝突
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(damage); 
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
