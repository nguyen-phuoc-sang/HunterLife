using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
  private bool attacking = false;
  private float timeToAttack = 0.15f;
  private GameObject attackArea = default;

  private float time = 0f;
  // Start is called before the first frame update
  void Start()
  {
    attackArea = transform.GetChild(0).gameObject;
  }

  // Update is called once per frame
  void Update()
  {
    if (attacking)
    {

      time += Time.deltaTime;
      if (time >= timeToAttack)
      {
        time = 0f;
        attacking = false;
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

  }
}
