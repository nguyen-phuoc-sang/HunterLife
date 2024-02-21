using System.Collections;
using UnityEngine;

public class TreeDungeon : MonoBehaviour
{

    private Animator anim;

    public AudioSource CutTreeSound;

    public GameObject wood = default;
    [SerializeField] private Transform viTriwoood;
    public GameObject roots = default;
    public GameObject PopUpDame;
    //  public TMP_Text popuptext;
    private int lifeTree_Snow = 5;
    [SerializeField] private SimpleFlash flashEffect;


    [SerializeField] private Transform viTriroots;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RotateMe(Vector3.back * 90, 0.9f));
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nameTree = collision.attachedRigidbody.gameObject.name;
        if (collision.gameObject.CompareTag("axe"))
        {
            Vector3 originPosotion = transform.position;

            lifeTree_Snow = lifeTree_Snow - 1;

            CutTreeSound.Play();
            flashEffect.Flash();
            // popuptext.text =damage.ToString;
            Instantiate(PopUpDame, originPosotion, Quaternion.identity
            );

            StartCoroutine(ShakeOnce(0.2f, 0.012f));
            if (lifeTree_Snow == 0)
            {
                TreeToRoots();
                StartCoroutine(RotateMe(Vector3.back * 90, 0.9f));
                Destroy(gameObject, 1.2f);
                // TreeToWood();
                lifeTree_Snow = 5;

            }

        }



    }
    private void TreeToWood()
    {
        /*ani.Play("Char_Attack_LR");*/

        GameObject woodfall = Instantiate(wood, viTriwoood.position, viTriwoood.rotation);


    }
    private void TreeToRoots()
    {

        GameObject rootsfall = Instantiate(roots, viTriroots.position, viTriroots.rotation);
    }
    public IEnumerator ShakeOnce(float time, float magnitude)
    {
        Vector3 originPosotion = transform.position;
        float totalTime = 0f;

        while (totalTime < time)
        {
            totalTime += Time.deltaTime;
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            float z = Random.Range(-1, 1) * magnitude;

            transform.position += new Vector3(x, 0, z);

            yield return null;
        }

        transform.position = originPosotion;


    }
    public IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        float totalTime = 0f;

        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        TreeToWood();




    }



}
