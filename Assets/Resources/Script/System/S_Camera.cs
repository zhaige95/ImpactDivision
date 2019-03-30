using UnityEngine;
using Unity.Entities;

public class S_Camera : ComponentSystem {

    struct Group
    {
        public C_Camera _Camera;
        public C_Velocity _Velocity;
    }

    public RaycastHit hit;
    bool cursorIsLocked = true;
    
    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<Group>())
        {
            var _camera = e._Camera;
            var _velocity = e._Velocity;
            
            if (_camera.m_cursorIsLocked)
            {
                float x = 0f;
                float y = 0f;

                if (_velocity.aiming)
                {
                    x = _camera.camera_x.localEulerAngles.x - _velocity.Dmouse_y * _camera.c_speed_x * _camera.aimRate;
                    y = _camera.camera_y.localEulerAngles.y + _velocity.Dmouse_x * _camera.c_speed_y * _camera.aimRate;
                }
                else
                {
                    x = _camera.camera_x.localEulerAngles.x - _velocity.Dmouse_y * _camera.c_speed_x ;
                    y = _camera.camera_y.localEulerAngles.y + _velocity.Dmouse_x * _camera.c_speed_y ;
                }

                if (x >= 260f && x <= 360f)
                {
                    if (x < 270f)
                    {
                        x = 270f;
                    }
                }
                else
                {
                    if (x > 70f && x <= 100f)
                    {
                        x = 70f;
                    }
                }

                _camera.camera_x.localEulerAngles = new Vector3(x, 0, 0);
                _camera.camera_y.localEulerAngles = new Vector3(0, y, 0);
            }
            

            if (_camera.forceX != 0)
            {
                _camera.camera_x.Rotate(-Time.deltaTime * _camera.forceX, 0, 0);
                _camera.forceX -= Time.deltaTime * 100f;

                if (_camera.forceX < 0.001f)
                {
                    _camera.forceX = 0f;
                }
            }
            if (_camera.forceY != 0)
            {
                _camera.camera_y.Rotate(0, Time.deltaTime * _camera.forceY, 0);
                _camera.forceY -= Time.deltaTime * 100f;
                if (_camera.forceY < 0.001f)
                {
                    _camera.forceY = 0f;
                }
            }

            // local player logic
            if (_velocity.isLocalPlayer)
            {
                _camera.mainCamera.fieldOfView = Mathf.Lerp(_camera.mainCamera.fieldOfView, _camera.FOVtarget, 10f * Time.deltaTime);
                PhysicalProcess(e);
                //_camera.m_cursorIsLocked = this.InternalLockUpdate();

                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    _camera.m_cursorIsLocked = !_camera.m_cursorIsLocked;
                }

                if (_camera.m_cursorIsLocked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else if (!_camera.m_cursorIsLocked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

            }

            if (_velocity.DcutCameraSide)
            {
                _camera.sideOffset *= -1f; 
                _camera.targetSideOffset1 = new Vector3(_camera.targetSideOffset1.x * -1f, _camera.targetSideOffset1.y, _camera.targetSideOffset1.z);
                _camera.targetSideOffset2 = new Vector3(_camera.targetSideOffset2.x * -1f, _camera.targetSideOffset2.y, _camera.targetSideOffset2.z);
            }

            _camera.camera_x.localPosition = Vector3.Lerp(_camera.camera_x.localPosition, _camera.targetSideOffset1, 10f * Time.deltaTime);
            _camera.cameraHandle.localPosition = Vector3.Lerp(_camera.cameraHandle.localPosition, _camera.targetSideOffset2, 10f * Time.deltaTime);
        }
    }

    private bool InternalLockUpdate()
    {   
        
        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    cursorIsLocked = !cursorIsLocked;
        //}

        //if (cursorIsLocked)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        //else if (!cursorIsLocked)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        return cursorIsLocked;
    }

    void PhysicalProcess(Group e)
    {
        
        var _camera = e._Camera;
        RaycastHit hitInfo;
        Vector3 from = _camera.camera_x.position;
        var _to = _camera.planePoints;
        
        _camera.NearClipPlanePoints();
        var rayDistance = Vector3.Distance(_camera.cameraHandle.position, from) + 0.1f;

        float distance = rayDistance;
        bool hit = false;

        //Debug.DrawRay(from, _to.LowerLeft - from, Color.red);
        //Debug.DrawLine(_to.LowerLeft, _to.LowerRight, Color.red);
        //Debug.DrawLine(_to.UpperLeft, _to.UpperRight, Color.red);
        //Debug.DrawLine(_to.UpperLeft, _to.LowerLeft, Color.red);
        //Debug.DrawLine(_to.UpperRight, _to.LowerRight, Color.red);
        //Debug.DrawRay(from, _to.LowerRight - from, Color.red);
        //Debug.DrawRay(from, _to.UpperLeft - from, Color.red);
        //Debug.DrawRay(from, _to.UpperRight - from, Color.red);

        if (Physics.Raycast(from, _to.LowerLeft - from, out hitInfo, rayDistance, _camera.coverLayerMask))
        {
            hit = true;
            if (distance > hitInfo.distance) distance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.LowerRight - from, out hitInfo, rayDistance, _camera.coverLayerMask))
        {
            hit = true;
            if (distance > hitInfo.distance) distance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperLeft - from, out hitInfo, rayDistance, _camera.coverLayerMask))
        {
            hit = true;
            if (distance > hitInfo.distance) distance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperRight - from, out hitInfo, rayDistance, _camera.coverLayerMask))
        {
            hit = true;
            if (distance > hitInfo.distance) distance = hitInfo.distance;
        }

        if (hit)
        {
            _camera.cameraObj.position = from + (_camera.cameraHandle.position - from).normalized * (distance - 0.2f);
        }
        else
        {
            _camera.cameraObj.localPosition = Vector3.zero;
        }

    }

}
