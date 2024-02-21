using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    // vùng tile cần thực hiện
    public Tilemap tilemap;
    // đất mới
    public TileBase newTile;
    // tạo cây
    public GameObject treePrefab;

    // tạo hình cây rìu khi thu hoạch
    public GameObject riuPrefab;
    // tốc độ rìu bay
    private float riuMoveDuration = 1f;

    // đóng or mở ô có thể đào và trồng
    private bool canDig = false;
    private bool canPlant = false;

    // xác định được ô nào đào rồi và ô nào được trồng cây rồi
    private bool[] dugTiles;
    private bool[] plantedTrees;

    private Animator animation;

    // làm hàm kiếm tra xem ô đó đã click chưa
    private bool[] clickedTiles;

    // kiểm tra xem có đang đào không
    private bool isDigging = false;

    // cập nhập hướng quay 
    private int facingDirection = 1;
    // hướng ban đầu
    private int initialFacingDirection = 1;

    private InventoryController inventoryController;

    private void Start()
    {
        // tạo 1 mảng lấy vị trí của ô đất đã đào và ô đã trồng cây
        dugTiles = new bool[tilemap.cellBounds.size.x * tilemap.cellBounds.size.y];
        plantedTrees = new bool[tilemap.cellBounds.size.x * tilemap.cellBounds.size.y];

        animation = GetComponent<Animator>();

        // bắt sự kiện phá hủy cây bên FLO
        FLO.OnDestroyed += PlayDestructionAnimation;

        // ô đất đã click ở vị trí nào
        clickedTiles = new bool[tilemap.cellBounds.size.x * tilemap.cellBounds.size.y];

        initialFacingDirection = (int)Mathf.Sign(transform.localScale.x);

        inventoryController = FindObjectOfType<InventoryController>();
        // ngày đêm
        //StartCoroutine(StartDayNightCycle());
    }

    private void Update()
    {
        // đào đất
        if (Input.GetMouseButtonDown(0) && canDig && !isDigging && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.Z))
        {
            if (!CanDigAtMousePosition())
            {
                StartCoroutine(MoveRiu());
                StartCoroutine(WaitAndDig());
            }
        }

        
        if (inventoryController != null)
        {
            foreach (var item in inventoryController.inventoryData.GetCurrentInventoryState())
            {
                if(item.Value.itemSO.idName == "651ff3086d1b88d6eb0d18de")
                {
                    if(item.Value.quantity >= 0)
                    {
                        // trồng cây
                        if (Input.GetKeyDown(KeyCode.Z) && canPlant && canDig && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                        {
                            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                            if (tilemap.GetTile(cellPosition) == newTile)
                            {
                                if (!IsTilePlanted(cellPosition) && !CanDigAtMousePosition())
                                {
                                    PlantFL();
                                    
                                }
                            }
                        }

                        if (Input.GetKey(KeyCode.Z) && canPlant && canDig && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                        {
                            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                            if (tilemap.GetTile(cellPosition) == newTile)
                            {
                                if (!IsTilePlanted(cellPosition))
                                {
                                    PlantFL();
                                                                   
                                }
                            }
                        }
                    }
                }
            }
        }

        


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dig"))
        {
            canDig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dig"))
        {
            canDig = false;
        }
    }

    // hàm chờ và đào đất
    private IEnumerator WaitAndDig()
    {
        if (!SoundController.isGamePaused)
        {
            isDigging = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            int index = GetTileIndex(cellPosition);

            if (index != -1 && index < clickedTiles.Length)  // Kiểm tra index hợp lệ
            {
                if (!clickedTiles[index])
                {
                    UpdateFacingDirection(cellPosition);
                    animation.Play("Player_DigGround");
                    AudioManager.instance.PlaySfx("Dig");
                }
                clickedTiles[index] = true;
                yield return new WaitForSeconds(0.7f);
                if (index < dugTiles.Length && !dugTiles[index])
                {
                    tilemap.SetTile(cellPosition, newTile);
                    dugTiles[index] = true;
                    canPlant = true;
                    RestoreInitialFacingDirection();
                }
            }
        }
        isDigging = false;
    }

    // tạo cây cuốc khi đào đất
    private IEnumerator MoveRiu()
    {
        if (!SoundController.isGamePaused)
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            int index = GetTileIndex(cellPosition);
            if (index != -1 && !dugTiles[index] && !clickedTiles[index])
            {
                // Tạo cây rìu trực tiếp tại vị trí trên ô đất cần đào
                Vector3 riuStartPosition = tilemap.GetCellCenterWorld(cellPosition) + new Vector3(-0.1f, 0.3f, 0f);

                // Tạo cây rìu
                GameObject riuInstance = Instantiate(riuPrefab, riuStartPosition, Quaternion.identity);

                // Vị trí đích của cây rìu (rơi xuống)
                Vector3 targetPosition = new Vector3(riuInstance.transform.position.x, riuInstance.transform.position.y, riuInstance.transform.position.z);

                Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);

                // Di chuyển cây rìu từ trên xuống và xoay 90 độ
                float elapsedTime = 0.6f;
                while (elapsedTime < riuMoveDuration)
                {
                    float t = elapsedTime / riuMoveDuration;
                    riuInstance.transform.position = Vector3.Lerp(riuInstance.transform.position, targetPosition, t);


                    // Thay đổi trục Z
                    float newZ = Mathf.Lerp(riuInstance.transform.position.z, targetPosition.z, t);
                    riuInstance.transform.position = new Vector3(riuInstance.transform.position.x, riuInstance.transform.position.y, newZ);

                    // Xoay cây rìu
                    riuInstance.transform.rotation = Quaternion.Lerp(Quaternion.identity, targetRotation, t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                // Hủy cây rìu sau khi hoàn thành
                Destroy(riuInstance);
            }
        }
    }

    // Kiểm tra ô đã trồng cây chưa
    private bool IsTilePlanted(Vector3Int cellPosition)
    {
        int index = GetTileIndex(cellPosition);
        return index != -1 && plantedTrees[index];
    }

    // hàm chờ để đưa về trạng thái ban đầu cho trồng cây
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        RestoreInitialFacingDirection();
    }

    // hàm trồng cây
    private void PlantFL()
    {
        if (!SoundController.isGamePaused)
        {
            if (canPlant)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                int index = GetTileIndex(cellPosition);

                if (index != -1 && dugTiles[index] && !plantedTrees[index])
                {
                    // Kiểm tra ô đã trồng cây chưa
                    if (!IsTilePlanted(cellPosition) && !TreeExistsAtCell(cellPosition))
                    {
                        UpdateFacingDirection(cellPosition);
                        inventoryController.removeItem("651ff3086d1b88d6eb0d18de", 1);
                        
                        // Trồng cây tại ô đã đào
                        Instantiate(treePrefab, tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
                        animation.Play("Player_Plant");
                        AudioManager.instance.PlaySfx("Plant");
                        StartCoroutine(Wait());

                    }
                }
            }
        }
    }

    // xác định vị trí của ô trong tilemap
    private int GetTileIndex(Vector3Int cellPosition)
    {
        int cellX = cellPosition.x - tilemap.cellBounds.x;
        int cellY = cellPosition.y - tilemap.cellBounds.y;

        if (cellX >= 0 && cellX < tilemap.cellBounds.size.x && cellY >= 0 && cellY < tilemap.cellBounds.size.y)
        {
            return cellY * tilemap.cellBounds.size.x + cellX;
        }

        return -1;
    }

    // kiểm tra xem cây có thu hoạch chưa
    private bool TreeExistsAtCell(Vector3Int cellPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(tilemap.GetCellCenterWorld(cellPosition));
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("FLBlue"))
            {
                return true;
            }
        }
        return false;
    }

    // thực hiện animation thu hoạch
    private void PlayDestructionAnimation()
    {
        animation.Play("Player_Harvest");
        AudioManager.instance.PlaySfx("Harvest");
    }

    // khoảng cách đào đất
    private bool CanDigAtMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
        Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);

        // Kiểm tra xem ô đất được chọn có cách ít nhất 1 ô đất giữa nó và nhân vật không
        return Mathf.Abs(cellPosition.x - playerCellPosition.x) > 1f || Mathf.Abs(cellPosition.y - playerCellPosition.y) > 1f;
    }

    // cập nhập hướng quay nhân vật
    private void UpdateFacingDirection(Vector3Int cellPosition)
    {
        int playerCellX = tilemap.WorldToCell(transform.position).x;
        if (cellPosition.x < playerCellX)
        {
            facingDirection = -1; // Quay về phía trái
        }
        else if (cellPosition.x > playerCellX)
        {
            facingDirection = 1; // Quay về phía phải
        }

        // Áp dụng hướng cho localScale
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * facingDirection;
        transform.localScale = newScale;
    }

    // khôi phục lại hướng ban đầu
    private void RestoreInitialFacingDirection()
    {
        facingDirection = initialFacingDirection;

        // Áp dụng hướng cho localScale
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * facingDirection;
        transform.localScale = newScale;
    }

}
