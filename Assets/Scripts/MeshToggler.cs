using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshToggler : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            meshRenderer.enabled = !meshRenderer.enabled;
        }
    }
}
