using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class HitAndHeathEnemies : MonoBehaviour
{
    public GameObject ActiveBlood;
    AIEnemies5 eni5;
    AIEnemies1 eni1;
    AIEnemies2 eni2;
    AIEnemies3 eni3;
    AIEnemies4 eni4;
    AIEnemies6 eni6;
    AIEnemies7 eni7;
    AIEnemies8 eni8;
    public Image blood;
    public float hit;
    private Animator animator;
    private bool isKill = false;
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] private Transform viTriPopUpDame;
    public GameObject PopUpDame;
    public Health health;
    public int numKey;
    public GameObject Key;
    public int numLifePot;
    public GameObject LifePot;
    [SerializeField] private Transform viTri;

    // public TMP_Text popuptext;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        eni5 = GetComponent<AIEnemies5>();
        eni1 = GetComponent<AIEnemies1>();
        eni2 = GetComponent<AIEnemies2>();
        eni3 = GetComponent<AIEnemies3>();
        eni4 = GetComponent<AIEnemies4>();
        eni6 = GetComponent<AIEnemies6>();
        eni7 = GetComponent<AIEnemies7>();
        eni8 = GetComponent<AIEnemies8>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isKill)
        {
            


            // GameObject Key2 = Instantiate(Key, viTri.position, viTri.rotation);
        
        
// GameObject LifePot2 = Instantiate(LifePot, viTri.position, viTri.rotation);
       
            DestroyEnemiesWithTag(gameObject.tag);
        }
        //  BeingAttacked(0.2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.gameObject.tag;

        //khi bị tấn công
        if (collision.gameObject.CompareTag("boom"))
        {
            BeingAttacked(0.25f);
        }
        if (collision.gameObject.CompareTag("axe"))
        {
            //  BeingAttacked(0.2f);
        }
         if (collision.gameObject.CompareTag("CArrow"))
        {
             BeingAttacked(0.15f);
        }
    }

    public void BeingAttacked(float dame)
    {

        //  Vector3 originPosotion = transform.position;
        float oneTouch = dame;
        string text = (dame * 100).ToString();


        flashEffect.Flash();
        //  popuptext.text=(oneTouch*100).ToString();
        GameObject prefab = Instantiate(PopUpDame, viTriPopUpDame.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = text;
        blood.fillAmount = blood.fillAmount - oneTouch;
        blood.fillAmount = blood.fillAmount;
        if (blood.fillAmount < 0.1f)
        {
            isKill = true;
        }
    }

    private void DestroyEnemiesWithTag(string tag)
    {
        if (tag == "") { return; }
        // mất thanh máu
        ActiveBlood.SetActive(false);
        animator.SetTrigger("isDie");
      
        switch (tag)
        {
            case "enemy1":
                eni1.moveSpeed = 0f;
                break;
            case "enemy2":
                eni2.moveSpeed = 0f;
                break;
            case "enemy3":
                eni3.moveSpeed = 0f;
                break;
            case "enemy4":
                eni4.moveSpeed = 0f;
                break;
            case "enemy5":
                eni5.moveSpeed = 0f;
                break;
            case "enemy6":
                eni6.moveSpeed = 0f;
                break;
            case "enemy7":
                eni7.moveSpeed = 0f;
                break;
            case "enemy8":
                eni8.moveSpeed = 0f;
                break;
            default:
                Debug.Log("không có tên quái này");
                break;
        }
        Invoke("DestroyEnemies", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void DestroyEnemies()
    {
        Destroy(gameObject);
    }
}
