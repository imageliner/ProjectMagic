using System;
using UnityEngine;

[System.Serializable]
public class GearItem : MonoBehaviour
{
    [SerializeField] private GearObject gearObject;
    public ParticleSystem swingEff;
    //public AnimationClip attackAnim;

    public GearObject GetGearObject()
    {
        return gearObject;
    }

    public void AddStatsToPlayer()
    {
        gearObject.AddStats();
    }

    public void RemoveStatsFromPlayer()
    {
        gearObject.RemoveStats();
    }
}
