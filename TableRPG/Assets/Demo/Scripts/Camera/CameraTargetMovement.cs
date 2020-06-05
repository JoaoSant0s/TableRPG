using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1f)]
    private float velocity;

    private InputCamera controls;
    private bool moving;
    private Vector3 lastPosition;

    private const float velocityDivisionFactor = 10f;

    private void Awake()
    {
        this.controls = new InputCamera();

        this.controls.Camera.StartMovement.performed += ctx => SetMoving(true);
        this.controls.Camera.StartMovement.canceled += ctx => SetMoving(false);

        this.controls.Camera.Movement.performed += ctx => TryMove(ctx.ReadValue<Vector2>());
    }

    private void OnDestroy()
    {
        this.controls.Camera.StartMovement.performed -= ctx => SetMoving(true);
        this.controls.Camera.StartMovement.canceled -= ctx => SetMoving(false);

        this.controls.Camera.Movement.performed -= ctx => TryMove(ctx.ReadValue<Vector2>());
    }    

    private void OnEnable()
    {
        this.controls.Enable();
    }

    private void OnDisable()
    {
        this.controls.Disable();
    }

    private void PrepareMovement()
    {
        var screenPoint = this.controls.Camera.Movement.ReadValue<Vector2>();
        Vector3 worldPoint = screenPoint.ScreenToWorldPoint();

        this.lastPosition = worldPoint;
    }

    private void SetMoving(bool isMoving)
    {
        if (isMoving) PrepareMovement();
        this.moving = isMoving;
    }

    private void TryMove(Vector2 position)
    {
        if (!this.moving) return;

        Vector3 currentPoint = position.ScreenToWorldPoint();

        var offset = currentPoint - this.lastPosition;

        transform.position -= (offset * this.velocity/velocityDivisionFactor);
    }
}
