using System;
using ExampleProject.Gameplay.Characters;
using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour
{
    #region Fields
    public EndCardData endCardDataWin;
    public EndCardData endCardDataLose;
    public Text textDes;
    public Text textCta;
    public Text textYouWin;
    public Image imgCTA;
    public CharacterAnimator CharacterAnimator;
    public EndCardType EndCardType;

    protected void OnEnable()
    {
        ShowCard(EndCardType);

    }
    void ChangeEndCardType(EndCardType type)
    {

    }
    #endregion

    #region Properties

    #endregion

    #region LifeCycle

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    public void ShowCard(EndCardType endCardType)
    {
        EndCardData _endCardData = new EndCardData();
        gameObject.SetActive(true);
        switch (endCardType)
        {
            case EndCardType.Win:
                _endCardData = endCardDataWin;
                CharacterAnimator.RandomDance();
                break;
            case EndCardType.Lose:
                _endCardData = endCardDataLose;
                CharacterAnimator.RandomDefeat();
                break;
            case EndCardType.Draw:
                break;
            default:
                break;
        }

        textDes.text = _endCardData.desEndCard.ToUpper();
        textCta.text = _endCardData.ctaEndCard.ToUpper();
        textYouWin.text = _endCardData.youWinLose.ToUpper();
        Luna.Unity.LifeCycle.GameEnded();
    }

    #endregion

}
[Serializable]
public class EndCardData
{
    public string desEndCard;
    public string ctaEndCard;
    public string youWinLose;
    public EndCardData()
    {

    }
    public EndCardData(EndCardData endCardData)
    {
        desEndCard = endCardData.desEndCard;
        ctaEndCard = endCardData.ctaEndCard;
        youWinLose = endCardData.youWinLose;
    }
}
public enum EndCardType
{
    None = -1,
    Win = 0,
    Lose = 1,
    Draw = 2
}