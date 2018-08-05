using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectingCoinExpPoints))]
public class Will : MonoBehaviour
{
    [HideInInspector] public GameObject cannonTriggered;
    Transform reference;

    public static Will will = null;

    [HideInInspector] public bool inCannon;
    float speed = 2;

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
        reference = cannonTriggered.transform.GetChild(0).gameObject.transform;
        StartCoroutine(MoveToCannon());
        transform.SetParent(cannonTriggered.gameObject.transform);
        StartCoroutine(cannonTriggered.GetComponent<CannonParent>().Wick());
        StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
    }

    IEnumerator MoveToCannon()
    {
        float step = (speed / (transform.position - reference.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while(t <= 1.0f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, reference.position, t);
            yield return new WaitForFixedUpdate();
        }
        transform.position = reference.position;
    }
}
