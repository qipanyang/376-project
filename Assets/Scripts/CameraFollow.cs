using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> _getCameraFollowPosition;
    public float left;
    public float right;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = _getCameraFollowPosition();
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
        
        // set camera boundary
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left, right), transform.position.y, transform.position.z);
    }

    public void Setup(Func<Vector3> getCameraFollowPosition)
    {
        _getCameraFollowPosition = getCameraFollowPosition;
    }
}
