using UnityEngine;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{

    public SceneFader sceneFader;
    public Button[] modeButtons;

    void Start()
    {
        int modesActived = PlayerPrefs.GetInt("modeActived", 1);

        for (int i = 0; i < modeButtons.Length; i++)
        {
            if(i + 1 > modesActived)
                modeButtons[i].interactable = false;
        }
    }

    public void Select(string modeName)
    {
        sceneFader.FadeTo(modeName);
    }
    
}
