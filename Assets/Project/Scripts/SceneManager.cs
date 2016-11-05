using UnityEngine;
using System.Collections;



public class SceneManager : MonoBehaviour {

    public GameObject LeftCamObj;
    public GameObject RightCamObj;

    public Camera LeftCam;
    public Camera CamRight;

    bool layerSet = false;

	// Use this for initialization
	void Start () {

        LeftCamObj = GameObject.Find("Main Camera Left");
        RightCamObj = GameObject.Find("Main Camera Right");

        LeftCam = LeftCamObj.GetComponent<Camera>();
        CamRight = RightCamObj.GetComponent<Camera>();

        LeftCam.cullingMask = 1 << 8;
        CamRight.cullingMask = 1 << 9;


    }
	
	// Update is called once per frame
	void Update () {

        if(layerSet == false)
        {
            StereoLayer();
        }
       


    }

    void StereoLayer()
    {

        if (LeftCam.cullingMask != 256)
        {

            LeftCam.cullingMask = 1 << 8;
            //Debug.Log("Left Mask changed");
            Debug.Log("Left Cull = " + LeftCam.cullingMask);

        }
        if (CamRight.cullingMask != 512)
        {

            CamRight.cullingMask = 1 << 9;
            //Debug.Log("Right Mask changed");
            Debug.Log("Right Cull = " + CamRight.cullingMask);

        }

        layerSet = true;

    }

}
