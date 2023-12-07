using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;

public class SocketWithTagCheck : XRSocketInteractor
{
    public string targetTag = string.Empty;
    private int mistakeCounter = 0;
    public Text mistakeText;

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);

        if (!MatchUsingTag(interactable))
        {
            CountMistake();
            UpdateMistakeText();
        }
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }

    private void CountMistake()
    {
        mistakeCounter++;
        Debug.Log("Mistake made! Total Mistakes: " + mistakeCounter);
    }

    private void UpdateMistakeText()
    {
        if (mistakeText != null)
        {
            mistakeText.text = "Mistakes: " + mistakeCounter;
        }
    }

    public void ResetMistakes()
    {
        mistakeCounter = 0;
        mistakeText.text = "Mistakes: 0";
        Debug.Log("Mistake counter reset!");
    }
}
