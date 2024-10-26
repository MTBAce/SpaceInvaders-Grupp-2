using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverFunctio : MonoBehaviour
{
    [SerializeField] private AudioClip selectEffectClip;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        objectRenderer.enabled = false;
    }
        
    void OnMouseEnter()
    {
        objectRenderer.enabled = true;
        SoundManager.instance.PlaySoundFXClip(selectEffectClip, transform, 1); //Playes sound effect when the mouse hovers over button

    }
    private void OnMouseExit()
    {
        objectRenderer.enabled = false;
    }
}
