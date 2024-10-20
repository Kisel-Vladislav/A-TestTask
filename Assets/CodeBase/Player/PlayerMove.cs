using CodeBase.Service;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed;

        private Camera _camera;
        private IInputService _inputService;

        private void Awake()
        {
            _camera = Camera.main;
            _inputService = new StandaloneInputService();
        }

        private void Update()
        {
            var _movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                _movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                _movementVector.y = 0;
                _movementVector.Normalize();

                transform.forward = _movementVector;
            }
            _movementVector += Physics.gravity;
            _characterController.Move(_movementVector * _moveSpeed * Time.deltaTime);
        }
    }
}
