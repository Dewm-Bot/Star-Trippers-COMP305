using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthTextScript : MonoBehaviour
{
    public TMP_Text playerHealthText;

    private PlayerBasicMovement PHS;

    private void Awake()
    {
        PHS = GameObject.Find("Player").GetComponent<PlayerBasicMovement>();
    }
    public void UpdatePlayerHealthText()
    {
        playerHealthText.text = $"Health: {PHS.playerHealth}";
    }
}
