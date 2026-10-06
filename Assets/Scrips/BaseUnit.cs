using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExampleProject.Gameplay.Characters;
using ExampleProject.Gameplay.Faction;
using ExampleProject.Interface;
using ExampleProject.UI.Shared;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public CharacterAnimator characterAnimator;
    public FactionId Faction;
    public UnitId unitID;
    [SerializeField] HealthBar healthBar;
    [SerializeField] FloatingTextSpawner floatingTextSpawner;
    [SerializeField] Transform hitPosTransform;
    [SerializeField] float health;
    [SerializeField] GameObject effectPickUp;
    public float damage;

    [SerializeField] public float CurrentHealth;
    [SerializeField] public float MaxHealth;
    public bool IsDead => CurrentHealth <= 0;


    public void Init()
    {
        ShowUnit();
        SetHealth();
        EffectPickUp(false);
    }
    void ShowUnit()
    {
        StartCoroutine(IEShowUnit());
        IEnumerator IEShowUnit()
        {
            characterAnimator.Spawn();
            yield return new WaitForSeconds(0.5f);
            characterAnimator.Idle();
        }
    }
    public void SetHealth()
    {
        MaxHealth = health;
        CurrentHealth = MaxHealth;
        healthBar.Initialize(Faction);
    }
    public void PlayIdleAnim()
    {
        characterAnimator.Idle();
    }
    public void PlayAttackAnim()
    {
        characterAnimator.Attack(1);
    }
    public void PlayMoveAnim()
    {
        characterAnimator.Run();
    }
    public void PlayPickUpAnim()
    {
        characterAnimator.PickUp();
    }
    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }
    public void SetLocalPosition(Vector3 _pos)
    {
        transform.localPosition = _pos;
    }
    public void SetLocalRotation(Vector3 _pos)
    {
        transform.localEulerAngles = _pos;
    }
    public void SetScale(float _newScale)
    {
        transform.localScale = _newScale * Vector3.one;
    }
    public void TakeDamage(float _damage)
    {
        CurrentHealth -= _damage;
        var _healthPercent = CurrentHealth / MaxHealth * 100;
        Debug.Log("TakeDamage " + _damage + " CurrentHealth " + CurrentHealth + " MaxHealth " + MaxHealth + " _healthPercent " + _healthPercent);
        healthBar.SetHealthPercent(_healthPercent);
        CheeckDeath();
    }
 void CheeckDeath()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            characterAnimator.Death();
        }
    }
    public void EffectPickUp(bool isShow)
    {
        if(effectPickUp)
        effectPickUp.SetActive(isShow);
    }

}
public enum UnitId
{
    None = 0,

    SpiderMan = 1,
    Iron = 2,
    Hammer = 3,
    Angry = 4,
    BigSpiderMan = 5,

    //Special melee units
    GundamMelee = 5001,

    // Special ranged units
    GundamRanged = 6001,

    // Boss units
    Boss1 = 7001,
    Boss2 = 7002,
    Boss3 = 7003,
    Boss4 = 7004,
    Boss5 = 7005,
    Boss6 = 7006,
    Boss7 = 7007,

    DummyUnit = 9999,
}
