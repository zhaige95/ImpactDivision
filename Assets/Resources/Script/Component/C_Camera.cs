using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Camera : MonoBehaviour {
    public Transform Carryer;
    public Camera mainCamera;
    public Transform camera_y;
    public Transform camera_x;
    public Transform aimPoint;
    public Transform hitPoint;
    public Transform cameraHandle;
    public Transform cameraObj;
    public float c_speed_x = 5f;
    public float c_speed_y = 5f;
    public float aimRate = 0.4f;
    public float hight;
    public float FOVdefault;
    public float FOVtarget;
    public float radius;
    public bool m_cursorIsLocked = true;
    public LayerMask shootLayerMask;
    public LayerMask coverLayerMask;
    public ClipPlanePoints planePoints;
    public float forceX = 0;
    public float forceY = 0;
    public float backForce = 3;
    RaycastHit hitInfo;
    public float sideOffset = 0.25f;
    public bool sideSwitch = false;
    public Vector3 targetSideOffset1;
    public Vector3 targetSideOffset2;

    public struct ClipPlanePoints
    {
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerLeft;
        public Vector3 LowerRight;
    }

    private void Start()
    {
        Carryer.parent = null;
        targetSideOffset1 = camera_x.localPosition;
        targetSideOffset2 = cameraHandle.localPosition;

        //var cameraController = GameObject.Instantiate(this.Carryer);
        //mainCamera = cameraController.GetComponentInChildren<Camera>();
        //camera_y = cameraController.transform;
        //camera_x = camera_y.GetChild(1);
        //aimPoint = camera_y.GetChild(3);

    }

    public RaycastHit GetAimInfo()
    {
        GetAimPoint(0, 0);
        return hitInfo;
    }

    public Vector3 GetAimPoint()
    {
        return GetAimPoint(0, 0);
    }
    public Vector3 GetAimPoint(Vector2 offsets)
    {
        return GetAimPoint(offsets.x, offsets.y);
    }

    public Vector3 GetAimPoint(float offsetX, float offsetY)
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2 + offsetX, Screen.height / 2 + offsetY, 0)), out hitInfo, 500f, shootLayerMask))
        {
            hitPoint.transform.position = hitInfo.point;
            return hitInfo.point;
        }
        return aimPoint.position;
    }

    public void UpdateClipPlanePoints()
    {
        planePoints.UpperLeft = cameraHandle.position + new Vector3(-radius, radius, 0);
        planePoints.UpperRight = cameraHandle.position + new Vector3(radius, radius, 0);
        planePoints.LowerLeft = cameraHandle.position + new Vector3(-radius, -radius, 0);
        planePoints.LowerRight = cameraHandle.position + new Vector3(radius, -radius, 0);
    }
    public void NearClipPlanePoints()
    {
        var pos = cameraHandle.position;
         
        var transform = mainCamera.transform;
        var halfFOV = (mainCamera.fieldOfView / 2) * Mathf.Deg2Rad;
        var aspect = mainCamera.aspect;
        var distance = mainCamera.nearClipPlane;
        var height = distance * Mathf.Tan(halfFOV);
        var width = height * aspect;
        height *= 5f;
        width *= 5f;
        planePoints.LowerRight = pos + transform.right * width;
        planePoints.LowerRight -= transform.up * height;
        planePoints.LowerRight += transform.forward * distance;

        planePoints.LowerLeft = pos - transform.right * width;
        planePoints.LowerLeft -= transform.up * height;
        planePoints.LowerLeft += transform.forward * distance;

        planePoints.UpperRight = pos + transform.right * width;
        planePoints.UpperRight += transform.up * height;
        planePoints.UpperRight += transform.forward * distance;

        planePoints.UpperLeft = pos - transform.right * width;
        planePoints.UpperLeft += transform.up * height;
        planePoints.UpperLeft += transform.forward * distance;
        
    }
}
