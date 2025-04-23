using UnityEngine;
using PolearmStudios.Input;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float interactionRange = 5f;
    [SerializeField] LayerMask interactionLayer;
    InputManager manager;

    private bool Raycast(out RaycastHit hit) => Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactionLayer);

    private void Awake() => Initialize();

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.OnLeftTriggerPulled -= Interact;
        }
    }

    private void Initialize()
    {
        manager = transform.parent.GetComponent<InputManager>();
        manager.OnLeftTriggerPulled += Interact;
    }

    private void Interact()
    {
        if (Raycast(out RaycastHit hit) && hit.transform.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
