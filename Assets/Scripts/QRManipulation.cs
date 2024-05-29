using UnityEngine;

public class QRManipulation : MonoBehaviour
{
    [Tooltip("Big QR in the middle -> Key Q")]
    [SerializeField] GameObject BigQR;
    [Tooltip("Small QR in the corner -> Key E")]
    [SerializeField] GameObject SmallQR;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            BigQR.SetActive(!BigQR.activeSelf);
        

        if (Input.GetKeyDown(KeyCode.E))
            SmallQR.SetActive(!SmallQR.activeSelf);
        
    }
}
