using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class Instance : MonoBehaviour {

	[SerializeField] Transform reference;
    [SerializeField] SkinData skinInfo;
    void Awake()
    {
        reference = transform;       
        //skinInfo = Resources.Load<SkinData>("DataBase/SkinDatabase.asset") as SkinData;
        //skinInfo = (SkinData)AssetDatabase.LoadAssetAtPath("Assets/DataBase/SkinDatabase.asset", typeof(SkinData));
        GameObject player = Instantiate(skinInfo.skins[GameManager.Instance.currentSkin].skinModel, reference.position, reference.rotation);
        player.AddComponent<Will>();
    }
}
