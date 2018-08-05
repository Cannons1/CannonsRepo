using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectingCoinExpPoints))]
public class Will : MonoBehaviour
{
    //public delegate void WillEvents();
    //public static event WillEvents OnDie;
    //public static event WillEvents OnCameraMove;

    [HideInInspector] public GameObject cannonTriggered;
    GameObject reference;

    public static Will will = null;

    [HideInInspector] public bool inCannon;

    private void Awake()
    {
        if (will == null)
        {
            will = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        cannonTriggered = other.gameObject;
        Debug.Log(cannonTriggered.name);

        if (cannonTriggered.tag == "Cannon")
        {
            inCannon = true;
            other.enabled = false;
            StuckOnCannon();
        }

        if (cannonTriggered.GetComponent<DieEvent>() != null) {
            DieEvent dieEvent;
            dieEvent = cannonTriggered.GetComponent<DieEvent>();
            dieEvent.ChargeMenuLevel();
        }
    }

    void StuckOnCannon()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        reference = cannonTriggered.transform.GetChild(0).gameObject;
        transform.SetParent(cannonTriggered.gameObject.transform);
        transform.position = reference.transform.position;
        StartCoroutine(cannonTriggered.GetComponent<CannonParent>().Wick());
        StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
    }

    /*private void OnBecameInvisible()
    {
        if(OnDie != null)
        {
            OnDie();
        }
        else
        {
            Debug.Log("I dont know what to do ma frien");
        }
    }*/
}
