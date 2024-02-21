using Inventory.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            StartCoroutine(LoadImage(item));

        }

    }

    IEnumerator LoadImage(Item item)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(item.InventoryItem.itemImage))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Tạo một sprite từ texture tải về
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                // Gán sprite               
                item.InventoryItem.IteamImage = sprite;
                ItemAPILogin.itemAPI.NewItemInventory(item.InventoryItem.idName, item.Quantity);
                if (item.InventoryItem.idName == "651ff3786d1b88d6eb0d18e4")
                {
                    ShowPotion.playerLife.LifePot += 1;
                }

                if (item.InventoryItem.idName == "6574bc92db53a20b56ab4326")
                {
                    ShowPotion.playerLife.Key += 1;
                }

                if (item.InventoryItem.idName == "657ee6149ffb7c266c17f886")
                {
                    ShowPotion.playerLife.Meat += 1;
                }

                if (item.InventoryItem.idName == "6571532ea3a65dd019bd9b8a")
                {
                    MissionController.MissionControllinstance.Mission4Controller(item.Quantity);
                }

                if (item.InventoryItem.idName == "651ff3bb6d1b88d6eb0d18e8")
                {
                    Debug.Log("Nhatqj");
                    MissionController.MissionControllinstance.Mission5Controller();
                }

                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    item.DestroyItem();
                    AudioManager.instance.PlaySfx("Coin");
                }
                else
                {
                    item.Quantity = reminder;
                }

                item.DestroyItem();

            }
            else
            {
                Debug.Log("Error loading image: " + www.error);
            }
        }
    }

}
