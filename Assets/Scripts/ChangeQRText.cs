using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQRText : MonoBehaviour
{
    [SerializeField] List<Sprite> images;
    Sprite currentImage;
    Image image;
    float interval = 10f;

    void Awake()
    {
        currentImage = GetRandomImage();
        image = GetComponent<Image>();
        image.sprite = currentImage;
        StartCoroutine(SelectRandomItemPeriodically());
    }

    IEnumerator SelectRandomItemPeriodically()
    {
        while (true)
        {
            // if (currentImage.enabled)
            currentImage = GetRandomImage();
            image.sprite = currentImage;
            yield return new WaitForSeconds(interval);
        }
    }

    Sprite GetRandomImage()
    {
        int randomIndex = UnityEngine.Random.Range(0, images.Count);
        return images[randomIndex];
    }
}