using UnityEngine;

public class PlayerMoveToEnemiesArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
             // Khi nhân vật vào vùng, thông báo cho quái vật.
            EnemiesAIController.Instance.EnterArea();
            Debug.Log("Nhân vật tiếp cận ");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            // Khi nhân vật vào vùng, thông báo cho quái vật.
            EnemiesAIController.Instance.ExitArea();
            Debug.Log("Nhân vật thoát ra ");
        }
    }
}