using UnityEngine;
using System.Collections;

public class SphereMapProperties : MonoBehaviour {

    public string spheremapName;
    public int imagesPerEye;
    public string sphereMapImageFolder;

    public Material[] leftMaterials;
    public Material[] rightMaterials;

    public GameObject leftSphereMap, rightSphereMap;
    public GameObject[] leftMaps, rightMaps;

}
