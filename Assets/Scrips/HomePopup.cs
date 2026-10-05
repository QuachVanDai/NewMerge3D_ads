using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePopup : Singleton<HomePopup>
{
    public Button FightButton;


    void OnEnable()
    {
        FightButton.onClick.AddListener(OnFightButtonClicked);
    }
    void OnDisable()
    {
        FightButton.onClick.RemoveListener(OnFightButtonClicked);
    }
    void OnFightButtonClicked()
    {
        GameManager.Instance.StateGame = StateGame.Fight;
    }
}
