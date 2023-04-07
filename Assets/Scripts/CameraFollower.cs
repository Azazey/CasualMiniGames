using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpChange;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x,transform.position.y,_target.position.z - 5f), Time.deltaTime * _lerpChange);
    }
}
