using Inventory.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        StartCoroutine(LoadImage());

        Debug.Log("acb");

    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUp());


    }

    public void LoadScriptableObject()
    {
        // Load danh sách ScriptableObject từ thư mục Resources
        ItemSO[] scriptableObjects = Resources.LoadAll<ItemSO>("DataAllItemAPI");

        // Lặp qua danh sách và làm cái gì đó
        foreach (ItemSO so in scriptableObjects)
        {
            if (so.id == InventoryItem.id)
            {
                InventoryItem.itemImage = so.itemImage;
                StartCoroutine(LoadImage());
                Debug.Log("ScriptableObject Name: " + so.IteamImage);
            }

            // Thực hiện các thao tác khác nếu cần
        }
    }

    private IEnumerator AnimateItemPickUp()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;

        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }

    IEnumerator LoadImage()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(InventoryItem.itemImage))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Tạo một sprite từ texture tải về
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                // Gán sprite trực tiếp vào SpriteRenderer
                GetComponent<SpriteRenderer>().sprite = sprite;
                transform.localScale = new Vector3(5, 5, 0);
                // Hoặc gán sprite trực tiếp vào một Sprite khác (không thông qua SpriteRenderer)
                // this.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            else
            {
                Debug.Log("Error loading image: " + www.error);
            }
        }
    }
}
