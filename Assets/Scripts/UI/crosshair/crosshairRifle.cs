using UnityEngine;
using UnityEngine.UI;


public class crosshairRifle : CrosshairBase
{

    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform bottom;
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform right;

    [SerializeField] private CanvasGroup centerRifle;


    public float zoomCameraDistance = 0.9f;

    [SerializeField] private float baseSize = 10f; // distancia mínima
    [SerializeField] private float maxExpand = 40f; // expansión máxima
    [SerializeField] private float expandSpeed = 5f; // velocidad de cambio

    public override void SetPrecision(float precision)
    {
        float spread = Mathf.Lerp(baseSize, maxExpand, precision);

        if (precision != 0)
        {
            centerRifle.alpha = 0;
        }
        else if (precision == 0)
        {
            centerRifle.alpha = 1;
        }

        top.anchoredPosition = new Vector2(0, spread);
        bottom.anchoredPosition = new Vector2(0, -spread);
        left.anchoredPosition = new Vector2(-spread, 0);
        right.anchoredPosition = new Vector2(spread, 0);
    }

}

