using System.Collections;
using UnityEngine;

public class Wildboar : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform positionBird;
    public Transform target;
    public float chaseRadius;
    private bool isCheck = false;
    public float chuviSize = 5f;
    private bool roaming;
    public SpriteRenderer characterSR;

    Coroutine moveCoroutine;
    private Vector3 initPos;
    private Animator ani;
    private float moveSpeed;
    public GameObject meat = default;
    public int maumax = 5;
    public int mau = 5;

    private bool hasExploded = false;
    private bool isRespawning = false;

    void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        ani = GetComponent<Animator>();
        initPos = positionBird.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (mau <= 0 && !hasExploded && !isRespawning)
        {
            StartCoroutine(DeathSequence());
        }

        float distanceToTarget = Vector3.Distance(positionBird.position, target.position);

        roaming = (distanceToTarget <= chaseRadius);
    }

    void CalculatePath()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);

        if (gameObject.activeSelf)
        {
            Vector2 t = FindTarget();
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine(t));
        }
        else
        {
            Debug.Log("Vật thể không tồn tại hoặc không active.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CArrow"))
        {
            mau--;
            RandomRoaming();
        }
    }

    IEnumerator DeathSequence()
    {
        Debug.Log("Bắt đầu DeathSequence");
        ani.Play("boardie");
        moveSpeed = 0f;
        hasExploded = true;
        yield return new WaitForSeconds(1.2f);
       //  DestroyAllComponents(gameObject);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
       

        GameObject Meatfall = Instantiate(meat, transform.position, transform.rotation);
        gameObject.SetActive(false);
       

       // isRespawning = true;
           mau = maumax;
        hasExploded = false;
        isRespawning = false;
        yield return new WaitForSeconds(5f);
        

     
      
     
    }

    IEnumerator MoveToTargetCoroutine(Vector2 target)
    {
        int currentWP = 0;
        while (currentWP < 1)
        {
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, target);
            if (distance < 0.1f)
            {
                currentWP++;
            }

            if (force.x != 0)
            {
                characterSR.transform.localScale = new Vector3(force.x < 0 ? -2f : 2f, 2f, 0);
            }

            yield return null;
        }
    }

    Vector2 RandomRoaming()
    {
        Vector2 randomPos = (Vector2)initPos + new Vector2(Random.Range(-chuviSize / 2f, chuviSize / 2f), Random.Range(-chuviSize / 2f, chuviSize / 2f));
        randomPos.x = Mathf.Clamp(randomPos.x, initPos.x - chuviSize / 2f, initPos.x + chuviSize / 2f);
        randomPos.y = Mathf.Clamp(randomPos.y, initPos.y - chuviSize / 2f, initPos.y + chuviSize / 2f);
        return randomPos;
    }

    Vector2 FlyAlways()
    {
        Vector3 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        Vector2 directionToPlayer = (transform.position - playerPos).normalized;
        ani.SetBool("idel", false);
        ani.SetFloat("run", 0.2f);
        Vector2 evadePosition = (Vector2)initPos + directionToPlayer * (chaseRadius + 2f);
        moveSpeed = 3f;

        if ((Vector2)positionBird.position == evadePosition)
        {
            ani.SetFloat("run", 0.04f);
            ani.SetBool("idel", true);
        }

        return evadePosition;
    }
    //  void DestroyAllComponents(GameObject target)
    // {
    //     // Lấy danh sách tất cả các component trên GameObject
    //     Component[] components = target.GetComponents<Component>();

    //     // Hủy bỏ từng component
    //     foreach (var component in components)
    //     {
    //         // Bạn có thể kiểm tra loại component để tránh hủy các thành phần quan trọng
    //         if (!(component is Transform) && !(component is GameObject))
    //         {
    //             Destroy(component);
    //         }
    //     }
    // }

    Vector2 FindTarget()
    {
        Vector2 newPos;
        if (roaming)
        {
            isCheck = true;
            newPos = FlyAlways();
        }
        else
        {
            if (!isCheck)
            {
                ani.SetFloat("run", 0.05f);
                ani.SetBool("idel", true);
                moveSpeed = 1.5f;
                newPos = RandomRoaming();
            }
            else
            {
                newPos = FlyAlways();
            }
        }

        return newPos;
    }
}
