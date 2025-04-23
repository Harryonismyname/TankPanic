using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] Sequence[] Sequences;
    private int currentIndex;
    private void Awake() => Initialize();
    private void OnDestroy()
    {
        foreach (var sequence in Sequences)
        {
            sequence.OnSequenceComplete -= Advance;
        }
    }

    private void Start()
    {
        foreach (var sequence in Sequences)
        {
            sequence.EndSequence();
        }
        StartCurrentSequence();
    }

    private void Initialize()
    {
        foreach (var sequence in Sequences)
        {
            sequence.OnSequenceComplete += Advance;
        }
        currentIndex = 0;
    }

    private void Advance()
    {
        EndCurrentSequence();
        currentIndex++;
        if (currentIndex >= Sequences.Length) currentIndex = 0;
        StartCurrentSequence();
    }

    private void StartCurrentSequence() => Sequences[currentIndex].StartSequence();
    private void EndCurrentSequence() => Sequences[currentIndex].EndSequence();
}
