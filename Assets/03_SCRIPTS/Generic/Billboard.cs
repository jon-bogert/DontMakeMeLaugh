using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] bool _doLookVertical = true;

    private void Start()
    {
        //Get Camera if not assigned
        if (_camera == null)
            _camera = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 destination = (_doLookVertical)?
            _camera.position :
            new Vector3(
                _camera.position.x,
                transform.position.y,
                _camera.position.z);

        transform.LookAt(destination);
    }
}
