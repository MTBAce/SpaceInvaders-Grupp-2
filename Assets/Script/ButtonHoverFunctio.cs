using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverFunctio : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        objectRenderer.enabled = false;
    }
        
    // Update is called once per frame
    void OnMouseEnter()
    {
        objectRenderer.enabled = true;
    }
    private void OnMouseExit()
    {
        objectRenderer.enabled = false;
    }
}
