using UnityEngine;
using UnityEngine.UI;

public class UILoadingBar : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    public void SetLoadFill(float amount)
    {
        loadingBar.value = amount;
    }
}
