using UnityEngine;

public class UICursor : MonoBehaviour
{
    [SerializeField] private RectTransform cursorRect;


    private void Awake()
    {
        if (cursorRect == null)
            cursorRect = GetComponentInChildren<RectTransform>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Cursor.visible = false;

    }

    private void Update()
    {
        cursorRect.position = Input.mousePosition;
    }
}
