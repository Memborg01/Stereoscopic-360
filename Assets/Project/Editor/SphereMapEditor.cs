﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(SphereMapProperties))]
public class SphereMapEditor : Editor {

    public string importFolder = "No Path Set";
    string destFolder, smSysName, resourceFolder, resSubFolder;

    int imageAmount;

    Material[] leftMatAssets, rightMatAssets;


    GameObject leftParent, rightParent;


    public override void OnInspectorGUI()
    {
        SphereMapProperties sphereMapProp = (SphereMapProperties)target;
        destFolder = sphereMapProp.sphereMapImageFolder;

        sphereMapProp.leftSphereMap = sphereMapProp.gameObject.transform.GetChild(0).gameObject;
        sphereMapProp.rightSphereMap = sphereMapProp.gameObject.transform.GetChild(1).gameObject;

        leftParent = sphereMapProp.leftSphereMap;
        rightParent = sphereMapProp.rightSphereMap;

        smSysName = sphereMapProp.spheremapName;

        resourceFolder = destFolder + "/Resources/"+smSysName+"_imgs/";
        resSubFolder = smSysName + "_imgs/";

        EditorGUILayout.LabelField("Sphere Map Name", sphereMapProp.spheremapName);
        EditorGUILayout.IntField("Images Per Eye", sphereMapProp.imagesPerEye);
        EditorGUILayout.LabelField("Project Image Path", sphereMapProp.sphereMapImageFolder);

        EditorGUILayout.LabelField("Resource Path", resourceFolder);

        EditorGUILayout.LabelField("Left GameObject", sphereMapProp.leftSphereMap.name);
        EditorGUILayout.LabelField("Right GameObject", sphereMapProp.rightSphereMap.name);

        imageAmount = sphereMapProp.imagesPerEye;



        leftMatAssets = new Material[imageAmount];
        rightMatAssets = new Material[imageAmount];

        if (GUILayout.Button("Import Sphere Map Images"))
        {
            importFolder = EditorUtility.OpenFolderPanel("Load Sphere Map Textures", "", "");
            LoadSphereMaps();
            CreateMaterials();
            AssetDatabase.Refresh();

        }

        if(GUILayout.Button("Assign Materials"))
        {

            LoadMaterialAssets();
            AssignMaterials();
            AssignTextures();

        }

        EditorGUILayout.LabelField("System Image Path", importFolder);
        

    }

    void LoadSphereMaps()
    {

        string[] imgFiles = Directory.GetFiles( importFolder );

        int smInt = 0;

        for (int i = 0; i < imgFiles.Length; i++)
        {

            //Debug.Log("imageAmount = " + imageAmount);
            
            string sphereImg = imgFiles[i];

            //Debug.Log("i = " + i);
           // Debug.Log("imgFiles.Length = " + imgFiles.Length);

            if (sphereImg.EndsWith(".png") || sphereImg.EndsWith(".jpg") || sphereImg.EndsWith(".JPG") || sphereImg.EndsWith(".PNG"))
            {
                File.Copy(sphereImg, resourceFolder + "sphereMap_" + smInt + ".jpg");
                
                

                //Keep at bottom
                smInt++;
            }

            //Debug.Log("DestFolder = " + destFolder);

        }

    }

    void CreateMaterials()
    {

        Material[] leftMats;
        Material[] rightMats;

        leftMats = new Material[imageAmount];
        rightMats = new Material[imageAmount];

        string leftFolder = destFolder + "/LeftImages/";
        string rightFolder = destFolder + "/RightImages/";

        for (int i = 0; i < imageAmount; i++)
        {

            leftMats[i] = new Material(Shader.Find("InsideVisibleTrans"));
            rightMats[i] = new Material(Shader.Find("InsideVisibleTrans"));

            string leftFileName = leftFolder + "Left" + i + ".mat";
            string rightFileName = rightFolder + "Right" + i + ".mat";

            if (!File.Exists(leftFileName))
            {
                AssetDatabase.CreateAsset(leftMats[i], leftFileName);
            }
            if (!File.Exists(rightFileName))
            {
                AssetDatabase.CreateAsset(rightMats[i], rightFileName);
            }
            
        }

    }

    void LoadMaterialAssets()
    {

        string leftFolder = destFolder + "/LeftImages/";
        string rightFolder = destFolder + "/RightImages/";


            
        for (int i = 0; i< imageAmount; i++)
        {

            string leftFileName = leftFolder + "Left" + i + ".mat";
            string rightFileName = rightFolder + "Right" + i + ".mat";

            leftMatAssets[i] = AssetDatabase.LoadAssetAtPath(leftFileName, typeof(Material)) as Material;
            rightMatAssets[i] = AssetDatabase.LoadAssetAtPath(rightFileName, typeof(Material)) as Material;

        }

    }

    void AssignMaterials()
    {

        int rightImgCorrector = imageAmount / 2;

        for (int i = 0; i < imageAmount; i++)
        {

            //Debug.Log("i = " + i);

            if (rightImgCorrector == imageAmount - 1)
            {
                rightImgCorrector = 0;
            }
            else if (i < imageAmount / 2)
            {
                rightImgCorrector = (imageAmount / 2) + i;
            }
            else
            {
                rightImgCorrector++;
            }

            //Debug.Log("Right Corrector = " + rightImgCorrector);

            GameObject tmpLeftMap = leftParent.transform.GetChild(i).gameObject;
            GameObject tmpRightMap = rightParent.transform.GetChild(i).gameObject;

            Renderer leftRend = tmpLeftMap.gameObject.GetComponent<Renderer>();
            Renderer rightRend = tmpRightMap.gameObject.GetComponent<Renderer>();

            leftRend.material = leftMatAssets[i];
            rightRend.material = rightMatAssets[i];

            Debug.Log("Resource Sub Folder  =  " + resSubFolder);
            Debug.Log("Left img = " + resSubFolder + "sphereMap_" + i);
            Debug.Log("Right img = " + resSubFolder + "sphereMap_" + rightImgCorrector);

        }

    }

    void AssignTextures()
    {

        int rightImgCorrector = imageAmount / 2;

        for(int i = 0; i < imageAmount; i++)
        {

            if (rightImgCorrector == imageAmount - 1)
            {
                rightImgCorrector = 0;
            }
            else if (i < imageAmount / 2)
            {
                rightImgCorrector = (imageAmount / 2) + i;
            }
            else
            {
                rightImgCorrector++;
            }


            GameObject tmpLeftMap = leftParent.transform.GetChild(i).gameObject;
            GameObject tmpRightMap = rightParent.transform.GetChild(i).gameObject;

            Renderer leftRend = tmpLeftMap.GetComponent<Renderer>();
            Renderer rightRend = tmpRightMap.GetComponent<Renderer>();


            Texture tmpLeftImg = Resources.Load(resSubFolder + "sphereMap_" + i) as Texture;
            Texture tmpRightImg = Resources.Load(resSubFolder + "sphereMap_" + rightImgCorrector) as Texture;

            leftRend.sharedMaterial.mainTexture = tmpLeftImg;
            rightRend.sharedMaterial.mainTexture = tmpRightImg;

        }

    }



}
