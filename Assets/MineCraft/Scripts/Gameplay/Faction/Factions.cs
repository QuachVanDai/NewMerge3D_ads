using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExampleProject.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExampleProject.Gameplay.Faction
{
    [CreateAssetMenu(fileName = "Factions", menuName = "ScriptableObjects/Faction/Factions")]
    public class Factions : ScriptableObject
    {
        #region Fields

        [SerializeField] List<FactionData> resourceDataList = new();

        const string resourceFolderPath = "Data/Factions";
        readonly static ResourceLoader<Factions> resourceLoader = new(resourceFolderPath);

        #endregion

        #region Properties



        #endregion

        #region LifeCycle   

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods

        public static List<FactionData> GetResourceDataList()
        {
            return resourceLoader.Resource.resourceDataList;
        }

        public static FactionData GetResourceData(FactionId _id)
        {
            var _data = GetResourceDataList().Find(x => x.id.Equals(_id));
            return _data;
        }

        #endregion
    }
}