using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundText;

    void OnEnable()
    {
        StartCoroutine(AnimatedText());
    }

    IEnumerator AnimatedText()
    {
        roundText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while (round < PlayerStats.rounds)
        {
            round++;
            roundText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
