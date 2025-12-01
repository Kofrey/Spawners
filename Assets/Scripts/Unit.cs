using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Unit : MonoBehaviour
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected UnityEngine.AI.NavMeshAgent _agent;
    protected Vector3 _lastPosition;
    protected float _checkTargetTime = 0.3f;
    protected float _checkTimer;

    protected static int s_isMoveSelf;

    protected virtual void Awake()
    {
        s_isMoveSelf = Animator.StringToHash("isMoveSelf");
        _lastPosition = transform.position;
        _checkTimer = 0;
        StartCoroutine(CheckTarget(_checkTargetTime));
    }

    public void SetTarget(Transform transform)
    {
        _target = transform;
    }

    private IEnumerator CheckTarget(float checkTime)
    {   
        if (_target == null)
            yield return null;

        while(enabled)
        {
            if (_checkTimer < checkTime)
            {
                _checkTimer += Time.deltaTime;
            }
            else
            {
                _agent.SetDestination(_target.position);

                if (transform.position != _lastPosition)
                    _animator.SetBool(s_isMoveSelf, true);
                else
                    _animator.SetBool(s_isMoveSelf, false);
        
                _lastPosition = transform.position;

                _checkTimer += Time.deltaTime - checkTime;
            }

            yield return null;
        }    
    }
}