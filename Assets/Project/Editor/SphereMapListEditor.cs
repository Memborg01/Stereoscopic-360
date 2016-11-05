using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SphereMapList))]
public class SphereMapListEditor : Editor {

    GameObject[] tmpSM;

    public override void OnInspectorGUI()
    {

        SphereMapList sMapList = (SphereMapList)target;
        int amount = sMapList.sphereMaps.Length;



        EditorGUILayout.IntField("Spheremaps in system", amount);

        DrawDefaultInspector();

        if(GUILayout.Button("Add SphereMap"))
        {

            getSphereMaps(sMapList.sphereMaps, amount);
            amount++;
            sMapList.sphereMaps = new GameObject[amount];
            setSpheres(sMapList.sphereMaps);

        }

        if(GUILayout.Button("Remove SphereMap"))
        {

            if(amount > 2)
            {

                amount--;
                getSphereMaps(sMapList.sphereMaps, amount);
                sMapList.sphereMaps = new GameObject[amount];
                setSpheres(sMapList.sphereMaps);
            }

        }

    }

    void getSphereMaps(GameObject[] spheres, int sphereAmount)
    {

        tmpSM = new GameObject[sphereAmount];

        for(int i = 0; i < tmpSM.Length; i++)
        {
            tmpSM[i] = spheres[i];
        }

    }

    void setSpheres(GameObject[] spheres)
    {

        for(int i = 0; i < tmpSM.Length; i++)
        {

            spheres[i] = tmpSM[i];

        }

    }



}
