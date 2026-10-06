using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour
{
    #region Fields
    public EndCardType endCardType;
    public string[] desEndCard;
    public string[] ctaEndCard;
    public Sprite[] imgEndCard;
    public Text textDes;
    public Text textCta;
    public Image imgCTA;

    public EndCardType EndCardType
    {
        get
        {
            return endCardType;
        }
        set
        {
            endCardType = value;

        }
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

    public void ShowCard(int id)
    {
        switch (endCardType)
        {
            case EndCardType.Win:
                break;
            case EndCardType.Lose:
                break;
            case EndCardType.Draw:
                break;
            default:
                break;
        }
        gameObject.SetActive(true);
        textDes.text = desEndCard[id].ToUpper();
        textCta.text = ctaEndCard[id].ToUpper();
        // imgCTA.sprite = imgEndCard[id];
        Luna.Unity.LifeCycle.GameEnded();
    }

    #endregion

}
public enum EndCardType
{
    None = -1,
    Win = 0,
    Lose = 1,
    Draw = 2
}