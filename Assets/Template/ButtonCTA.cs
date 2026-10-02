using UnityEngine;
using UnityEngine.UI;
namespace Template
{
    public class ButtonCTA : MonoBehaviour
    {
        private Button ButtonCta=>GetComponent<Button>();

        public void _OnCTA()
        {
            Luna.Unity.Playable.InstallFullGame();
        }
        // private void OnEnable()
        // {
        //     ButtonCta.onClick.AddListener(OnListen);
        // }
        //
        // private void OnDisable()
        // {
        //     ButtonCta.onClick.RemoveListener(OnListen);
        // }
        //
        // private void OnListen()
        // {
        //     Luna.Unity.Playable.InstallFullGame();
        //     Debug.Log("Install");
        // }
    }
}
