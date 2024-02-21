using Inventory;
using Inventory.Model;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class CreateScripts : MonoBehaviour
{
    public ItemSO itemSO;

    public InventorySO inventoryData;

    public InventoryController inventoryController;


    public Sprite spriteRenderer;

    public static CreateScripts createScriptsIntance;



    [MenuItem("Tools/Create My ScriptableObject")]

    //load vật phẩm túi đồ
    public void CreateMyScriptableObject()
    {
        StartCoroutine(LoadImageSprite());


    }

    IEnumerator LoadImageSprite()
    {
        InventoryController inventoryController = FindObjectOfType<InventoryController>();
        //SellController sellController = FindObjectOfType<SellController>();
        //ItemSO[] scriptableObjects = Resources.LoadAll<ItemSO>("DataItemAPI");
        if (inventoryController != null)
        {
            // xóa dữ liệu túi đồ cũ
            inventoryController.initalItems.Clear();
            //sellController.initalItems.Clear();           

            foreach (TestModel model in ItemAPILogin.testModel)
            {

                // Tạo một ScriptableObject mới
                ItemSO newScriptableObject = ScriptableObject.CreateInstance<ItemSO>();

                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(model.itemName.image))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        // Tạo một sprite từ texture tải về
                        Texture2D texture = DownloadHandlerTexture.GetContent(www);
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                        // Gán sprite trực tiếp vào SpriteRenderer
                        spriteRenderer = sprite;
                        newScriptableObject.idName += model.itemName._id;
                        newScriptableObject.Name += model.itemName.itemName;
                        newScriptableObject.Description += model.itemName.description;
                        newScriptableObject.itemImage = model.itemName.image;
                        newScriptableObject.IteamImage = spriteRenderer;
                        newScriptableObject.quantity += model.quantity;
                        newScriptableObject.price += model.itemName.price;
                        newScriptableObject.IsStackable = true;
                        newScriptableObject.MaxStackSize = 99;


                        // Hoặc gán sprite trực tiếp vào một Sprite khác (không thông qua SpriteRenderer)
                        // this.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    else
                    {
                        Debug.Log("Error loading image: " + www.error);
                    }
                }

                // Lưu đối tượng vào thư mục Assets
                string assetPath = "Assets/Resources/DataItemAPI/" + model.itemName.itemName + ".asset";
                AssetDatabase.CreateAsset(newScriptableObject, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                // đổ item lên túi đồ
                inventoryController.newScriptableObjectt = newScriptableObject;
                inventoryController.PrepareInventoryData();

                //// đổ item lên bán
                //sellController.newScriptableObjectt = newScriptableObject;
                //sellController.PrepareInventoryData();

                // Chọn đối tượng mới tạo trong Project window
                Selection.activeObject = newScriptableObject;

            }
        }
        inventoryController.LoadItemSell();
        inventoryController.LoadQuantity();
    }



    //load tất cả vật phẩm

    public void CreateMyScriptableObjectAllItem()
    {
        StartCoroutine(GetAllItemAPI());

    }

    IEnumerator GetAllItemAPI()
    {
        foreach (GetAllItemResponseModel model in ItemAPILogin.getAllItemResponse)
        {

            // Tạo một ScriptableObject mới
            ItemSO newScriptableObject = ScriptableObject.CreateInstance<ItemSO>();

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(model.image))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    // Tạo một sprite từ texture tải về
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                    // Gán sprite trực tiếp vào SpriteRenderer
                    spriteRenderer = sprite;
                    newScriptableObject.idName += model._id;
                    newScriptableObject.Name += model.itemName;
                    newScriptableObject.Description += model.description;
                    newScriptableObject.itemImage = model.image;
                    newScriptableObject.IteamImage = spriteRenderer;
                    newScriptableObject.price += model.price;
                    newScriptableObject.IsStackable = true;
                    newScriptableObject.MaxStackSize = 99;

                    // Hoặc gán sprite trực tiếp vào một Sprite khác (không thông qua SpriteRenderer)
                    // this.GetComponent<SpriteRenderer>().sprite = sprite;
                }
                else
                {
                    Debug.Log("Error loading image: " + www.error);
                }
            }

            // Lưu đối tượng vào thư mục Assets
            string assetPath = "Assets/Resources/DataAllItemAPI/" + model.itemName + ".asset";
            AssetDatabase.CreateAsset(newScriptableObject, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();




            // Chọn đối tượng mới tạo trong Project window
            Selection.activeObject = newScriptableObject;

        }

        InventoryController inventoryController = FindObjectOfType<InventoryController>();
        if (inventoryController != null)
        {
            inventoryController.LoadQuantity();
        }

    }

    //load vật phẩm trong cửa hàng
    public void CreateMyScriptableObjectAllItemShop()
    {
        StartCoroutine(GetAllItemShopAPI());

    }

    IEnumerator GetAllItemShopAPI()
    {

        ShopController shopController = FindObjectOfType<ShopController>();
        //ItemSO[] scriptableObjects = Resources.LoadAll<ItemSO>("DataItemAPI");
        if (shopController != null)
        {
            // xóa dữ liệu túi đồ cũ
            shopController.initalItems.Clear();

            foreach (ShopItemResponseModel model in ItemAPILogin.shopItemResponseModel)
            {

                // Tạo một ScriptableObject mới
                ItemSO newScriptableObject = ScriptableObject.CreateInstance<ItemSO>();

                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(model.itemName.image))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        // Tạo một sprite từ texture tải về
                        Texture2D texture = DownloadHandlerTexture.GetContent(www);
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                        // Gán sprite trực tiếp vào SpriteRenderer
                        spriteRenderer = sprite;
                        newScriptableObject.id += model._id;
                        newScriptableObject.idName += model.itemName._id;
                        newScriptableObject.Name += model.itemName.itemName;
                        newScriptableObject.Description += model.itemName.description;
                        newScriptableObject.itemImage = model.itemName.image;
                        newScriptableObject.IteamImage = spriteRenderer;
                        newScriptableObject.price += model.price;
                        newScriptableObject.quantity += model.quantity;
                        newScriptableObject.IsStackable = true;
                        newScriptableObject.MaxStackSize = 99;

                        // Hoặc gán sprite trực tiếp vào một Sprite khác (không thông qua SpriteRenderer)
                        // this.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    else
                    {
                        Debug.Log("Error loading image: " + www.error);
                    }
                }

                // Lưu đối tượng vào thư mục Assets
                string assetPath = "Assets/Resources/DataAllItemShopAPI/" + model._id + ".asset";
                AssetDatabase.CreateAsset(newScriptableObject, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                shopController.newScriptableObjectt = newScriptableObject;
                shopController.PrepareInventoryData();


                // Chọn đối tượng mới tạo trong Project window
                Selection.activeObject = newScriptableObject;

            }
        }
        shopController.LoadItemShop();
        InventoryController inventoryController = FindObjectOfType<InventoryController>();
        if (inventoryController != null)
        {
            inventoryController.LoadQuantity();
        }
    }

    private void Awake()
    {
        if (createScriptsIntance == null)
        {
            createScriptsIntance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            CreateMyScriptableObject();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            CreateMyScriptableObjectAllItemShop();
        }
    }

}