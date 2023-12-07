using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomButton : XRBaseInteractable
{
    private bool isTimerRunning = false;
    private float timer = 0f;
    private int pressCount = 0;
    public Text timerText;
    public TextMeshProUGUI statusText;
    public SocketWithTagCheck mistakesCounter;

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        pressCount++;

        if (pressCount % 3 == 1)
        {
            StartTimer();
        }
        else if (pressCount % 3 == 2)
        {
            StopTimer();
        }
        else if (pressCount % 3 == 0)
        {
            StartCoroutine(ResetXRSockets());
        }
    }

    private void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("Timer started");
        if (timerText != null)
        {
            timerText.enabled = true;
        }
        if (statusText != null)
        {
            statusText.text = "Stop";
        }
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer stopped. Elapsed time: " + timer + " seconds");
        if (statusText != null)
        {
            statusText.text = "Reset";
        }
    }

    private IEnumerator ResetXRSockets()
    {
        timer = 0f;
        Debug.Log("XR sockets reset");

        GameObject rightLeg = GameObject.FindGameObjectWithTag("RightLeg");
        GameObject leftLeg = GameObject.FindGameObjectWithTag("LeftLeg");
        GameObject rightArm = GameObject.FindGameObjectWithTag("RightArm");
        GameObject leftArm = GameObject.FindGameObjectWithTag("LeftArm");
        GameObject head = GameObject.FindGameObjectWithTag("Head");
        GameObject chest = GameObject.FindGameObjectWithTag("Chest");

        if (rightLeg != null && leftLeg != null && rightArm != null && leftArm != null && head != null && chest != null)
        {
            rightLeg.GetComponent<XRGrabInteractable>().enabled = false;
            rightLeg.GetComponent<RightLegOriginalPos>().ResetToOriginalPosition();

            leftLeg.GetComponent<XRGrabInteractable>().enabled = false;
            leftLeg.GetComponent<LeftLegOriginalPos>().ResetToOriginalPosition();

            rightArm.GetComponent<XRGrabInteractable>().enabled = false;
            rightArm.GetComponent<RightArmOriginalPos>().ResetToOriginalPosition();

            leftArm.GetComponent<XRGrabInteractable>().enabled = false;
            leftArm.GetComponent<LeftArmOriginalPos>().ResetToOriginalPosition();

            head.GetComponent<XRGrabInteractable>().enabled = false;
            head.GetComponent<HeadOriginalPos>().ResetToOriginalPosition();

            chest.GetComponent<XRGrabInteractable>().enabled = false;
            chest.GetComponent<ChestOriginalPos>().ResetToOriginalPosition();
            
            yield return new WaitForSeconds(1f);

            rightLeg.GetComponent<XRGrabInteractable>().enabled = true;
            leftLeg.GetComponent<XRGrabInteractable>().enabled = true;
            rightArm.GetComponent<XRGrabInteractable>().enabled = true;
            leftArm.GetComponent<XRGrabInteractable>().enabled = true;
            head.GetComponent<XRGrabInteractable>().enabled = true;
            chest.GetComponent<XRGrabInteractable>().enabled = true;
        }
        if (statusText != null)
        {
            statusText.text = "Start";
        }

        mistakesCounter.ResetMistakes();
        timerText.text = "Timer: 0.000";
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Timer: " + timer.ToString("F3", CultureInfo.InvariantCulture);
            }
        }
    }
}
