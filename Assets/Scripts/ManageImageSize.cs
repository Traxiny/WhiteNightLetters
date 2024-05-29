using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageImageSize : MonoBehaviour
{
    [SerializeField] RectTransform imageRectTransform;

    void Start()
    {
        ResizeImageToDisplay();
    }

    void ResizeImageToDisplay()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate the side length of the square as the smaller dimension of the screen
        float squareSize = Mathf.Min(screenWidth, screenHeight) * 0.75f; // 75% of the smaller dimension

        // Apply the size to the RectTransform
        imageRectTransform.sizeDelta = new Vector2(squareSize, squareSize);

        // Center the image in the middle of the screen
        imageRectTransform.anchoredPosition = Vector2.zero;
    }
}
