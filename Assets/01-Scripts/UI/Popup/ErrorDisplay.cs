using TMPro;
using UnityEngine;

public class ErrorDisplay : PopupDisplay
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    public override void ShowMessage(string message)
    {
        descriptionText.text = message;
        Open();
    }
}