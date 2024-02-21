using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Pathfinding;
using TMPro;

public class EnemiesAIController : MonoBehaviour
{
    public static EnemiesAIController Instance;
    public GameObject Area;
    private Vector3 characterPosition;
    private bool isFollowingCharacter = false;

    private float moveSpeed; // Tốc độ di chuyển của quái vật
    private Vector3 initialPosition; // Vị trí ban đầu
    private Animator animator;

    //AI
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDestination;
    public float nextWPDistance;
    public bool updateContinuesPath;
    public SpriteRenderer characterSR;

    private void Start()
    {
        animator= GetComponent<Animator>();
        // lấy vị trí ban đầu, lấy khoảng cách sẽ đi
        initialPosition = transform.position;

        reachDestination = true;

    }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isFollowingCharacter)
        {
            // Nếu đang theo dõi nhân vật, di chuyển đến vị trí của nhân vật.
            //AI
            moveSpeed = 2f;
            Debug.Log("Vị trí ban đầu: " + (Vector2)initialPosition);
            InvokeRepeating("CalculatePath", 0f, 0.5f);
        }
        else
        {
            // Nếu không theo dõi nhân vật, thực hiện hành vi đi bình thường.
            CancelInvoke("CalculatePath");
            StopAllCoroutines();
            StartCoroutine(WayBackStartPosition());
        }
    }

    public void UpdateCharacterPosition(Vector3 position)
    {
        characterPosition = position;
    }

    public void EnterArea()
    {
        isFollowingCharacter = true;
    }

    public void ExitArea()
    {
        isFollowingCharacter = false;
    }

    void CalculatePath()
    {
        Vector2 target = characterPosition;
        if (seeker.IsDone() && reachDestination|| updateContinuesPath)
            seeker.StartPath(transform.position, target,OnPathComlete);
    }

    void OnPathComlete(Path p) 
    {
        if (p.error) return;
        path = p;
        // move to target
        MoveToTarget();
    }
    
    void MoveToTarget()
    {
        if(moveCoroutine!= null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination= false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] -(Vector2) transform.position).normalized;
            Debug.Log("Dir: "+ direction);
            Vector2 force = direction * moveSpeed *Time.deltaTime;
            transform.position += (Vector3)force;
            animator.SetFloat("isRun", direction.sqrMagnitude);

            float distane = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distane < nextWPDistance)
                currentWP++;
            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    characterSR.transform.localScale = new Vector3(-4f, 4f, 0);
                }
                else
                {
                    characterSR.transform.localScale = new Vector3(4f, 4f, 0);
                }
            }
            yield return null;
        }
    }

     IEnumerator WayBackStartPosition()
    {
        Vector2 direction = ((Vector2)initialPosition - (Vector2)transform.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;
        transform.position += (Vector3)force;
        animator.SetFloat("isRun", direction.sqrMagnitude);
        // Kiểm tra hướng di chuyển
        if (force.x > 0)
        {
            characterSR.transform.localScale = new Vector3(4f, 4f, 0);
        }
        else if (force.x < 0)
        {
            characterSR.transform.localScale = new Vector3(-4f, 4f, 0);
        }

        // Kiểm tra nếu quái vật đã đến gần vị trí đích
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            characterSR.transform.localScale = new Vector3(4f, 4f, 0);
            animator.SetFloat("isRun",0.05f);
        
            moveSpeed = 0;
            StopAllCoroutines();
            Debug.Log("Đã đến gần vị trí đích!");
        }

        yield return null;
    }
}