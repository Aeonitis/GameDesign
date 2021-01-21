using UnityEngine;

// Manages intended input to expected behaviour
namespace MoveWithInput
{
    public class MovePlayerWithInput : MonoBehaviour
    {
        // Gamepad Controller Input
        private PlayerControls _playerControls;
        private Vector2 _gamepadStickMoveVector2;

        // Read-only modifiers for intended input
        // private bool InputShouldMoveLocalRight => Input.GetKey(KeyCode.D);
        // private bool InputShouldMoveLocalLeft => Input.GetKey(KeyCode.A);
        // private bool InputShouldMoveLocalForward => Input.GetKey(KeyCode.W);
        // private bool InputShouldMoveLocalBackward => Input.GetKey(KeyCode.S);
        // private bool InputShouldWorldTurnLeft => Input.GetKey(KeyCode.Q);
        // private bool InputShouldWorldTurnRight => Input.GetKey(KeyCode.E);
        // private bool InputShouldResetPosition => Input.GetKey(KeyCode.Space);

        // Essentially a 'metres per second' speed
        [SerializeField] private float movementSpeed = 2f;

        // // Speed of turn rotation
        // [SerializeField] private float turnSpeed = 30f;

        // I want everything to stay on the plane for now
        private const float UniformHeightOnPlane = 1f;

        // Where player jumps/resets to
        private static readonly Vector3 StartPosition = new Vector3(0, UniformHeightOnPlane, 0);

        private void Awake()
        {
            SetUpPlayerControls();
        }

        private void SetUpPlayerControls()
        {
            _playerControls = new PlayerControls();
            _playerControls.PlayerInputActionMap.Pass.performed += callbackContext => PlayerPassed();
            _playerControls.PlayerInputActionMap.Move.performed += callbackContext =>
                _gamepadStickMoveVector2 = callbackContext.ReadValue<Vector2>();

            // Reset when not moving thumbstick
            _playerControls.PlayerInputActionMap.Move.canceled +=
                callbackContext => _gamepadStickMoveVector2 = Vector2.zero;
        }

        void PlayerPassed()
        {
            print("Pass made.");
        }

        private void OnEnable()
        {
            _playerControls.PlayerInputActionMap.Enable();
        }

        private void OnDisable()
        {
            _playerControls.PlayerInputActionMap.Disable();
        }

        /**
         * As long as input keys are held pressed DOWN, you move.
         */
        void Update()
        {
            Vector2 input2DMovement = new Vector2(_gamepadStickMoveVector2.x, _gamepadStickMoveVector2.y) *
                                      (movementSpeed * Time.deltaTime); //*DeltaTime makes update frame-independent

            // Move object by this vector
            transform.Translate(new Vector3(input2DMovement.x, 0, input2DMovement.y), Space.World);

            // if (InputShouldWorldTurnLeft)
            //     transform.TurnLeftOnGlobalAxes(turnSpeed);
            //
            // if (InputShouldWorldTurnRight)
            //     transform.TurnRightOnGlobalAxes(turnSpeed);
            //

            //     if (InputShouldMoveLocalRight)
            //         transform.MoveRightOnLocalAxes(movementSpeed);
            //
            //     if (InputShouldMoveLocalLeft)
            //         transform.MoveLeftOnLocalAxes(movementSpeed);
            //
            //     if (InputShouldMoveLocalForward)
            //         transform.MoveForwardOnLocalAxes(movementSpeed);
            //
            //     if (InputShouldMoveLocalBackward)
            //         transform.MoveBackwardOnLocalAxes(movementSpeed);


            // // Reset position
            // if (InputShouldResetPosition)
            //     transform.SetToPosition(StartPosition);
        }
    }
}