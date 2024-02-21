using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharAndNPC
{
    public class Player : MonoBehaviour
    {
        private SpriteRenderer rp;
        private bool isLR;
        private bool isTD;
        [SerializeField] private float speedRun;
        private bool runTop, runBottom, runLR;
        private Animator ani;
        //Tùng thêm
        public GameObject npc;
        private bool isTouchNPC;
        public bool isAfterTouchNPC;
        public bool isClickBtn;

        //tạo getter setter để gọi speedRun mà ko cần public
        public float getSpeedRun()
        {
            return speedRun;
        }
        public void setSpeedRun(float speedRun)
        {
            this.speedRun = speedRun;
        }
        //
        // Start is called before the first frame update
        void Start()
        {
            ani = GetComponent<Animator>();
            rp = GetComponent<SpriteRenderer>();
            //
            isTouchNPC = false;
            isClickBtn = false;
            isAfterTouchNPC = false;
        }

        // Update is called once per frame
        void Update()
        {

            Vector3 velocity = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                velocity = new Vector3(0, 10, 0);
                isTD = true;
                ani.SetBool("Run_Top", true);
                ani.Play("Char_Run_Top");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                velocity = new Vector3(0, -10, 0);
                isTD = false;
                ani.SetBool("Run_Down", true);
                ani.Play("Char_Run_Down");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                velocity = new Vector3(-10, 0, 0);
                rp.GetComponent<SpriteRenderer>().flipX = true;
                isLR = true;
                ani.SetBool("Run_LR", true);
                ani.Play("Char_Run");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                velocity = new Vector3(10, 0, 0);
                rp.GetComponent<SpriteRenderer>().flipX = false;
                isLR = false;
                ani.SetBool("Run_LR", true);
                ani.Play("Char_Run");
            }
            speedRun = getSpeedRun();
            transform.Translate(velocity * speedRun * Time.deltaTime);


        }
        // bắt sự kiện va chạm 
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var name = collision.gameObject.tag;

            //khi nhân vật chạm npc
            if (collision.gameObject.CompareTag("npc"))
            {
                if (!isTouchNPC)
                {
                    isTouchNPC = true;
                }
            }
        }
    }
}
