using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameButton : MonoBehaviour
{

    public enum EButtonType
    {
        NotSet,
        PairNumberBtn,
        CategoryBtn
    };

    public EButtonType ButtonType = EButtonType.NotSet;

    [HideInInspector]
    public GameSettings.EPairNumber PairNumber = GameSettings.EPairNumber.NotSet;
    
    [HideInInspector]
    public GameSettings.ECategory Category = GameSettings.ECategory.NotSet;

    [SerializeField]
   private GameSettings.EPairNumber Pairs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGameOption(string GameSceneName)
    {
        switch(this.ButtonType)
        {
            case EButtonType.PairNumberBtn:
                GameSettings.Instance.SetPairNumber(this.PairNumber);
                break;
            case EButtonType.CategoryBtn:
                GameSettings.Instance.SetCategory(this.Category);
                break;
        }

        if (GameSettings.Instance.AllSettingsReady())
        {
            SceneManager.LoadScene(GameSceneName);
        }

    }
}
