using UnityEngine;
using UnityEngine.Serialization;
using ExampleProject.Tools;

namespace ExampleProject.Gameplay.Faction
{
    [CreateAssetMenu(fileName = "FactionData", menuName = "ScriptableObjects/Faction/FactionData")]
    public class FactionData : ScriptableObject
    {
        #region Fields

        public FactionId id;
        public Sprite icon;
        public Sprite fightBackground;
        public Sprite healthBarBackground;
        public Sprite healthBarForeground;

        #endregion

        #region Properties



        #endregion
    }

    public enum FactionId
    {
        None = 0,
        Friendly = 1,
        Enemy = 2,
    }
}