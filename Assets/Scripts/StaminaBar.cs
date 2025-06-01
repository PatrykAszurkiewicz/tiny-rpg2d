using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private PlayerMove player;
    public Image staminaFillImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Object.FindAnyObjectByType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && staminaFillImage != null)
        {
            float staminaPercent = player.stamina / player.maxStamina;
            //staminaFillImage.fillAmount = staminaPercent;
            staminaFillImage.fillAmount = Mathf.Lerp(staminaFillImage.fillAmount, staminaPercent, Time.deltaTime * 10f);

        }
    }
}
