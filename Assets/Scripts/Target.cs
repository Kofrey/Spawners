using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : Unit
{
    [SerializeField] private List<Transform> _wayPoints;
    private int _currentIndex;
    private float _distanceToReachPoint = 1f;

    protected override void Awake()
    {
        s_isMoveSelf = Animator.StringToHash("isMoveSelf");
        _target = _wayPoints[0];
        _currentIndex = 0;
        _lastPosition = transform.position;
        _checkTimer = 0;
        StartCoroutine(CheckTarget(_checkTargetTime));
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
                if(IsPointReached())
                    NextTarget();

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

    private void NextTarget()
    {
        if (_currentIndex < _wayPoints.Count - 1)
        {
            _currentIndex++;
            _target = _wayPoints[_currentIndex];
        }
        else
        {
            _target = _wayPoints[0];
            _currentIndex = 0;
        }
    }

    private bool IsPointReached()
    {
        return (_target.position - transform.position).sqrMagnitude <= _distanceToReachPoint * _distanceToReachPoint;
    }
}
