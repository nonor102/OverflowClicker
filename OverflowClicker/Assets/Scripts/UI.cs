using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Canvas alpha;
    [SerializeField] private Canvas beta;
    [SerializeField] private Canvas gamma;
    [SerializeField] private Canvas delta;
    [SerializeField] private Canvas epsilon;
    [SerializeField] private Canvas settings;

    void Start()
    {
        // èâä˙èÛë‘Ç≈alphaÉpÉlÉãï\é¶
        AlphaCanvasEnable();
    }

    public void AlphaCanvasEnable()
    {
        if(alpha != null)
        {
            alpha.enabled = true;
            beta.enabled = false;
            gamma.enabled = false;
            delta.enabled = false;
            epsilon.enabled = false;
            settings.enabled = false;
            Debug.Log("seeing alpha");
        }
    }
    public void BetaCanvasEnable()
    {
        if(beta != null)
        {
            beta.enabled = true;
            alpha.enabled = false;
            gamma.enabled = false;
            delta.enabled = false;
            epsilon.enabled = false;
            settings.enabled = false;
            Debug.Log("seeing beta");
        }
    }
    public void GammaCanvasEnable()
    {
        if(gamma != null)
        {
            gamma.enabled = true;
            alpha.enabled = false;
            beta.enabled = false;
            delta.enabled = false;
            epsilon.enabled = false;
            settings.enabled = false;
            Debug.Log("seeing gamma");
        }
    }
    public void DeltaCanvasEnable()
    {
        if(delta != null)
        {
            delta.enabled = true;
            alpha.enabled = false;
            beta.enabled = false;
            gamma.enabled = false;
            epsilon.enabled = false;
            settings.enabled = false;
            Debug.Log("seeing delta");
        }
    }
    public void EpsilonCanvasEnable()
    {
        if(epsilon != null)
        {
            epsilon.enabled = true;
            alpha.enabled =false;
            beta.enabled = false;
            gamma.enabled = false;
            delta.enabled = false;
            settings.enabled = false;
            Debug.Log("seeing epsilon");
        }
    }
    public void SettingsCanvasEnable()
    {
        if(settings != null)
        {
            settings.enabled = true;
            alpha.enabled = false;
            beta.enabled = false;
            gamma.enabled = false;
            delta.enabled = false;
            epsilon.enabled = false;
            Debug.Log("seeing settings");
        }
    }

}
