using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bird1Script : MonoBehaviour
{
    private bool roaming;
    private float moveSpeed;
    private float nextWPDistance = 0.3f;
    public SpriteRenderer characterSR;

    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDestination = false;

    //public GameObject Pos;
    private Vector3 initPos;
    private Animator ani;

    public Transform target;
    public Transform positionEnemies;
    public float chaseRadius;
    private bool isCheck = false;
    public float chuviSize = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
        ani= GetComponent<Animator>();

        // lấy vị trí ban đầu
        initPos = positionEnemies.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(positionEnemies.position, target.position);


        if (distanceToTarget <= chaseRadius)
        {
            // nhân vật tới gần
            roaming = true;
            Destroy(gameObject, 4);
            Debug.Log("Vị trí ban đầu: " + initPos);
        }
        else
        {
            roaming = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var name = collision.gameObject.tag;

    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination))
            seeker.StartPath(transform.position, target, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }

            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    characterSR.transform.localScale = new Vector3(5f, 5f, 0);
                }
                else
                {
                    characterSR.transform.localScale = new Vector3(-5f, 5f, 0);
                }
            }

            yield return null;

        }
        reachDestination = true;
    }

    Vector2 RandomPosition()
    {
        // Tạo vị trí mới trong ô vuông
        Vector2 randomPos = (Vector2) initPos + new Vector2(Random.Range(-chuviSize / 2f, chuviSize / 2f), Random.Range(-chuviSize / 2f, chuviSize / 2f));

        // Giới hạn vị trí trong ô vuông
        randomPos.x = Mathf.Clamp(randomPos.x, initPos.x - chuviSize / 2f, initPos.x + chuviSize / 2f);
        randomPos.y = Mathf.Clamp(randomPos.y, initPos.y - chuviSize / 2f, initPos.y + chuviSize / 2f);

        return randomPos;
    }

    Vector2 FlyAlways()
    {
        Vector3 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        Vector2 directionToPlayer = (transform.position - playerPos).normalized;
        ani.SetBool("isIdle", false);
        ani.SetFloat("isFly", 0.2f);
        Vector2 evadePosition = (Vector2)initPos + directionToPlayer * (chaseRadius + 2f);
        moveSpeed = 3f;
        if ((Vector2)positionEnemies.position == evadePosition)
        {
            ani.SetFloat("isFly", 0.05f);
            ani.SetBool("isIdle", true);
        }
        return evadePosition;
    }
    Vector2 FindTarget()
    {
        Vector2 newPos;
        if (roaming)
        {
            isCheck = true;
            newPos = FlyAlways();    
        }
        else
        // nhân vật đi xa
        {
            
            if(!isCheck) {
                ani.SetFloat("isFly", 0.05f);
                ani.SetBool("isIdle", true);
                moveSpeed = 1.5f;
                newPos = RandomPosition();
            }
            else
            {
                newPos = FlyAlways();
            }
        }
        return newPos;
    }
}
