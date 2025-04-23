using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interaction Logged!");
    }
}
