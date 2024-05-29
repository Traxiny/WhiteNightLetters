using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggler : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            image.enabled = !image.enabled;
        }
    }
}
