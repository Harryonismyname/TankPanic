using UnityEngine;

public class SequenceTest : MonoBehaviour
{
    [SerializeField] Sequence sequence;

    private void Awake()
    {
        sequence.OnSequenceComplete += Test;
    }
    private void OnDestroy()
    {
        sequence.OnSequenceComplete -= Test;
    }

    private void Test()
    {
        Debug.Log("Sequence Complete!");
        Debug.Log("Ending Sequence...");
        sequence.EndSequence();
        Debug.Log("Restarting Sequence...");
        sequence.StartSequence();

    }
}
