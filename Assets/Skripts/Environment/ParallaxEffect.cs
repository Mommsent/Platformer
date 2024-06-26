using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private Transform targetPos;
 
    Vector2 startingPosition;

    float strtingZ;

    Vector2 cameraMoveSinceStart => (Vector2)mainCamera.transform.position - startingPosition;
    float zDistanceFromTarget => transform.position.z - targetPos.transform.position.z;
    float clippingPlane => (mainCamera.transform.position.z + (zDistanceFromTarget > 0 ? mainCamera.farClipPlane : mainCamera.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        strtingZ = transform.position.z;
    }


    void Update()
    {
        Vector2 newPos = startingPosition + cameraMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, strtingZ);
    }
}
