using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 0.1f;

    private void Update()
    {
        transform.Translate(Vector2.up * _scrollSpeed * Time.deltaTime);
    }
}
