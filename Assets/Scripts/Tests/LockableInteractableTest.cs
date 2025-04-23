using UnityEngine;

public class LockableInteractableTest : MonoBehaviour, IInteractable, ILockable
{
    [SerializeField] bool isLocked;
    [SerializeField] EventChannel<IInteractable> interactableChannel;

    public void Interact()
    {
        if (isLocked) return;
        Debug.Log(name + " was interacted with");
        interactableChannel.Raise(this);
    }
    public void Lock()
    {
        if (isLocked) return;
        Debug.Log(name + " Locked");
        isLocked = true;
    }
    public void Unlock()
    {
        if(!isLocked) return;
        Debug.Log(name + " Unlocked");
        isLocked = false;
    }
}
