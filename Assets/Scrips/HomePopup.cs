using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePopup : Singleton<HomePopup>
{
    public Button FightButton;
    public Transform PanelDragToMerge;
    public Transform PanelFight;
    public EndCard PanelEndCard;
    public void ShowPanelDragToMerge(bool isShow)
    {
        PanelDragToMerge.gameObject.SetActive(isShow);
    }
    public void ShowPanelFight(bool isShow)
    {
        PanelFight.gameObject.SetActive(isShow);
    }
    public void ShowPanelEndCard(EndCardType EndCardType)
    {
        PanelEndCard.ShowCard(EndCardType);
    }
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
