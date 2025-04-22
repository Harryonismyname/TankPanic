using UnityEngine;
using PolearmStudios.Input;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 maxEulerAngles = new(90, 180, 0);
    [SerializeField] float turningSpeed = 5f;
    [SerializeField] bool invertedControls = false;
     InputManager inputManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.OnAim += Aim;
    }

    private void Aim(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            Vector3 newRot = invertedControls ? new(input.y, input.x) : new(-input.y, input.x);
            float turnDelta = Time.fixedDeltaTime * turningSpeed;
            transform.Rotate(turnDelta * newRot);
            Vector3 eulerAngles = transform.localEulerAngles;

            eulerAngles.x = (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x;
            eulerAngles.y = (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y;
            eulerAngles.z = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z;

            eulerAngles.x = Mathf.Clamp(eulerAngles.x, -maxEulerAngles.x, maxEulerAngles.x);
            eulerAngles.y = Mathf.Clamp(eulerAngles.y, -maxEulerAngles.y, maxEulerAngles.y);
            eulerAngles.z = Mathf.Clamp(eulerAngles.z, -maxEulerAngles.z, maxEulerAngles.z);

            transform.localRotation = Quaternion.Euler(eulerAngles);
        }
    }
}
