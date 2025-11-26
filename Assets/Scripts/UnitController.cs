using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    [Tooltip("Speed ​​at which the unit moves.")]
    [SerializeField] private float _velocity = 250f;
    [SerializeField] private float _rotationSpeed = 2.0f;
    [SerializeField] private Transform _destination;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _destinationSize;


    private float _velocityModifier = 0.02f;

    private void Update()
    {
        if (_destination == null)
            return;

        Vector3 direction = _destination.position - transform.position;

        if (Vector3.Angle(direction, transform.forward) > 0.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
        }
        else 
        {
            float distance = Vector3.Distance(transform.position, _destination.position);

            if (distance >= _destinationSize)
            {
                transform.Translate(Vector3.forward * _velocity * _velocityModifier * Time.deltaTime);

                _animator.SetBool("isMoveSelf", true );
            }
            else
            {
                _animator.SetBool("isMoveSelf", false );
            }
        }
    }

    public void SetDestination(Transform destination)
    {
        _destination = destination;
    }
}