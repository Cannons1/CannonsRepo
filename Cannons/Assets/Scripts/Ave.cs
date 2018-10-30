using System.Collections;
using UnityEngine;

public class Ave : MonoBehaviour,ICoins {

    [SerializeField] Distance distance;
    [SerializeField] Transform background;
    [SerializeField] WriteVbles writeVbles;
    Rigidbody mRigid;
    Vector3 initialPosition;
    SpriteRenderer sprite;
    WaitForSeconds wait = new WaitForSeconds(0f);
    private float randomPos;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mRigid = GetComponent<Rigidbody>();
        initialPosition = transform.localPosition;
        distance.delSeagull += ActiveSeagull;
    }

    private void ActiveSeagull() {
        StartCoroutine(SetActive());
    }

    public IEnumerator SetActive()
    {
        mRigid.AddForce(Vector3.left * 2f, ForceMode.Impulse);       
        while (true)
        {
            if (!sprite.enabled) sprite.enabled = true;
            randomPos = Random.Range(5.5f, 7f);
            transform.position = new Vector3(initialPosition.x, background.position.y + randomPos, initialPosition.z);      
            wait = new WaitForSeconds(Random.Range(10f, 15f));
            yield return wait;
        }                     
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Will>() != null) {
            sprite.enabled = false;
        }
    }

    public void CollectCoins()
    {
        Singleton.instance.Coins += Random.Range(3,9);
        writeVbles.WritingNumberOfCoins();//Will write the number of coins in a text
    }
}