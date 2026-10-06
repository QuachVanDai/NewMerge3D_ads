using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExampleProject.Gameplay.Characters;
using ExampleProject.Interface;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    [SerializeField] BaseUnit thisBase;
    public RigidbodyVelocityTargetMovable TargetMovable;
    public CharacterAnimator characterAnimator;
    public float AttackSpeed = 1f;
    public float Interval => 1f / AttackSpeed;
    public float LastAttackTime { get; private set; }
    [SerializeField] float damageableDistance;
    [SerializeField] BaseUnit targetUnit;
    [SerializeField]
    protected void OnEnable()
    {
        // Damageable.OnDie += OnDieListener;
        // BasicAttack.OnStartAttack += OnStartAttackListner;
        characterAnimator.onAttackPointReached += OnAttackPointReachedListener;

        TargetMovable.OnStartMove += OnStartMoveListener;
        TargetMovable.OnStopMove += OnStopMoveListener;
        // EventDispatcher.Instance.AddListener(EventName.OnBattleStart, OnBattleStartListener);
        //EventDispatcher.Instance.AddListener(EventName.OnVictory, OnVictoryListener);
    }
    protected void OnDisable()
    {
        // Damageable.OnDie -= OnDieListener;
        // BasicAttack.OnStartAttack -= OnStartAttackListner;
        characterAnimator.onAttackPointReached -= OnAttackPointReachedListener;

        TargetMovable.OnStartMove -= OnStartMoveListener;
        TargetMovable.OnStopMove -= OnStopMoveListener;
        // EventDispatcher.Instance.RemoveListener(EventName.OnBattleStart, OnBattleStartListener);
        // EventDispatcher.Instance.RemoveListener(EventName.OnVictory, OnVictoryListener);

        this.transform.DOKill();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.StateGame != StateGame.Fight)
            return;
        UnitNear();
        if (!targetUnit) return;
        if (IsInAttackRange())
        {
            if (TargetMovable.IsMoving)
                TargetMovable.StopMove();
            TryAttack();
        }
        else
        {
            TargetMovable.MoveTo(targetUnit.transform);
        }
    }
    void TryAttack()
    {
        if (Time.time < LastAttackTime + Interval)
            return;

        LastAttackTime = Time.time;
        thisBase.PlayAttackAnim();
    }
    public bool IsInAttackRange()
    {
        if (targetUnit == null)
            return false;
        float _distance = (transform.position - targetUnit.transform.position).sqrMagnitude;

        // Add DamageableDistance to allow attacking when the target is too big and its center is outside of the attack range but its edge is still within the range
        return _distance <= damageableDistance + 1;
    }


    public void UnitNear()
    {
        BaseUnit nearestUnit = null;
        float nearestDistanceSqr = 999;

        for (int i = 0; i < EnemyController.Instance.BaseUnits.Count; i++)
        {
            BaseUnit unit = EnemyController.Instance.BaseUnits[i].unit;
            if (unit == null) continue;

            float distanceSqr = (transform.position - unit.transform.position).sqrMagnitude;
            if (distanceSqr > nearestDistanceSqr) continue;

            nearestDistanceSqr = distanceSqr;
            nearestUnit = unit;
        }

        targetUnit = nearestUnit;
    }
    protected void OnStartMoveListener()
    {
        thisBase.PlayMoveAnim();
        //  basicAttack.ResetAttackTimer();
    }
    protected void OnStopMoveListener()
    {
        // if (!IsHasTarget)
        //     model.Idle();
    }
    public void OnAttackPointReachedListener()
    {
        targetUnit.TakeDamage(thisBase.damage);
        Debug.Log("OnAttackPointReachedListener " + gameObject.name);
    }
}
