using UnityEngine;
using System;
using System.Collections.Generic;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

public class Instance : MonoBehaviour {

	[SerializeField] Transform reference;
    [SerializeField] SkinData skinInfo;
    GameObject playerObject;
    Will will;
    AnimatorOverrideController animOverr;
    AnimationClipOverrides clipOverr;

    void Awake()
    {
        reference = transform;
        playerObject = Instantiate(skinInfo.skins[GameController.Instance.currentSkin].skinModel, reference.position, reference.rotation);
        playerObject.AddComponent<Will>();

        will = playerObject.GetComponent<Will>();
        animOverr = new AnimatorOverrideController(will._anim.runtimeAnimatorController);

        clipOverr = new AnimationClipOverrides(animOverr.overridesCount);
        animOverr.GetOverrides(clipOverr);

        clipOverr["InCannon"] = skinInfo.skins[GameController.Instance.currentSkin].animations[0];
        clipOverr["Flying"] = skinInfo.skins[GameController.Instance.currentSkin].animations[1];
        clipOverr["Falling"] = skinInfo.skins[GameController.Instance.currentSkin].animations[2];
        animOverr.ApplyOverrides(clipOverr);
        will._anim.runtimeAnimatorController = animOverr;

    }
}
