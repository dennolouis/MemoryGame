using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private int _settings;
    private const int SettingsNumber = 1;//2;

    public enum EPairNumber
    {
        NotSet = 0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20pairs = 20
    }

    public enum ECategory
    {
        NotSet,
        Fruits,
        Vegetables
    }

    public struct Settings
    {
        public EPairNumber PairNumber;
        public ECategory Category;
    }

    private Settings _gameSettings;


    public static GameSettings Instance;

    private void Awake() 
    {
        if(Instance == null){
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameSettings = new Settings();
        ResetGameSettings();
    }

    public void SetPairNumber(EPairNumber Number)
    {
        if(_gameSettings.PairNumber == EPairNumber.NotSet)
            _settings++;

        _gameSettings.PairNumber = Number;
    }

    public void SetCategory(ECategory Cat)
    {
        if(_gameSettings.Category == ECategory.NotSet)
            _settings++;

        _gameSettings.Category = Cat;
    }

    public EPairNumber GetPairNumber()
    {
        return _gameSettings.PairNumber;
    }

    public ECategory GetCategot()
    {
        return _gameSettings.Category;
    }

    void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.PairNumber = EPairNumber.NotSet;
        _gameSettings.Category = ECategory.Fruits;
    }

    public bool AllSettingsReady()
    {
        return _settings == SettingsNumber;
    }

    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }

    public string GetCardFaceDirectoryName()
    {
        return "CardFaces/";
    }
}
