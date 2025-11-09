using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponObject", menuName = "Scriptable Objects/WeaponObject")]
public class WeaponObject : ScriptableObject
{
    [SerializeField] private ClassType classType;
    [SerializeField] public GameObject mesh;
    public CharacterAbility attackAbility;

    [SerializeField] private string itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private int pAtk;
    [SerializeField] private int mAtk;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float critRate;
    [SerializeField] private float critDmg;
    [SerializeField] private int strength;
    [SerializeField] private int intelligence;
    [SerializeField] private int dexterity;

    public enum ClassType
    {
        Warrior,
        Mage,
        Ranger
    }


    public void Attack(int attackID, Transform t)
    {
        attackAbility.Use(attackID, t);
    }
}
