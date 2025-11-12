using System;
using UnityEngine;

[System.Serializable]
public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponObject weaponObject;
    public AnimationClip attackAnim;

    public WeaponObject GetWeaponObject()
    {
        return weaponObject;
    }
}
