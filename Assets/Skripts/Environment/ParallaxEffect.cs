using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera mainCamera;
    public Transform followTarget;

    Vector2 startingPosition;

    float strtingZ;

    Vector2 cameraMoveSinceStart => (Vector2)mainCamera.transform.position - startingPosition;
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
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
