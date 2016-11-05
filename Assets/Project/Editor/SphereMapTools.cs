using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class SphereMapTools : ScriptableWizard {


    public string sphereMapName = "Default SphereMap";
    public int imgAmount = 20;


    [MenuItem ("Stereo SphereMap Tools/Create New Stereo SphereMap System")]
    static void SphereMapWizard()
    {

        ScriptableWizard.DisplayWizard<SphereMapTools>("Create Stereo SphereMaps", "Create new", "Update selected");

    }

    void OnWizardCreate()
    {
        
        

        GameObject SphereMapSystem = new GameObject();
        GameObject LeftSmSystem = new GameObject();
        GameObject RightSmSystem = new GameObject();
        Vector3 origin = new Vector3(0, 0, 0);

        string tempFolderPath = "Assets/SphereMaps/" + sphereMapName;

        SphereMapProperties properties = SphereMapSystem.AddComponent<SphereMapProperties>();
        SphereFader sphereFader = SphereMapSystem.AddComponent<SphereFader>();

        properties.spheremapName = sphereMapName;
        properties.imagesPerEye = imgAmount;

        if (!AssetDatabase.IsValidFolder("Assets/SphereMaps"))
        {

            string sourceFolder = AssetDatabase.CreateFolder("Assets", "SphereMaps");
            string sourceFolderPath = AssetDatabase.GUIDToAssetPath(sourceFolder);
            

        }


        /*if (AssetDatabase.IsValidFolder("Assets/SphereMaps/" + sphereMapName))
        {
            tempFolderPath = "Assets/SphereMaps" + sphereMapName;
        }*/
        if (!AssetDatabase.IsValidFolder("Assets/SphereMaps/"+sphereMapName))
        {
            string imgMatFolder = AssetDatabase.CreateFolder("Assets/SphereMaps", sphereMapName);
            string newFolderPath = AssetDatabase.GUIDToAssetPath(imgMatFolder);
            tempFolderPath = newFolderPath;
        }

    
        properties.sphereMapImageFolder = tempFolderPath;

        if (!AssetDatabase.IsValidFolder(tempFolderPath + "/LeftImages"))
        {
            string leftImgFolder = AssetDatabase.CreateFolder(tempFolderPath, "LeftImages");
            string leftImgFolderPath = AssetDatabase.GUIDToAssetPath(leftImgFolder);
        }
        if (!AssetDatabase.IsValidFolder(tempFolderPath + "/RightImages"))
        {
            string rightImgFolder = AssetDatabase.CreateFolder(tempFolderPath, "RightImages");
            string rightImgFolderPath = AssetDatabase.GUIDToAssetPath(rightImgFolder);
        }
        if(!AssetDatabase.IsValidFolder(tempFolderPath + "/Resources"))
        {
            string resourceFolder = AssetDatabase.CreateFolder(tempFolderPath, "Resources");
            string resourceFolderPath = AssetDatabase.GUIDToAssetPath(resourceFolder);
        }
        if (!AssetDatabase.IsValidFolder(tempFolderPath + "/Resources/" + sphereMapName + "_imgs"))
        {
            string resSubFolder = AssetDatabase.CreateFolder(tempFolderPath + "/Resources", sphereMapName + "_imgs");
            string resSubFolderPath = AssetDatabase.GUIDToAssetPath(resSubFolder);
        }

        SphereMapSystem.transform.position = origin;
        LeftSmSystem.transform.position = origin;
        RightSmSystem.transform.position = origin;

        LeftSmSystem.transform.SetParent(SphereMapSystem.transform);
        RightSmSystem.transform.SetParent(SphereMapSystem.transform);

        SphereMapSystem.name = sphereMapName;
        SphereMapSystem.gameObject.tag = "mainSphere";
        LeftSmSystem.name = "Left Sphere Map";
        RightSmSystem.name = "Right Sphere Map";

        for(int i = 1; i <= imgAmount; i++)
        {
            GameObject LeftSphereMap = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject RightSphereMap = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            LeftSphereMap.name = "left_" + i;
            RightSphereMap.name = "right_" + i;

            LeftSphereMap.transform.position = origin;
            RightSphereMap.transform.position = origin;



            LeftSphereMap.transform.SetParent(LeftSmSystem.transform);
            RightSphereMap.transform.SetParent(RightSmSystem.transform);

            LeftSphereMap.gameObject.layer = 8;
            RightSphereMap.gameObject.layer = 9;

        }

        SphereMapSystem.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

    }


}
