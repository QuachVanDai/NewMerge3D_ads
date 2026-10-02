using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour
{
    #region Fields

    public string[] desEndCard;
    public string[] ctaEndCard;
    public Sprite[] imgEndCard;
    public Text textDes;
    public Text textCta;
    public Image imgCTA;

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
        gameObject.SetActive(true);
        textDes.text = desEndCard[id].ToUpper();
        textCta.text = ctaEndCard[id].ToUpper();
       // imgCTA.sprite = imgEndCard[id];
        Luna.Unity.LifeCycle.GameEnded();
    }

    #endregion
}