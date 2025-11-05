using TMPro;
using UnityEngine;

public class UIDamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dmgNumberText;

    private void Update()
    {
    }

    public void SetDamageNumber(int damage)
    {
        dmgNumberText.text = damage.ToString();
    }
}
