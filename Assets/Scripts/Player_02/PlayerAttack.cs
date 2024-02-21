using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack    : MonoBehaviour
{
    private GameObject attackArea = default;
    public GameObject AXe ;


    private bool attacking = false;
    private bool attacking2 = true;

    private float timeToAttack = 0.25f;

    private float time = 0f;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>(); 
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
             Attack();
             ani.Play("Char_Attack_LR");
            
           
           


        }else if (attacking)
        {

            time += Time.deltaTime;
            if(time>= timeToAttack)
            {
                time = 0f;
                attacking= false;
              //  attacking2=true;

                attackArea.SetActive(attacking);
               //  AXe.SetActive(attacking2);
            }
        }

    }    

    private void Attack()
    {
        /*ani.Play("Char_Attack_LR");*/

        attacking = true;
     //   attacking2 = false;

        attackArea.SetActive(attacking);
      //  AXe.SetActive(attacking2);
              
        ;
      
        
    }







}
