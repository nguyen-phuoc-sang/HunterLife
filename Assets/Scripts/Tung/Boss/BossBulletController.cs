using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public float speed; // Tốc độ của viên đạn

    private Vector2 direction; // Hướng của viên đạn

    private Vector2 scale;

    private void Start()
    {
        
    }
    void Update()
    {
        // Di chuyển viên đạn theo hướng đã thiết lập
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Hàm này để thiết lập hướng của viên đạn
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
    public void SetScale(Vector2 newScale)
    {
        scale = newScale;
    }
}
