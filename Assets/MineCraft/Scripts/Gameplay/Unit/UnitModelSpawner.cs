using ExampleProject.Gameplay.Characters;
using ExampleProject.Tools;
using System;
using UnityEngine;

namespace ExampleProject.Gameplay.Unit
{
    public class UnitModelSpawner : MonoBehaviour
    {
        #region Fields

        // [SerializeField] private UnitModel model;
        [SerializeField] private BaseUnit modelTransform;
        [SerializeField] private UnitId UnitId;

        #endregion

        #region Properties

        public CharacterAnimator CharacterAnimator;
        public SkinnedMeshRenderer MeshRenderer;
        public Transform LeftHandTransform
        {
            get;
            private set;
        }
        public Transform RightHandTransform
        {
            get;
            private set;
        }


        #endregion

        #region LifeCycle   



        #endregion

        #region Private Methods



        #endregion

        #region Public Methods

        public void Init(UnitId _id)
        {
            //  Helpers.DestroyAllChilds(this.transform);
            // var _data = Units.GetUnitData(_id);
            // CharacterAnimator.SetAnimatorOverrideController(_data.animatorOverride);

            // MeshRenderer.sharedMesh = _data.mesh;
            // var _materials = MeshRenderer.sharedMaterials;
            // _materials[0] = _data.material;
            // MeshRenderer.sharedMaterials = _materials;

            // LeftHandTransform = modelTransform.LeftHandTransform;
            // RightHandTransform = modelTransform.t.RightHandTransform;
        }
        public void Clear()
        {
            CharacterAnimator = null;
            MeshRenderer = null;
            LeftHandTransform = null;
            RightHandTransform = null;
        }

        public void RandomDance()
        {
            CharacterAnimator.RandomDance();
        }
        public void RandomDefeat()
        {
            CharacterAnimator.RandomDefeat();
        }
        public void Death()
        {
            CharacterAnimator.Death();
        }
        public void Run()
        {
            CharacterAnimator.Run();
        }
        public void Attack(float _interval)
        {
            CharacterAnimator.Attack(_interval);
        }
        public void Idle()
        {
            CharacterAnimator.Idle();
        }
        public void PickUp()
        {
            CharacterAnimator.PickUp();
        }
        // public void Spawn(Action _onComplete)
        // {
        //     CharacterAnimator.Spawn(_onComplete);
        // }
        public void Jump(float _speed = 1)
        {
            CharacterAnimator.Jump(_speed);
        }
        public void SetLocalPositionAndRotation(Vector3 _position, Quaternion _rotation)
        {
            //modelTransform.SetLocalPositionAndRotation(_position, _rotation);
        }
        public void SetLocalScale(Vector3 _scale)
        {
            //  modelTransform.SetLocalScale(_scale);
        }
        // public bool IsSpawnAnimPlaying()
        // {
        //     return CharacterAnimator.IsSpawnAnimPlaying();
        // }
        // public bool IsIdleAnimPlaying()
        // {
        //     return CharacterAnimator.IsIdleAnimPlaying();
        // }
        // public bool IsPickUpAnimPlaying()
        // {
        //     return CharacterAnimator.IsPickUpAnimPlaying();
        // }

        #endregion
    }
}
