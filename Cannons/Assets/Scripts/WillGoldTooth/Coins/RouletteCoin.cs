using UnityEngine;

public class RouletteCoin : MonoBehaviour, IRouletteCoin
{
    BoxCollider mBox;
    Renderer mRend;

    private void Start()
    {
        mBox = GetComponent<BoxCollider>();
        mRend = GetComponent<Renderer>();
    }

    public void RoulletteCoinCollected()
    {
        DeactivateCoin();
    }

    public void DeactivateCoin() {
        mBox.enabled = false;
        mRend.enabled = false;
    }
}
