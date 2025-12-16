using TMPro;
using UnityEngine;

public class UIPotionHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCount;

    public void SetItemCount(int count)
    {
        itemCount.text = "x" + count.ToString();
    }
}
