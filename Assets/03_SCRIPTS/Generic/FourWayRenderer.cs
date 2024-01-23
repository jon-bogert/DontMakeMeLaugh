using UnityEngine;
using XephTools;

public class FourWayRenderer : MonoBehaviour
{
    public enum FacingDirection { Front, Right, Back, Left }

    [SerializeField] bool _billboardVertical = false;
    [SerializeField] Transform _camera;


    Material _material;
    Transform _renderer;

    FacingDirection _facingDirection = FacingDirection.Front;

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        _material = meshRenderer.material;

        //Move visual components to new gameobject
        GameObject child = Instantiate(new GameObject(), transform);
        child.name = name + "_VISUAL";
        child.AddComponent<MeshFilter>().mesh = meshFilter.mesh;
        Destroy(meshFilter);
        MeshRenderer mr = child.AddComponent<MeshRenderer>();
        mr.material = _material;
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Destroy(meshRenderer);
        child.AddComponent<Billboard>().doLookVertical = _billboardVertical;

        _renderer = child.transform;
    }

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 cameraDirection = (_camera.position - transform.position).normalized;

        float angle = Vector3.SignedAngle(transform.forward, cameraDirection, Vector3.up);
        FacingDirection newDirection = FacingDirection.Front;

        if (angle > -45 && angle <= 45)
            newDirection = FacingDirection.Front;
        else if (angle > 45 && angle <= 135)
            newDirection = FacingDirection.Right;
        else if ((angle > 135 && angle <= 180) || (angle >= -180 && angle < -135))
            newDirection = FacingDirection.Back;
        else // (angle >= -135 && angle < -45)
            newDirection = FacingDirection.Left;

        DebugMonitor.UpdateValue("Angle", angle);
        DebugMonitor.UpdateValue("Direction", newDirection);

        if (newDirection == _facingDirection)
            return;

        _facingDirection = newDirection;
        _material.SetFloat("_Selector", (float)_facingDirection);
    }
}
