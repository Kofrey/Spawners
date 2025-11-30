using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [Tooltip("Speed ​​at which the unit moves.")]
    [SerializeField] private float _velocity = 250f;
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Animator _animator;

    private static int s_isMoveSelf;
    private float _velocityModifier = 0.02f;

    private void Awake()
    {
        s_isMoveSelf = Animator.StringToHash("isMoveSelf");
    }

    private void Update()
    {
        if (_direction == null)
            return;

        if (Vector3.Angle(_direction, transform.forward) > 0.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), _rotationSpeed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector3.forward * _velocity * _velocityModifier * Time.deltaTime);

            _animator.SetBool(s_isMoveSelf, true);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}