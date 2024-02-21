using System.Collections;
using UnityEngine;

public class WeaponParentF : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public Vector2 PointerPosition { get; set; }



    public float delay = 0.3f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }


    public float radius;
   
   
   
   
    [Header("Weapon3(bow)")]
    public SpriteRenderer weaponRenderer3;
    public Animator animator3;
    //  public Transform circleOrigin3;
    public GameObject Weapon3;
    public GameObject ArrowPrefab;
    public Transform Arrowfire;
    public float shootDelay = 1f; // Thời gian chờ giữa mỗi lần bắn
    [Header("Weapon4(GunVIP)")]
    public SpriteRenderer weaponRenderer4;
    public Animator animator4;
    //  public Transform circleOrigin3;
    public GameObject Weapon4;
    public GameObject BulletPrefab;
    public Transform Bulletfire;
    public float shootDelayGun = 0.15f; // Thời gian chờ giữa mỗi lần bắn


    //
    private SpriteRenderer weaponRenderer;
    private Animator animator;
    private Transform circleOrigin;
    //

    private int W ;

    private float lastShootTime;

    public MissionController mission;

    void Start()
    {
        //   attackArea = transform.GetChild(0).gameObject;
        weaponRenderer = weaponRenderer3;
        animator = animator3;
      
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Update()
    {
        //Alpha1
      
     
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(W == 3){setNone();}
            else
            {
                 setWeapon3();
            }
           


        }
        if (Input.GetKeyDown(KeyCode.V) )
        {
            if(W == 4){setNone();}
            else
            {
                 setWeapon4();
            }


        }

        if (IsAttacking)
            return;
        //  if(  weaponRenderer == weaponRenderer1 ||weaponRenderer == weaponRenderer2){
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        //   if (Input.GetMouseButtonDown(0) && weaponRenderer == weaponRenderer3 )
        // {
        //     ShootArrow();
        // }

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
        //  }


        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
            if(W==0)
            return;
        if (W == 3)
        {
            if (Time.time > lastShootTime + shootDelay)
            {
                ShootArrow();
                lastShootTime = Time.time;
                animator.SetTrigger("Attackbow");
            }


        }
        else
        {
            if (W == 4)
            {
                if (Time.time > lastShootTime + shootDelayGun)
                {
                    ShootBullet();
                    lastShootTime = Time.time;
                    animator.SetTrigger("Attackgun");
                }

            }

          
        }


    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    public void ShootArrow()
    {

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;



        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 rotationEulerAngles = new Vector3(0, 0, angle);
        Quaternion rotation = Quaternion.Euler(rotationEulerAngles);

        GameObject Arroww = Instantiate(ArrowPrefab, Arrowfire.position, rotation);
        //   Arroww.GetComponent<Arrow>().SetDirection(direction);
    }
    public void ShootBullet()
    {

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;



        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 rotationEulerAngles = new Vector3(0, 0, angle);
        Quaternion rotation = Quaternion.Euler(rotationEulerAngles);

        GameObject Bullett = Instantiate(BulletPrefab, Bulletfire.position, rotation);
        //   Arroww.GetComponent<Arrow>().SetDirection(direction);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
    //     Gizmos.DrawWireSphere(position, radius);
    // }
   
    public void setWeapon3()
    {
        W = 3;
        weaponRenderer = weaponRenderer3;
        animator = animator3;
        //  circleOrigin = circleOrigin3;
       
        Weapon3.SetActive(true);
        Weapon4.SetActive(false);


    }
    public void setWeapon4()
    {
        W = 4;
        weaponRenderer = weaponRenderer4;
        animator = animator4;
        //  circleOrigin = circleOrigin3;
       
        Weapon3.SetActive(false);
        Weapon4.SetActive(true);


    }
     public void setNone(){
          W =0;
        Weapon3.SetActive(false);
        Weapon4.SetActive(false);


    }


  
}