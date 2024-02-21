using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack    : MonoBehaviour
{
    private GameObject attackArea = default;

   
   
    private float timeToAttack = 0.14f;
    private float time = 0f;

   

    // Start is called before the first frame update
    void Start()
    {
     
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
      
              
        }

       

    public IEnumerator  Attack()
    {
        

    while (time < timeToAttack)
    {
      time += Time.deltaTime;
     
     attackArea.SetActive(true);

      yield return null;
    }
      time = 0f;

    
    
        attackArea.SetActive(false);
    
              
        
      
        
    }







}
