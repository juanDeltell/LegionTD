using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader sceneFader;
    public Button[] levelButtons;

    void Start()
    {
        int levelLocked = PlayerPrefs.GetInt("levelLocked", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelLocked)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string modeName)
    {
        sceneFader.FadeTo(modeName);
    }
    
}
