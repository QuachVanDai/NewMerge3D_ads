using ExampleProject.Gameplay.Unit;
using ExampleProject.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ExampleProject.UI.Shared
{
    public class UnitDisplayImage : MonoBehaviour
    {
        #region Fields

        [SerializeField] RawImage renderImage;
        [SerializeField] UI3DDisplay display;
        [SerializeField] UnitModelSpawner modelSpawner;
        [SerializeField] float scale = 1.5f;
        [SerializeField] Vector3 rotation;
        [SerializeField] Vector3 position;

        #endregion

        #region Properties


        #endregion

        #region LifeCycle   

        private void OnEnable()
        {
            display.SetRenderSize(renderImage.rectTransform.rect.size);
            //renderImage.material = display.MaterialInstance;
            renderImage.texture = display.RenderTextureInstance;
        }

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods

        public void Spawn(UnitId _id, bool _isSad = false)
        {
            modelSpawner.Init(_id);
            var _scale = Vector3.one * scale;
            var _rotation = Quaternion.Euler(rotation);
            var _position = position;

            modelSpawner.SetLocalPositionAndRotation(_position, _rotation);
            modelSpawner.SetLocalScale(_scale);
            if (_isSad)
                modelSpawner.RandomDefeat();
            else
                modelSpawner.RandomDance();
        }

        #endregion
    }
}