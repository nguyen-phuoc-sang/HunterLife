using System.Collections;
using UnityEngine;

public class FLO : MonoBehaviour
{
    public Sprite newSprite, newSprite1, newSprite2, newSprite3; // Hình ảnh mới cần thay đổi


    private SpriteRenderer spriteRenderer;
    // thu hoạch đc chưa
    private bool isCollect;
    // màu khi rê chuột vào
    private Color originalColor;
    public Color highlightColor = Color.red;

    public GameObject harvestSymbolPrefab; // Prefab của kí hiệu thu hoạch
    private GameObject harvestSymbolInstance; // Instance của kí hiệu thu hoạch

    // gọi sự kiện cho scipts khác sử dụng 
    public delegate void OnDestroyedEventHandler();
    public static event OnDestroyedEventHandler OnDestroyed;

    // tạo hình cây rìu khi thu hoạch
    public GameObject riuPrefab;
    // tốc độ rìu bay
    private float riuMoveDuration = 0.4f;
    private bool hasHarvested = false;

    // cây bông biến mất chưa
    private bool hasDisappeared = false;

    // thu hoạch xong tạo ra bó lúa
    public GameObject rice = default;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeSpriteAfterDelay(2.0f));

        isCollect = false;
        originalColor = GetComponent<Renderer>().material.color; // Lấy màu gốc từ Renderer
    }

    private void Update()
    {
        // thu hoạch được và bằng chuột
        if (isCollect && !hasHarvested)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (OnDestroyed != null)
                    {
                        OnDestroyed();
                        StartCoroutine(MoveRiuAndHarvest());
                        hasHarvested = true;
                    }
                }
            }
        }

        // xuất hiện hình cây liềm
        if (isCollect)
        {
            if (harvestSymbolInstance == null)
            {
                Vector3 symbolPosition = transform.position + Vector3.up * 0.7f; // Điều chỉnh vị trí kí hiệu
                harvestSymbolInstance = Instantiate(harvestSymbolPrefab, symbolPosition, Quaternion.identity);
            }
        }

        // thu hoạch bằng shift
        if (isCollect)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (!hasDisappeared)
                    {
                        OnDestroyed();
                        StartCoroutine(MoveRiuAndHarvest());
                        hasHarvested = true;
                        hasDisappeared = true;
                    }
                }
            }
        }
    }


    // hàm cho cây lớn từ từ
    private IEnumerator ChangeSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = newSprite;

        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = newSprite1;

        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = newSprite2;

        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = newSprite3;

        isCollect = true;
    }
    private void OnMouseEnter()
    {
        if (isCollect)
        {
            GetComponent<Renderer>().material.color = highlightColor;
        }

    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor; // Khôi phục màu khi chuột rời khỏi
    }

    // di chuyển cây liềm và thu hoạch cây
    private IEnumerator MoveRiuAndHarvest()
    {
        // Tạo cây rìu
        GameObject riuInstance = Instantiate(riuPrefab, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.identity);

        // Lật ngược cây rìu 260 độ
        riuInstance.transform.rotation = Quaternion.Euler(-1, -178, -255);

        // Tính vị trí đích của cây rìu theo hình vòng cung từ tay trái sang tay phải
        float startX = transform.position.x - 0.3f;
        float startY = transform.position.y - 0.3f;
        float targetX = startX + 0.7f; // Điều chỉnh khoảng cách cây rìu di chuyển

        float elapsedTime = 0f;
        while (elapsedTime < riuMoveDuration)
        {
            // Tính toán vị trí theo hình vòng cung
            float t = elapsedTime / riuMoveDuration;
            float currentX = Mathf.Lerp(startX, targetX, t);
            float currentY = startY - Mathf.Sin(t * Mathf.PI) * 0.1f; // Điều chỉnh độ cao của hình vòng cung

            riuInstance.transform.position = new Vector3(currentX, currentY, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        Destroy(harvestSymbolInstance);
        // Hủy cây rìu sau khi hoàn thành
        Destroy(riuInstance);
        RiceToSheaf();
    }

    private void RiceToSheaf()
    {
        Vector3 sheafPosition = transform.position;
        GameObject sheaf = Instantiate(rice, sheafPosition, Quaternion.identity);

        Rigidbody2D sheafRb = sheaf.GetComponent<Rigidbody2D>();
        if (sheafRb == null)
        {
            sheafRb = sheaf.AddComponent<Rigidbody2D>();
        }

        // Thiết lập vận tốc ban đầu cho nhảy
        float jumpForce = 5f; // Điều chỉnh mức độ nhảy
        sheafRb.velocity = new Vector2(0f, jumpForce);
        Destroy(sheafRb, 1f);
    }
}
