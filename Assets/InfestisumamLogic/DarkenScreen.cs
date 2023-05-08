using UnityEngine;
using UnityEngine.UI;

public class DarkenScreen : MonoBehaviour
{

    public float darkeningSpeed = 0.5f;
    public float maxAlpha = 0.5f;

    private Image image;
    private bool isDarkening;

    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0);
        isDarkening = false;
        
    }

    public void StartDarkening()
    {
        isDarkening = true;
    }
    void Update()
    {
        if (isDarkening)
        {
            float alpha = image.color.a + darkeningSpeed * Time.deltaTime;
            alpha = Mathf.Clamp(alpha, 0, maxAlpha);
            image.color = new Color(0, 0, 0, alpha);
        }
    }
}