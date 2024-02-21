using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public string key_Score="score_save";
    public int score=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.K))
        {
          Debug.Log("Save");
          Save();


        }
          if (Input.GetKeyDown(KeyCode.L))
        {
          Debug.Log("Load");
          Load();


        }
        
    }
  public void  Save()
  { 
     score++;
     PlayerPrefs.SetInt(key_Score, score);



  }
   public void  Load(){
    int s = PlayerPrefs.GetInt(key_Score);
    Debug.Log("///"+s);


   }
}
