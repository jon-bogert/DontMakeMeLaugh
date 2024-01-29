using UnityEngine;
using UnityEngine.Events;

public class CreditsEnd : MonoBehaviour
{
    [SerializeField] UnityEvent onComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onComplete?.Invoke();
    }
}
