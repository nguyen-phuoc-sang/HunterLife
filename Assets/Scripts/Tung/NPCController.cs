using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CharAndNPC
{
    public class NPCController : MonoBehaviour
    {
        public GameObject Panel;
        public TMP_Text contentLabel;
        public string[] content;
        private int index;
        public GameObject button;

        private int checkBtnClick2Time;

        public float wordSpeed;

        private float moveSpeed = 3.0f;
        public float width = 5.0f;
        public float height = 3.0f;
        private Vector2 initialPosition;
        private Vector2 targetPosition;
        private Animator animator;
        private BoxCollider2D npcBox;
        private PlayerMovement playerSpeed;
        private bool isAfterTouchChar;



        private enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }

        private Direction currentDirection = Direction.Left;

        private void Start()
        {
            npcBox = GetComponent<BoxCollider2D>();
            initialPosition = transform.position;
            targetPosition = initialPosition + Vector2.down * height; // Bắt đầu di chuyển sang phải
            animator = GetComponent<Animator>();
            UpdateAnimator(Direction.Down); // Bắt đầu với Animator "Right"
            checkBtnClick2Time = 0;
            isAfterTouchChar = false;
            // Truy cập biến của PlayerInteraction.cs 
            playerSpeed = FindObjectOfType<PlayerMovement>();
            if (playerSpeed != null) { }
            else
            {
                Debug.Log("không truy cập được PlayerMovement");
            }
        }

        private void Update()
        {
            if (isAfterTouchChar)
            {
                moveSpeed = 3.0f;
            }
            // Di chuyển NPC đến vị trí tiếp theo
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra xem NPC đã đến gần vị trí tiếp theo chưa
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Chuyển đổi hướng di chuyển
                switch (currentDirection)
                {
                    case Direction.Left:
                        targetPosition = new Vector2(initialPosition.x, initialPosition.y - height);
                        currentDirection = Direction.Down;
                        UpdateAnimator(Direction.Down);
                        break;
                    case Direction.Down:
                        targetPosition = new Vector2(initialPosition.x + width, initialPosition.y - height);
                        currentDirection = Direction.Right;
                        UpdateAnimator(Direction.Right);
                        break;
                    case Direction.Right:
                        targetPosition = new Vector2(initialPosition.x + width, initialPosition.y);
                        currentDirection = Direction.Up;
                        UpdateAnimator(Direction.Up);
                        break;
                    case Direction.Up:
                        targetPosition = new Vector2(initialPosition.x, initialPosition.y);
                        currentDirection = Direction.Left;
                        UpdateAnimator(Direction.Left);
                        break;
                }
            }
            button.SetActive(false);
            if(contentLabel.text == content[index])
            {
                button.SetActive(true);
            }
        }

        IEnumerator Typing()
        {
            foreach(char letter in content[index].ToCharArray())
            {
                contentLabel.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            button.SetActive(false);
            if (index < content.Length - 1)
            {
                index++;
                contentLabel.text = "";
                StartCoroutine(Typing());
            }
            else
            {
                Debug.Log("ko hiện chữ tiếp theo dc");
            }
        }

        private void UpdateAnimator(Direction newDirection)
        {
            // Dừng tất cả animation trước đó
            animator.StopPlayback();
            switch (newDirection)
            {
                case Direction.Left:
                    animator.Play("NPC_Run_Left");
                    break;
                case Direction.Down:
                    animator.Play("NPC_Run_Dowm");
                    break;
                case Direction.Right:
                    animator.Play("NPC_Run_Right");
                    break;
                case Direction.Up:
                    animator.Play("NPC_Run_Top");
                    break;
            }
        }
        // bắt sự kiện 2 box va chạm nhau
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var name = collision.gameObject.tag;

            //khi nhân vật chạm npc
            if (collision.gameObject.CompareTag("player"))
            {
                if (!isAfterTouchChar)
                {
                    animator.StopPlayback();
                    animator.Play("NPC_Stand");
                    moveSpeed = 0f;
                    PanelHandler();
                    // dừng di chuyển char
                    playerSpeed.setSpeedRun(0f);
                }
            }
        }
        // thao tác với panel 
        private void PanelHandler()
        {
            Debug.Log("đang đứng");
            Panel.SetActive(true);
            StartCoroutine(Typing());
        }

        public void checkClick()
        {
            checkBtnClick2Time++;
            Debug.Log("lần nhân thứ: " + checkBtnClick2Time);
            // muốn nhập bao nhiêu text cũng được, không cần if từ 1 tới n...
            for (int i = 0; i < content.Length; i++)
            {
                if (checkBtnClick2Time == i)
                {
                    NextLine();
                }
            }
            if (checkBtnClick2Time == content.Length)
            {
                Debug.Log("đã click");
                playerSpeed.setSpeedRun(5f);
                //playerInstance.isClickBtn = true;
                Debug.Log("đang chạy");
                //tắt panel
                Panel.SetActive(false);
                isAfterTouchChar = true;
                //npc đi xuyên qua char
                npcBox.isTrigger=true;
                // xóa npc sau 3s
                Destroy(gameObject, 3);   
            }
        }
    }
}