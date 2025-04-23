using UnityEngine;

public class LockableInteractableTest : MonoBehaviour, IInteractable, ILockable
{
    [SerializeField] bool isLocked;
    [TextArea]
    [SerializeField] string customMessage;
    [SerializeField] EventChannel<IInteractable> interactableChannel;
    MeshRenderer Renderer;
    Collider Collider;

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        Collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (isLocked) return;
        Debug.Log(customMessage);
        interactableChannel.Raise(this);
    }
    public void Lock()
    {
        if (isLocked) return;
        Debug.Log(name + " Locked");
        isLocked = true;
        Renderer.enabled = false;
        Collider.enabled = false;
    }
    public void Unlock()
    {
        if(!isLocked) return;
        Debug.Log(name + " Unlocked");
        isLocked = false;
        Renderer.enabled = true;
        Collider.enabled = true;
    }
}
