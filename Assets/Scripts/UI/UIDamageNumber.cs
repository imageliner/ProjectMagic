using TMPro;
using UnityEngine;

public class UIDamageNumber : MonoBehaviour
{
    private DisplayNumberPool poolOwner;
    [SerializeField] private TextMeshProUGUI dmgNumberText;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDamageNumber(int damage)
    {
        dmgNumberText.text = damage.ToString();
    }

    public void UseNumber(int number)
    {
        transform.Translate(Vector3.up * 2f);
        dmgNumberText.text = number.ToString();
        animator.Play("Base");
        Invoke("ResetNumber", 1f);
    }

    public void InitializePooledNumbers(DisplayNumberPool owner)
    {
        poolOwner = owner;
    }

    private void ResetNumber()
    {

        poolOwner.ReturnNumber(this);

        gameObject.SetActive(false);
    }
}
