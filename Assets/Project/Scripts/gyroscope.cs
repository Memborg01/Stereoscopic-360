#if UNITY_EDITOR

using UnityEngine;
using System.Collections;

public class gyroscope : MonoBehaviour
{
    private Gyroscope gyro;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }
}

#endif