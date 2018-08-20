using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Database", menuName = "Skin database")]
public class SkinData : ScriptableObject {

    public List<SkinClass> skins = new List<SkinClass>();
}
