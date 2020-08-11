using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{
public GameObject missilePrefab;
    
    //public AudioClip fireSound;

    public float _Velocity_0, Degree, Angle_Split;

    float _theta;
    float PI = Mathf.PI;

    private int timeCount = 0;

    // 弾を発射する時間間隔
    public int shotTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BossShoot()
    {           
         
        timeCount += 1;
        // 弾を発射する時間間隔の調整
        if (timeCount % shotTime == 0)
        {
        for (int i = 0; i <= (Angle_Split - 1); i++) {
            //n-way弾の端から端までの角度
            float AngleRange = PI * (Degree / 180);

            //弾インスタンスに渡す角度の計算
            if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (PI + AngleRange);
            else _theta = 0.5f * PI;

        //GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        // 第三引数：無回転
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        
        Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();


        Vector2 bulletV = missileRb.velocity;
        bulletV.x = _Velocity_0 * Mathf.Cos(_theta);
        bulletV.y = _Velocity_0 * Mathf.Sin(_theta);
        missileRb.velocity = bulletV;

        //missileRb.AddForce(transform.forward * missileSpeed);
        //Debug.Log("ボスの弾を発射");
        //AudioSource.PlayClipAtPoint(fireSound, transform.position);
        
        // 弾のSEを鳴らす
        SoundManager.instance.PlaySound("BossEnemy");

        // 発射したミサイルを10秒後に破壊（削除）する。
        Destroy(missile, 10.0f);
        }
        }
    }


}
