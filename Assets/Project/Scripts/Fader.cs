﻿using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{

    public float CurrentDegree;

    public int NumberOfImages;


    // New Variables for Dynamic Setup

    GameObject mainSpheremap, leftSpheremap, rightSpheremap;

    public GameObject[] leftSphereMaps, rightSphereMaps;

    public Renderer[] leftSphereMapRenderer, rightSphereMapRenderer;
    float[] trans;

    

    //public float transCorrector = 1.3f;

    //bool stereoShifted = false;


    void Start()
    {

        // Debug.Log("Start Function Run");

        GetSphereMapObjects();

        GetRenderer();

       // Debug.Log("Start Function Ended");


    }
    void Update()
    {

        CurrentDegree = this.transform.eulerAngles.y;
        //Debug.Log("In Update");
        /*if (CurrentDegree > 360) { CurrentDegree = CurrentDegree - 360; }
        if (CurrentDegree < 0) { CurrentDegree = CurrentDegree + 360; }*/

        

        transHandler();

        debugArray();

        SetSpheremapTransparency();

       // SetRenderer();


    }

    void GetSphereMapObjects()
    {

        


        mainSpheremap = GameObject.FindGameObjectWithTag("mainSphere");

        leftSpheremap = mainSpheremap.transform.GetChild(0).gameObject;
        rightSpheremap = mainSpheremap.transform.GetChild(1).gameObject;

        int childInSpheremap = leftSpheremap.transform.childCount;

        leftSphereMaps = new GameObject[NumberOfImages];
        rightSphereMaps = new GameObject[NumberOfImages];

        for (int k = 0; k < childInSpheremap; k++)
        {

            leftSphereMaps[k] = leftSpheremap.transform.GetChild(k).gameObject;
            rightSphereMaps[k] = rightSpheremap.transform.GetChild(k).gameObject;

        }

        // Debug.Log("GameObjects initialized and assigned");


        NumberOfImages = leftSphereMaps.Length;

    }

    void GetRenderer()
    {

        //Debug.Log("GetRender is Run");

        

        Debug.Log("Left sm = " + leftSphereMaps.Length);
        Debug.Log("Number of Images = " + NumberOfImages);

        leftSphereMapRenderer = new Renderer[NumberOfImages];
        rightSphereMapRenderer = new Renderer[NumberOfImages];

        

        trans = new float[NumberOfImages];
      
        for(int i = 0; i < NumberOfImages; i++)
        {

            leftSphereMapRenderer[i] = leftSpheremap.transform.GetChild(i).transform.GetComponent<Renderer>();
            rightSphereMapRenderer[i] = rightSpheremap.transform.GetChild(i).transform.GetComponent<Renderer>();

        }

        //Debug.Log("for-loop ended");



    }


    void SetSpheremapTransparency()
    {

        //Debug.Log("In SetSpheremapTrans");

        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i] < 0)
            {
                trans[i] = 0;
            }
            if (trans[i] > 1)
            {
                trans[i] = 1;
            }
        }

        for(int k = 1; k < trans.Length-2; k++)
        {

            if((trans[k]+trans[k-1]) > 1 || (trans[k] + trans[k + 1]) > 1)
            {

                if((trans[k] + trans[k - 1]) > 1)
                {

                    float tSum = (trans[k] + trans[k - 1]);
                    float alpha = tSum - 1;

                    if(trans[k] == 1)
                    {
                        trans[k - 1] = 0.0f;
                    }
                    else if (trans[k - 1] == 1)
                    {
                        trans[k] = 0.0f;
                    }
                    else
                    {
                        trans[k] = trans[k] - (alpha / 2);
                        trans[k - 1] = trans[k - 1] - (alpha / 2);
                    }

                }

                else if ((trans[k] + trans[k + 1]) > 1)
                {

                    float tSum = (trans[k] + trans[k + 1]);
                    float alpha = tSum - 1;

                    if (trans[k] == 1)
                    {
                        trans[k + 1] = 0.0f;
                    }
                    else if (trans[k + 1] == 1)
                    {
                        trans[k] = 0.0f;
                    }
                    else
                    {
                        trans[k] = trans[k] - (alpha / 2);
                        trans[k + 1] = trans[k + 1] - (alpha / 2);
                    }

                }

            }

        }

        if(trans[0]+trans[NumberOfImages-1] > 1)
        {

            float tSum = trans[0] + trans[NumberOfImages - 1];
            float alpha = tSum - 1;

            if(trans[0] == 1)
            {
                trans[NumberOfImages-1] = 0;
            }
            else if(trans[NumberOfImages-1] == 1)
            {
                trans[0] = 0;
            }
            else
            {
                trans[0] = trans[0] - (alpha / 2);
                trans[NumberOfImages - 1] = trans[NumberOfImages - 1] - (alpha / 2);
            }

            Debug.Log("TransParency for 360/0 changed. 360 = " + trans[NumberOfImages - 1] + ". 0 = " + trans[0]);

        }


        for (int i = 0; i < NumberOfImages; i++)
        {

            Debug.Log("Degree = " + CurrentDegree);

            leftSphereMapRenderer[i].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            rightSphereMapRenderer[i].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);


        }

    }

    void transHandler()
    {

        float sliceSize = 360 / NumberOfImages;
        

        float normalTrans = 1 / sliceSize;

       // Debug.Log("Current Degree = " + CurrentDegree);

        if (CurrentDegree >= 0 && CurrentDegree <= 342)
        {

            for (int i = 0; i < trans.Length; i++)
            {

                float tmpSliceSize = sliceSize * i;

                float angularDiff = CurrentDegree - tmpSliceSize;

                if (angularDiff < 0)
                {
                    angularDiff = -angularDiff;
                }


                if (angularDiff < sliceSize && angularDiff >= 0)
                {

                    leftSphereMaps[i].SetActive(true);
                    rightSphereMaps[i].SetActive(true);
                    trans[i] = ((sliceSize - angularDiff) * normalTrans);

                }

                else
                {
                    trans[i] = 0;
                    leftSphereMaps[i].SetActive(false);
                    rightSphereMaps[i].SetActive(false);
                }

                //Debug.Log("Angular Difference for img " + i + " = " + diff[i]);

            }

        }

        if(CurrentDegree > 342 && CurrentDegree <= 360)
        {


            leftSphereMaps[NumberOfImages - 2].SetActive(false);
            rightSphereMaps[NumberOfImages - 2].SetActive(false);

            leftSphereMaps[1].SetActive(false);
            rightSphereMaps[1].SetActive(false);

            /*if(CurrentDegree == 360)
            {
                CurrentDegree = 0;
            }*/

            float angularDiffEnd = CurrentDegree - 360;
            float angularDiff = CurrentDegree - (360 - sliceSize);

            if (angularDiff < 0)
            {
                angularDiff = -angularDiff;
            }
            if (angularDiffEnd < 0)
            {
                angularDiffEnd = -angularDiffEnd;
            }

            if (angularDiffEnd < sliceSize && angularDiffEnd >= 0)
            {

                leftSphereMaps[0].SetActive(true);
                rightSphereMaps[0].SetActive(true);
                trans[0] = ((sliceSize - angularDiffEnd) * normalTrans);
                

            }
            else
            {
                trans[0] = 0;
                leftSphereMaps[0].SetActive(false);
                rightSphereMaps[0].SetActive(false);
            }
            if(angularDiff < sliceSize && angularDiff >= 0)
            {

                leftSphereMaps[NumberOfImages - 1].SetActive(true);
                rightSphereMaps[NumberOfImages - 1].SetActive(true);
                trans[NumberOfImages - 1] = ((sliceSize - angularDiff) * normalTrans);

            }
            else
            {
                trans[NumberOfImages - 1] = 0;
                leftSphereMaps[NumberOfImages - 1].SetActive(false);
                rightSphereMaps[NumberOfImages - 1].SetActive(false);
            }



        }
        

        //Debug.Log("trans.length = " + trans.Length);


    }


    void debugArray()
    {

        for(int i = 0; i < NumberOfImages; i++)
        {

            Debug.Log("Transparency " + i + " = " + trans[i]);

        }

    }

}