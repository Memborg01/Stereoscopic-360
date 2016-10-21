using UnityEngine;
using System.Collections;
using UnityEditor;

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

        SphereMapProperties properties = SphereMapSystem.AddComponent<SphereMapProperties>();

        properties.spheremapName = sphereMapName;


        SphereMapSystem.transform.position = origin;
        LeftSmSystem.transform.position = origin;
        RightSmSystem.transform.position = origin;

        LeftSmSystem.transform.SetParent(SphereMapSystem.transform);
        RightSmSystem.transform.SetParent(SphereMapSystem.transform);

        SphereMapSystem.name = sphereMapName;
        LeftSmSystem.name = "Left Sphere Map";
        RightSmSystem.name = "Right Sphere Map";

        for(int i = 1; i <= imgAmount/2; i++)
        {
            GameObject LeftSphereMap = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject RightSphereMap = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            LeftSphereMap.name = "left_" + i;
            RightSphereMap.name = "right_" + i;

            LeftSphereMap.transform.position = origin;
            RightSphereMap.transform.position = origin;



            LeftSphereMap.transform.SetParent(LeftSmSystem.transform);
            RightSphereMap.transform.SetParent(RightSmSystem.transform);

        }

        SphereMapSystem.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

    }

    


}
