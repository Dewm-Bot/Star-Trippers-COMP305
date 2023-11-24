using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthTextScript : MonoBehaviour
{
    public TMP_Text playerHealthText;

    [SerializeField] private PlayerBasicMovement PHS;

    public void UpdatePlayerHealthText()
    {
        playerHealthText.text = $"Health: {PHS.playerHealth}";
    }
}
