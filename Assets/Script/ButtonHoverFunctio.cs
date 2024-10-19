using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverFunctio : MonoBehaviour
{

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        objectRenderer.enabled = false;
    }
        
    void OnMouseEnter()
    {
        objectRenderer.enabled = true;


    }
    private void OnMouseExit()
    {
        objectRenderer.enabled = false;
    }
}
