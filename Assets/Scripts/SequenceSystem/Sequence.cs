using System;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    [SerializeField] GameObject[] stepObjects;
    [SerializeField] EventChannel<IInteractable> interactableEvents;
    public Action OnSequenceComplete;
    readonly List<SequenceStep> steps = new();
    private int currentIndex;
    private bool initialized = false;
    private SequenceStep CurrentStep => steps.Count > 0 ? steps[currentIndex] : null;

    private void Awake() => Initialize();
    private void OnDestroy()
    {
        steps.Clear();
        interactableEvents.OnRaised -= Advance;
    }

    private void Initialize()
    {
        foreach (var step in stepObjects)
        {
            if (step.TryGetComponent(out IInteractable interactable) && step.TryGetComponent(out ILockable lockable))
            {
                steps.Add(new(interactable, lockable));
            }
            else
            {
                Debug.LogError($"Object: {step.name} is not a valid Sequence Step! Aborting Initialization...");
                return;
            }
        }
        interactableEvents.OnRaised += Advance;
        currentIndex = 0;
        initialized = true;
    }

    private void Advance(IInteractable interactable)
    {
        if (!initialized) return;
        if (CurrentStep == null)
        {
            Debug.LogError("Steps Not Initialized Properly!");
            return;
        }
        if (!CurrentStep.Is(interactable)) return;
        Lock();
        currentIndex++;
        if (currentIndex >= steps.Count)
        {
            OnSequenceComplete?.Invoke();
            return;
        }
        Unlock();

    }

    private void Lock()
    {
        if (!initialized) return;
        steps[currentIndex].Lock();
    }

    private void Unlock()
    {
        if (!initialized) return;
        steps[currentIndex].Unlock();
    }

    public void EndSequence()
    {
        if (!initialized) return;
        foreach (var step in steps)
        {
            step.Lock();
        }
    }

    public void StartSequence()
    {
        if (!initialized) return;
        currentIndex = 0;
        steps[currentIndex].Unlock();
    }


    private class SequenceStep
    {
        readonly IInteractable Interactable;
        readonly ILockable Lockable;
        private SequenceStep() { }
        public SequenceStep(IInteractable interactable, ILockable lockable)
        {
            Interactable = interactable;
            Lockable = lockable;
        }
        public void Lock()
        {
            Lockable.Lock();
        }
        public void Unlock()
        {
            Lockable.Unlock();
        }
        public bool Is(IInteractable _interactable) => _interactable == Interactable;
    }
}
