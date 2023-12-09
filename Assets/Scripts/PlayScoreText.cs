using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayScoreText : MonoBehaviour
{

    public TMP_Text playerScoreText;
    [SerializeField] private PlayerBasicMovement PBM;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerScoreText.text = $"Score: {PBM.score}";
    }
}

