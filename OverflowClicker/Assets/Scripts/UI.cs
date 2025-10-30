using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Sprite lockPic; // 錠前の画像
    [SerializeField] private Sprite alphaAvailablePic; // alphaの画像
    [SerializeField] private Sprite betaAvailablePic; // betaの普通の時の画像
    [SerializeField] private Sprite gammaAvailablePic; // gammaの普通の時の画像
    [SerializeField] private Sprite deltaAvailablePic; // deltaの普通の時の画像
    [SerializeField] private Sprite epsilonAvailablePic; // epsilonの普通の時の画像
    [SerializeField] private Sprite settingsAvailablePic; // 設定の画像

    [SerializeField] private Canvas alpha;
    [SerializeField] private Canvas beta;
    [SerializeField] private Canvas gamma;
    [SerializeField] private Canvas delta;
    [SerializeField] private Canvas epsilon;
    [SerializeField] private Canvas settings;

    [SerializeField] private Button alphaButton;
    [SerializeField] private Button betaButton;
    [SerializeField] private Button gammaButton;
    [SerializeField] private Button deltaButton;
    [SerializeField] private Button epsilonButton;
    [SerializeField] private Button settingsButton;

    void Start()
    {
        // 初期状態でalphaパネル表示
        AlphaCanvasEnable();
        alphaButton.interactable = true;
        settingsButton.interactable = true;
    }

    private void Update()
    {
        UnlockTab();
    }

    private void UnlockTab()
    {
        alphaButton.image.sprite = alphaAvailablePic;
        betaButton.image.sprite = lockPic;
        betaButton.interactable = false;
        gammaButton.image.sprite = lockPic;
        gammaButton.interactable = false;
        deltaButton.image.sprite = lockPic;
        deltaButton.interactable = false;
        epsilonButton.image.sprite = lockPic;
        epsilonButton.interactable = false;
        settingsButton.image.sprite = settingsAvailablePic;
        if (GameManager.Instance.IsArrivedBeta)
        {
            betaButton.image.sprite = betaAvailablePic;
            betaButton.interactable = true;
        }
        if (GameManager.Instance.IsArrivedGamma)
        {
            gammaButton.image.sprite = gammaAvailablePic;
            gammaButton.interactable = true;
        }
        if (GameManager.Instance.IsArrivedDelta)
        {
            deltaButton.image.sprite = deltaAvailablePic;
            deltaButton.interactable = true;
        }
        if (GameManager.Instance.IsArrivedEpsilon)
        {
            epsilonButton.image.sprite = epsilonAvailablePic;
            epsilonButton.interactable = true;
        }
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
