using System.Collections;
using System.Collections.Generic;
using ExampleProject.Gameplay.Characters;
using ExampleProject.Gameplay.Faction;
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
    [SerializeField] float damageableDistance;

    public int CurrentHealth { get; private set; }
   public int MaxHealth { get; private set; }
    public void Init()
    {
        ShowUnit();
        SetHealth();

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
