using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBullet : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public SpriteRenderer characterSR;
    HitAndHeathBoss hit;
    void Start()
    {
        characterSR = GetComponent<SpriteRenderer>();
        hit = GetComponent<HitAndHeathBoss>();

        if(!hit.isDie)
        {
            StartCoroutine(ShootBulletAfterDelay());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ShootBulletAfterDelay()
    {
        while (!hit.isDie)
        {
            // Bắn viên đạn
            ShootBullet();
            yield return new WaitForSeconds(3f);
        }
    }

    void ShootBullet()
    {
        // Tạo một bản sao của prefab viên đạn tại vị trí firePoint và cùng hướng với quái vật
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Thiết lập hướng của viên đạn theo hướng của quái vật
        BossBulletController bulletController = bullet.GetComponent<BossBulletController>();
        
        if(characterSR.transform.localScale.x == 4f)
        {
            bulletController.SetDirection(Vector2.left);
            // Đảo scale của đạn
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x = -4;
            bullet.transform.localScale = bulletScale;
        }
        else if(characterSR.transform.localScale.x == -4f)
        {
            bulletController.SetDirection(Vector2.right);
            // Đảo scale của đạn
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x = 4;
            bullet.transform.localScale = bulletScale;
        }
        Destroy(bullet, 4f);
    }
}
