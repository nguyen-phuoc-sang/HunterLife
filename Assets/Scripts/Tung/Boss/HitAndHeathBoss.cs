using UnityEngine;
using UnityEngine.UI;

public class HitAndHeathBoss : MonoBehaviour
{
    public Image bloodGreen;
    public float hitGreen = 0;
    public Image bloodRed;
    public float hitRed = 0;
    private Animator animator;
    private bool isEndGreen = false;
    //  Boom bom = new Boom();
    public bool isDie = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.gameObject.tag;

        //khi bị tấn công
        if (collision.gameObject.CompareTag("CArrow"))
        {
            // if (bom.isActive)
            // {
            //     //return;
            // }
            // else {                
            if (isEndGreen)
            {
                BeingAttackedRed();
            }
            else
            {
                BeingAttackedGreen();
            }
        }

        //  }
    }
    public void BeingAttacked()
    {
        if (isEndGreen)
        {
            BeingAttackedRed();
        }
        else
        {
            BeingAttackedGreen();
        }


    }


    private void BeingAttackedGreen()
    {
        float oneTouch = 1f / hitGreen;
        bloodGreen.fillAmount = bloodGreen.fillAmount - oneTouch;
        bloodGreen.fillAmount = bloodGreen.fillAmount;
        if (bloodGreen.fillAmount < 0.1f)
        {
            isEndGreen = true;
            bloodRed.gameObject.SetActive(true);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
    }

    private void BeingAttackedRed()
    {
        bloodGreen.gameObject.SetActive(false);
        float oneTouch = 1f / hitRed;
        bloodRed.fillAmount = bloodRed.fillAmount - oneTouch;
        bloodRed.fillAmount = bloodRed.fillAmount;
        if (bloodRed.fillAmount < 0.1f)
        {
            isDie = true;
            //Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
    }
    public void DestroyBoss()
    {
        Destroy(gameObject);
    }
}

