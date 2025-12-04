using UnityEngine;
using UnityEngine.UI;


public class ToggleHighlightStaysActiveAfterLosingFocus : MonoBehaviour
{
    //code by https://www.youtube.com/watch?v=ZFuEjAcx1-s

    [SerializeField] private Toggle toggle;
    [SerializeField] private Image imageToKeepFocusActive;

    private void Reset()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(toggle.isOn);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (imageToKeepFocusActive == null) return;

        imageToKeepFocusActive.color = toggle.isOn ? toggle.colors.highlightedColor : Color.clear;
    }
}
