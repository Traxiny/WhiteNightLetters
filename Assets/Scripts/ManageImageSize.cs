using UnityEngine;

public class ManageImageSize : MonoBehaviour
{
    [SerializeField] RectTransform imageRectTransform;
    [SerializeField] float dimensionSize = 0.75f;

    void Start()
    {
        ResizeImageToDisplay();
    }

    void ResizeImageToDisplay()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float squareSize = Mathf.Min(screenWidth, screenHeight) * dimensionSize;

        imageRectTransform.sizeDelta = new Vector2(squareSize, squareSize);

        imageRectTransform.anchoredPosition = Vector2.zero;
    }
}
