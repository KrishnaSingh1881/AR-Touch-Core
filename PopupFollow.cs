using System.Collections;
using UnityEngine;

public class PopupFollow : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 offset;
    private float smoothSpeed = 4f;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        offset = transform.position - cameraTransform.position;
        StartCoroutine(FollowCoroutine());
    }

    private IEnumerator FollowCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = cameraTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(cameraTransform);
            yield return null;
        }
    }
}