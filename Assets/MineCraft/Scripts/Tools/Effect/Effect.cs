using DG.Tweening;
using ExampleProject.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools.Effect
{
    public class Effect : MonoBehaviour
    {
        // [SerializeField] MainParticleSystem mainParticleSystem;
        // [SerializeField] bool isDestroyAfterStop = true;
        // [SerializeField] List<ParticleSystem> particleSystems = new();

        // public bool IsPlaying
        // {
        //     get
        //     {
        //         if (mainParticleSystem != null)
        //             return mainParticleSystem.ThisParticleSystem.isPlaying;
        //         else
        //             return false;
        //     }
        // }

        // public void OnParticleSystemStoppedListener()
        // {
        //     if (isDestroyAfterStop)
        //         Destroy(this.gameObject);
        // }

        // public void Init(Vector3 _pos, Transform _parent = null)
        // {
        //     this.transform.position = _pos;
        //     SetParent(_parent);
        // }
        // public void SetParent(Transform _parent)
        // {
        //     this.transform.SetParent(_parent);
        // }
        // public void SetRotation(Quaternion _rotation)
        // {
        //     this.transform.rotation = _rotation;
        // }
        // public void SetLocalPosition(Vector3 _localPos)
        // {
        //     this.transform.localPosition = _localPos;
        // }
        // public void DOMoveY(float _value, float _duration)
        // {
        //     this.transform.DOMoveY(_value, _duration);
        // }
        // public void Play()
        // {
        //     //if (IsPlaying)
        //     //    return;
        //     mainParticleSystem.ThisParticleSystem.Play();
        // }
        // public void Reset()
        // {
        //     mainParticleSystem.ThisParticleSystem.Clear();
        //     mainParticleSystem.ThisParticleSystem.Stop();
        // }
        // public void Stop()
        // {
        //     mainParticleSystem.ThisParticleSystem.Stop();
        // }
        // public void Pause()
        // {
        //     mainParticleSystem.ThisParticleSystem.Pause();
        // }
        // public void SetRateOverTime(float _value)
        // {
        //     var emission = mainParticleSystem.ThisParticleSystem.emission;
        //     emission.rateOverTime = _value;
        // }
        // public void SetSpeedSimulation(float _value)
        // {
        //     foreach (var _ps in particleSystems)
        //     {
        //         if (_ps == null)
        //             continue;
        //         var main = _ps.main;
        //         main.simulationSpeed = _value;
        //     }
        // }
        // public void DOMove(Vector3 _target, float _duration, float _delay, Action _onCompleteAction = null)
        // {
        //     this.transform.DOMove(_target, _duration).SetDelay(_delay)
        //         .OnComplete(() => { _onCompleteAction?.Invoke(); });
        // }
        // public virtual void SetColor(Color _color)
        // {
        //     if (mainParticleSystem == null || mainParticleSystem.ThisParticleSystem == null)
        //         return;

        //     var _main = mainParticleSystem.ThisParticleSystem.main;
        //     _main.startColor = _color;
        // }

        // // Fix burst, only for ui swarm effect
        // public void SetBurstCount(short _count)
        // {
        //     var _emission = mainParticleSystem.ThisParticleSystem.emission;
        //     _count = (short)Mathf.Clamp(_count, 0, 10);
        //     ParticleSystem.Burst _burst = new(0, _count, _count, 1, 0.01f)
        //     {
        //         probability = 1
        //     };
        //     _emission.SetBurst(0, _burst);
        // }

        // public void SetActive(bool _active)
        // {
        //     this.gameObject.SetActive(_active);
        // }
        // void GetAllParticleSystems()
        // {
        //     particleSystems.Clear();
        //     particleSystems = Helpers.GetAllChildrenComponent<ParticleSystem>(this.transform);
        // }
    }
}