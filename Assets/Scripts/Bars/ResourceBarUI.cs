using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private float targetFill = 1f;
    private float lerpSpeed = 10f;

    public void SetValue(float current, float max)
    {
        targetFill = Mathf.Clamp01(current / max);
    }

    private void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
    }
}
