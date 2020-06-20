using UnityEngine;
using UnityEngine.InputSystem;
public class CameraTargetMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1f)]
    private float velocity;

    private InputCamera controls;
    private Vector3 lastPosition;

    private const float velocityDivisionFactor = 10f;

    private void Awake()
    {
        this.controls = new InputCamera();

        this.controls.Camera.StartMovement.performed += ctx => SetMoving(true);
        this.controls.Camera.StartMovement.canceled += ctx => SetMoving(false);
    }

    private void OnDestroy()
    {
        this.controls.Camera.StartMovement.performed -= ctx => SetMoving(true);
        this.controls.Camera.StartMovement.canceled -= ctx => SetMoving(false);
    }

    private void OnEnable()
    {
        this.controls.Enable();
    }

    private void OnDisable()
    {
        this.controls.Disable();
    }

    private void SubscribeMovementEvent()
    {
        this.controls.Camera.Movement.performed += MoveContext;
    }

    private void DisubscriveMovementEvent()
    {
        this.controls.Camera.Movement.performed -= MoveContext;
    }

    private void MoveContext(InputAction.CallbackContext ctx)
    {
        if (UtilWrapper.IsPointOverUIObject()) return;

        Move(ctx.ReadValue<Vector2>());
    }

    private void PrepareMovement()
    {
        var screenPoint = this.controls.Camera.Movement.ReadValue<Vector2>();
        Vector3 worldPoint = screenPoint.ScreenToWorldPoint();

        this.lastPosition = worldPoint;
    }

    private void SetMoving(bool isMoving)
    {
        if (isMoving)
        {
            PrepareMovement();
            SubscribeMovementEvent();
        }
        else
        {
            DisubscriveMovementEvent();
        }
    }

    private void Move(Vector2 position)
    {
        Vector3 currentPoint = position.ScreenToWorldPoint();

        var offset = currentPoint - this.lastPosition;

        transform.position -= (offset * this.velocity / velocityDivisionFactor);
    }
}
