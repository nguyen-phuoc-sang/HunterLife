using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.TextCore.Text;

public class BirdNoPathfinder : MonoBehaviour
{
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

    //public float roamRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        ani = GetComponent<Animator>();

        // lấy vị trí ban đầu
        initPos = positionBird.position;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(positionBird.position, target.position);

        if (distanceToTarget <= chaseRadius)
        {
            roaming = true;
            Destroy(gameObject, 6);
            }
        else
        {
            roaming = false;
        }
    }

    void CalculatePath()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        Vector2 t = FindTarget();
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine(t));
    }

    IEnumerator MoveToTargetCoroutine( Vector2 target)
    {
        int currentWP = 0;
        while(currentWP < 1)
        {
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position,target);
            if (distance < 0.1f)
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
    }
    Vector2 RandomRoaming()
    {
        // Tạo vị trí mới trong ô vuông
        Vector2 randomPos = (Vector2)initPos + new Vector2(Random.Range(-chuviSize / 2f, chuviSize / 2f), Random.Range(-chuviSize / 2f, chuviSize / 2f));

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
        if ((Vector2)positionBird.position == evadePosition)
        {
            ani.SetFloat("isFly", 0.05f);
            ani.SetBool("isIdle", true);
        }
        return evadePosition;
    }
    Vector2 FindTarget()
    {
        Vector2 newPos ;
        if (roaming)
        {
            isCheck = true;
            newPos = FlyAlways();
        }
        else
        // nhân vật đi xa
        {

            if (!isCheck)
            {
                ani.SetFloat("isFly", 0.05f);
                ani.SetBool("isIdle", true);
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