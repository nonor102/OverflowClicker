using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button click;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + GameManager.Instance.SbyteScoreForDisplay;
    }

    public void OnSbyteButtonClicked()
    {
        GameManager.Instance.UpdateSbyteScore();
    }

    /*void ScoreDisplayUpdate()
    {
        scoreText.text = "" + GameManager.Instance.SbyteScoreForDisplay;
    }*/
}
