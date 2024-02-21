using UnityEngine;

public class PlayerMovementDungeon : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rigidbody2D;
    public Animator ani;
    Vector2 movement;

    // Đặt boom 
    [SerializeField] private GameObject boom;
    [SerializeField] private Transform viTriDatBoom;
    private float timer;
    private float time = 2f;


    // Update is called once per frame
    void Update() // theo frame
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);
        ani.SetFloat("Speed", movement.sqrMagnitude);

        DatBoom();





        //tùng
        moveSpeed = getSpeedRun();
    }


    //tạo getter setter để gọi speedRun mà ko cần public
    public float getSpeedRun()
    {
        return moveSpeed;
    }
    public void setSpeedRun(float speedRun)
    {
        this.moveSpeed = speedRun;
    }

    private void FixedUpdate() // 50 lần mỗi giây
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.deltaTime);
    }

    // Hàm đặt boom
    public void DatBoom()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - timer > time)
        {
            GameObject quaboom = Instantiate(boom, viTriDatBoom.position, viTriDatBoom.rotation);
        }
    }




}
