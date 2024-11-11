using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeOutObjective : MonoBehaviour
{
    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI currentObjectiveText;
    [SerializeField] private TextMeshProUGUI objectiveText;

    private Color currentObjectiveColor;
    private Color objectiveColor;


    [Header("Fade Out Speed")]

    [SerializeField] private float fadeOutSpeed;


    private bool startFadeOut;


    private void Start()
    {
        currentObjectiveColor = currentObjectiveText.color;
        objectiveColor = objectiveText.color;


        startFadeOut = false;


        StartCoroutine(FadeOutCountdown());
    }


    private void Update()
    {
        if (startFadeOut == true)
        {
            currentObjectiveColor.a -= fadeOutSpeed * Time.deltaTime;
            objectiveColor.a -= fadeOutSpeed * Time.deltaTime;


            currentObjectiveColor.a = Mathf.Clamp01(currentObjectiveColor.a);
            objectiveColor.a = Mathf.Clamp01(objectiveColor.a);


            currentObjectiveText.color = currentObjectiveColor;
            objectiveText.color = objectiveColor;
        }
    }


    private IEnumerator FadeOutCountdown()
    {
        yield return new WaitForSeconds(5);


        startFadeOut = true;
    }
}
