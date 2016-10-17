using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneSetup
{

    public int stereoImgAmount = 20;
    int goPerEye;

    
    static void Create()
    {
        for (int x = 0; x != 10; x++)
        {
            GameObject go = new GameObject("MyCreatedGO" + x);
            go.transform.position = new Vector3(x, 0, 0);
        }
    }





} 