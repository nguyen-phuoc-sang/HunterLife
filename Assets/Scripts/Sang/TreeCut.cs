using System.Collections;
using UnityEngine;

public class TreeCut : MonoBehaviour
{
    public GameObject wood = default;
    private int lifeTree_Snow = 5;

    [SerializeField] private Transform viTriwood;



    private void Update()
    {
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nameTree = collision.attachedRigidbody.gameObject.name;
        if (collision.gameObject.CompareTag("axe"))
        {
            lifeTree_Snow = lifeTree_Snow - 1;


            StartCoroutine(ShakeOnce(0.2f, 0.012f));
            if (lifeTree_Snow == 0)
            {
                GetComponent<Collider2D>().isTrigger = true;
                StartCoroutine(RotateMe(Vector3.back * 70, 0.9f));
                AudioManager.instance.PlaySfx("Tree");
                Destroy(gameObject, 1f);
                TreeToWood();
                lifeTree_Snow = 5;

            }

        }

    }
    private void TreeToWood()
    {
        GameObject woodfall = Instantiate(wood, viTriwood.position, viTriwood.rotation);
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
    }
}
