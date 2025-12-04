using UnityEngine;
using UnityEngine.UI;

public class UIScrollUV : MonoBehaviour
{
    public RawImage img;
    public float speed = 0.2f;

    private void Update()
    {
        if (img == null)
            return;

        Rect r = img.uvRect;
        r.x += speed * Time.unscaledDeltaTime;
        if (r.x > 1f) r.x -= 1f;
        img.uvRect = r;
    }
}
