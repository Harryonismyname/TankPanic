using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractionChannel", menuName = "ScriptableObjects/Events/InteractionEventChannel")]
public class InteractionEventChannel : EventChannel<IInteractable> {}
