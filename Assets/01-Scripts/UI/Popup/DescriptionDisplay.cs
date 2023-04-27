using TMPro;
using UnityEngine;

public class DescriptionDisplay : PopupDisplay
{
    [SerializeField] private TextMeshProUGUI gradeText;
    [SerializeField] private TextMeshProUGUI domainText;
    [SerializeField] private TextMeshProUGUI clusterText;
    [SerializeField] private TextMeshProUGUI standardIdText;
    [SerializeField] private TextMeshProUGUI standardDescriptionText;
    
    public override void ShowMessage(string message)
    {
        var parsedMessage = message.Split(GameManager.Instance.gameSettings.descriptionSeparator);
        gradeText.text = parsedMessage[0];
        domainText.text = parsedMessage[1];
        clusterText.text = parsedMessage[2];
        standardIdText.text = parsedMessage[3];
        standardDescriptionText.text = parsedMessage[4];
        Open();
    }
}