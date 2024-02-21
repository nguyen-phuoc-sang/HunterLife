using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
     private Rigidbody2D rb;
    public float speed = 5f; // Tốc độ của viên đạn
    public float forceMagnitude = 10f;
    public int BoA =1;
     private GameObject parent;
     // Lực của viên đạn

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Áp dụng lực xuất phát cho mũi tên khi nó được tạo ra
        rb.AddForce(transform.right * forceMagnitude, ForceMode2D.Impulse);
    }
    //  public void SetDirection(Vector2 newDirection)
    // {
    //     direction = newDirection;
    // }
      public void SetParent(GameObject newParent)
    {
        parent = newParent;
    }
    void OnTriggerEnter2D(Collider2D other)
{      
    if( BoA==1){
    // Kiểm tra nếu mũi tên va chạm vào một vật thể
    if (other.CompareTag("enemy3"))
    {
      
       transform.parent = other.transform;
          StickToTarget();
     
        Destroy(rb);
    }
      if (other.CompareTag("enemy6"))
    {
       
       transform.parent = other.transform;
          StickToTarget();
        Destroy(rb);
    }
     if (other.CompareTag("enemy7"))
    {
        
       transform.parent = other.transform;
          StickToTarget();  
        Destroy(rb);
    }
      if (other.CompareTag("enemy8"))
    {
        
       transform.parent = other.transform;
          StickToTarget();  
        Destroy(rb);
    }
    if (other.CompareTag("Boss"))
    {
        
       transform.parent = other.transform;
          StickToTarget();  
        Destroy(rb);
    }
     if (other.CompareTag("boar"))
    {
        
       transform.parent = other.transform;
          StickToTarget();  
        Destroy(rb);
    }
     if (other.CompareTag("tuong"))
    {
        // Gán mũi tên là con của vật thể đó
       transform.parent = other.transform;
       

        // Tắt Rigidbody để mũi tên không bị ảnh hưởng bởi vật lý
        Destroy(rb);

        // Tắt script điều khiển để mũi tên không di chuyển nữa (nếu cần)
        // Destroy(GetComponent<Arrow>());
    }
    }
     if( BoA==2){
    // Kiểm tra nếu mũi tên va chạm vào một vật thể
    if (other.CompareTag("enemy3"))
    {
        Destroy(gameObject);
    }
      if (other.CompareTag("enemy6"))
    {
       Destroy(gameObject);
    }
     if (other.CompareTag("enemy7"))
    {
       Destroy(gameObject);
    }
      if (other.CompareTag("enemy8"))
    {
        Destroy(gameObject);
    }
    if (other.CompareTag("Boss"))
    {
        Destroy(gameObject);
    }
      if (other.CompareTag("boar"))
    {      
        Destroy(gameObject);
    }
     if (other.CompareTag("tuong"))
    {
     Destroy(gameObject);

        // Tắt script điều khiển để mũi tên không di chuyển nữa (nếu cần)
        // Destroy(GetComponent<Arrow>());
    }
    }
}
void StickToTarget()
    {
        Vector2 averagePosition = (transform.position + transform.parent.position) / 2f;
        transform.position = averagePosition;
        GetComponent<Collider2D>().enabled = false;

        // Kiểm tra xem parent có tồn tại hay không trước khi sử dụng
       
}
}