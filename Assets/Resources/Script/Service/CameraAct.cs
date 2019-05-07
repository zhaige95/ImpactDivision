using UnityEngine;
using System.Collections;
using Unity.Entities;
using DG.Tweening;

public class CameraAct : MonoBehaviour
{

    //public static void ShakePosition(float duration, Vector3 strength, int vibrato = 10, float randomness = 90, bool fadeOut = true)
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera).GetComponent<Camera>();
    //    _camera.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    //}


    //public static void ShakePosition(float duration, float strength, int vibrato = 10, float randomness = 90, bool fadeOut = true)
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera).GetComponent<Camera>();

    //    _camera.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    //}

    //public static void ShakeRotation(float duration, Vector3 strength, int vibrato = 10, float randomness = 90, bool fadeOut = true)
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera).GetComponent<Camera>();

    //    _camera.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut);
    //}

    //public static void ShakeRotation(float duration, float strength, int vibrato = 10, float randomness = 90, bool fadeOut = true)
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera).GetComponent<Camera>();

    //    _camera.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut);
    //}

    //public static Vector3 GetAimPoint()
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera);
    //    var _cameraTransform = _camera.GetComponent<Camera>().gameObject.transform;
    //    RaycastHit hit;
    //    if (Physics.Raycast(_camera.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f)), out hit, 500f, _camera.layerMask))
    //    {
    //        return hit.point;
    //    }
    //    return _camera.aimPoint.position;
    //}

    //public static void FixRotation()
    //{
    //    var _camera = Tools._entityManager.GetComponentObject<C_Camera>(Config._camera);
    //    _camera.GetComponent<Camera>().gameObject.transform.localEulerAngles = Vector3.zero;

    //}
}
