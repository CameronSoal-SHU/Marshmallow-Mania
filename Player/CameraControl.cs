using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { private set; get; } // Singleton instance

    private Bounds camBounds; // <- change when room changes

    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float followSpeed = 10f;
    [SerializeField]
    private float heightOffsetForBounds = 0.0f;

    private Vector3 offset;
    private Camera cam;

    [Header("Camera Shake")]
    [SerializeField] float shakeDecrease = 1;
    [SerializeField] float maxShakeAngle = 10;
    [SerializeField] float maxShakeOffset = 0.5f;
    private float shakiness = 0.0f;
    private Vector3 unshakenPos;
    private Vector3 startingEulerAngles;

    private void Awake()
    {
        // Set up Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Multiple instances of CameraControl!");
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        offset = target.position - transform.position;
        unshakenPos = transform.position;
        startingEulerAngles = transform.eulerAngles;
    }

    void LateUpdate()
    {
        transform.position = unshakenPos;

        Vector3 targetPos = target.position - offset;
        targetPos.x = Mathf.Clamp(targetPos.x, camBounds.min.x, camBounds.max.x);
        targetPos.y = transform.position.y;
        targetPos.z = Mathf.Clamp(targetPos.z, camBounds.min.z, camBounds.max.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

        ShakeCamera();
    }

    public void CalculateBounds(Bounds boundsInWorldSpace)
    {
        float frustumHeight = 2.0f * (transform.position.y - boundsInWorldSpace.center.y - heightOffsetForBounds) * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        Vector3 boundsSize = Vector3.zero;
        boundsSize.x = boundsInWorldSpace.size.x - frustumWidth;
        if (boundsSize.x < 0) boundsSize.x = 0;
        boundsSize.z = boundsInWorldSpace.size.z - frustumHeight;
        if (boundsSize.z < 0) boundsSize.z = 0;

        //Debug.Log(boundsInWorldSpace.Intersects(camBounds));

        camBounds = new Bounds(boundsInWorldSpace.center, boundsSize);
    }

    void ShakeCamera()
    {
        unshakenPos = transform.position;
        float shake = shakiness * shakiness;
        shakiness = Mathf.Clamp(shakiness - Time.deltaTime * shakeDecrease, 0, 1);
        
        float offsetX = maxShakeOffset * shake * Random.Range(-1, 1);
        float offsetY = maxShakeOffset * shake * Random.Range(-1, 1);
        transform.position = transform.position + new Vector3(offsetX, 0, offsetY);

        float angle = maxShakeAngle * shake * Random.Range(-1, 1);
        transform.eulerAngles = startingEulerAngles + new Vector3(0, angle, 0);
    }

    /// <param name="shake">Value between 0 and 1.</param>
    public void AddShake(float shake = 0.5f)
    {
        shakiness = Mathf.Clamp(shakiness + shake, 0, 1);
    }
}
