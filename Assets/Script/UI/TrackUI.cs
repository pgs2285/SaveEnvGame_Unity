using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{

    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform Subject;
    [SerializeField] private Vector3 Offset;
 
    void Update()
    {
        transform.position = PlayerCamera.WorldToScreenPoint(Subject.position) + Offset;
    }
}
