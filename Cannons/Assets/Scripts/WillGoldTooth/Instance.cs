using UnityEngine;

public class Instance : MonoBehaviour {

	[SerializeField] Transform reference;
    [SerializeField] SkinData skinInfo;
    void Awake()
    {
        reference = transform;       
        GameObject player = Instantiate(skinInfo.skins[GameManager.Instance.currentSkin].skinModel, reference.position, reference.rotation);
        player.AddComponent<Will>();
    }
}
