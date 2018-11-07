using System.Collections;
using UnityEngine;

public class Ave : MonoBehaviour,ICoins {

    [SerializeField] Distance distance;
    [SerializeField] Transform background;
    [SerializeField] WriteVbles writeVbles;
    [SerializeField] Rigidbody mRigid;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Collider mCollider;
    [SerializeField] ParticleSystem cParticle;
    [SerializeField] AudioController audioController;
    Vector3 initialPosition;
    WaitForSeconds wait = new WaitForSeconds(0f);
    private float randomPos;

    private void Start()
    {
        initialPosition = transform.localPosition;
        distance.delSeagull += SetActive;
    }

    public IEnumerator SetActive()
    {
        mRigid.AddForce(Vector3.left * Random.Range(1f, 2.5f), ForceMode.Impulse);       
        audioController.AudioGullSound(1f);
        while (true)
        {
            if (!sprite.enabled) { sprite.enabled = true; mCollider.enabled = true; }                
            randomPos = Random.Range(5.5f, 7f);
            transform.position = new Vector3(initialPosition.x, background.position.y + randomPos, initialPosition.z);      
            wait = new WaitForSeconds(Random.Range(10f, 15f));
            yield return wait;
            audioController.AudioGullSound(1f);
        }                     
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Will>() != null) {
            audioController.AudioSmashSeagull();
            mRigid.velocity = Vector3.zero;
            sprite.enabled = false;
            mCollider.enabled = false;
            cParticle.Play();
        }
    }

    public void CollectCoins()
    {
        Singleton.instance.Coins += Random.Range(3,9);
        writeVbles.WritingNumberOfCoins();//Will write the number of coins in a text
    }
}