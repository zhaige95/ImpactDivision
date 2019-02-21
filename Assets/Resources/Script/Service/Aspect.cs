using UnityEngine;
using System.Collections;
using Unity.Entities;

public class Aspect{


    public static bool IsAvatarTags(string tag) {
        return AvatarTagsData.tags.Contains(tag); ;
    }
    public static void RotateToCameraY(Transform cameraTransform, Transform bodyTransform, float range = -1f)
    {
        var targetY = cameraTransform.localEulerAngles.y ;
        var _transform = bodyTransform;

        if (range == -1)
        {
            _transform.rotation = Quaternion.Euler(new Vector3(0, targetY, 0));
        }
        else
        {
            var targetRotation = Quaternion.Euler(new Vector3(0, targetY, 0));
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, range);
        }

    }
    public static void RotateToCameraY(GameObject gameObject, float range = -1f)
    {
        var targetY = gameObject.GetComponent<C_Camera>().Carryer.localEulerAngles.y;
        var _transform = gameObject.transform;

        if (range == -1)
        {
            _transform.rotation = Quaternion.Euler(new Vector3(0, targetY, 0));
        }
        else
        {
            var targetRotation = Quaternion.Euler(new Vector3(0, targetY, 0));
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, range);
        }

    }
    public static void SetCamp(GameObject obj, int camp)
    {
        int layerIndex = 15;
        switch (camp)
        {
            case 1: layerIndex = 15; break;
            case 2: layerIndex = 16; break;
            case 3: layerIndex = 17; break;
        }
        foreach (Transform item in obj.GetComponentsInChildren<Transform>())
        {
            item.gameObject.layer = layerIndex;
        }
        obj.layer = 13;
    }

    public static void SetFreeDirection(Transform transform, C_Velocity _velocity, C_Camera _camera)
    {
        // follow the camera when movecharacter
        var directionIndex = 0;
        if (_velocity.Dfwd)
        {

            directionIndex = 1;
            if (_velocity.Dleft)
            {
                directionIndex = 8;
            }
            if (_velocity.Dright)
            {
                directionIndex = 2;
            }
        }
        if (_velocity.Dbwd)
        {
            directionIndex = 5;
            if (_velocity.Dleft)
            {
                directionIndex = 6;
            }
            if (_velocity.Dright)
            {
                directionIndex = 4;
            }
        }
        if (_velocity.Dleft)
        {
            directionIndex = 7;
            if (_velocity.Dfwd)
            {
                directionIndex = 8;
            }
            if (_velocity.Dbwd)
            {
                directionIndex = 6;
            }
        }
        if (_velocity.Dright)
        {
            directionIndex = 3;
            if (_velocity.Dfwd)
            {
                directionIndex = 2;
            }
            if (_velocity.Dbwd)
            {
                directionIndex = 4;
            }
        }


        if (directionIndex != 0)
        {
            Vector3 targetAngles = new Vector3();
            switch (directionIndex)
            {
                case 1:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y, 0);
                    break;
                case 2:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 45, 0);
                    break;
                case 3:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 90, 0);
                    break;
                case 4:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 135, 0);
                    break;
                case 5:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 180, 0);
                    break;
                case 6:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + -135, 0);
                    break;
                case 7:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y - 90, 0);
                    break;
                case 8:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y - 45, 0);
                    break;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetAngles), Time.deltaTime * 10);
        }
    }

}
