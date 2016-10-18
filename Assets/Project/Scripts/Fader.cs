using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{
    
    public int CurrentDegree;

    public int NumberOfImages;
    

    // New Variables for Dynamic Setup

    public Renderer[] sphereMapRenderers;
    int imagesPerEye;
    float[] trans;

    public float transCorrector = 1.3f;

    bool stereoShifted = false;


    void Start()
    {

       // Debug.Log("Start Function Run");

        GetRenderer();

       // Debug.Log("Start Function Ended");


    }
    void Update()
    {

        //Debug.Log("In Update");
        if (CurrentDegree > 360) { CurrentDegree = CurrentDegree - 360; }
        if (CurrentDegree < 0) { CurrentDegree = CurrentDegree + 360; }

        flipLayer();

        transHandler();

        for(int i = 0; i < trans.Length; i++)
        {
            if(trans[i] < 0)
            {
                trans[i] = 0;
            }
            if (trans[i] > 1)
            {
                trans[i] = 1;
            }
        }

        SetSpheremapTransparency();

       // SetRenderer();


    }

    void GetRenderer()
    {

        //Debug.Log("GetRender is Run");

        GameObject[] leftSpheremaps, rightSpheremaps;
        leftSpheremaps = GameObject.FindGameObjectsWithTag("left");
        rightSpheremaps = GameObject.FindGameObjectsWithTag("right");

       // Debug.Log("GameObjects initialized and assigned");

        imagesPerEye = leftSpheremaps.Length;

        NumberOfImages = leftSpheremaps.Length + rightSpheremaps.Length;

        sphereMapRenderers = new Renderer[NumberOfImages];
        trans = new float[imagesPerEye];

        int tmpIndex = 0;
      
        for(int i = leftSpheremaps.Length-1; i >= 0; i--)
        {

            Renderer tmpLeftRenderer = leftSpheremaps[i].GetComponent<Renderer>();
            Renderer tmpRightRenderer = rightSpheremaps[i].GetComponent<Renderer>();

                   
            sphereMapRenderers[tmpIndex] = tmpLeftRenderer;
            sphereMapRenderers[tmpIndex + leftSpheremaps.Length] = tmpRightRenderer;

            tmpIndex++;

            //Debug.Log("For-loop running... " + i);


        }

        //Debug.Log("for-loop ended");



    }

    void SetRenderer()
    {


        GameObject[] leftSpheremaps, rightSpheremaps;
        leftSpheremaps = GameObject.FindGameObjectsWithTag("left");
        rightSpheremaps = GameObject.FindGameObjectsWithTag("right");


        for (int i = 0; i < leftSpheremaps.Length; i++)
        {


            Renderer tmpLeftRenderer = leftSpheremaps[i].GetComponent<Renderer>();
            Renderer tmpRightRenderer = rightSpheremaps[i].GetComponent<Renderer>();

            Debug.Log("Trans parency for trans[" + i + "] = " + trans[i]);

            tmpLeftRenderer = sphereMapRenderers[i];
            tmpRightRenderer = sphereMapRenderers[i + imagesPerEye];


        }


    }

    void SetSpheremapTransparency()
    {

        //Debug.Log("In SetSpheremapTrans");

        for (int i = 0; i < imagesPerEye; i++)
        {


            sphereMapRenderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            sphereMapRenderers[i + imagesPerEye].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);


        }

    }

    void transHandler()
    {

        float sliceSize = 360 / NumberOfImages;

        float[] diff = new float[trans.Length];

        float normalTrans = 1 / sliceSize;

        if (CurrentDegree >= 0 && CurrentDegree < 180)
        {

            for (int i = 0; i < trans.Length; i++)
            {

                float tmpSliceSize = sliceSize * i;

                float angularDiff = CurrentDegree - tmpSliceSize;

                if (angularDiff < 0)
                {
                    angularDiff = -angularDiff;
                }

                diff[i] = angularDiff;

                if (angularDiff <= 18 && angularDiff >= 0)
                {

                    trans[i] = ((sliceSize - angularDiff) * normalTrans) * transCorrector;

                }

                else
                {
                    trans[i] = 0;
                }

                Debug.Log("Angular Difference for img " + i + " = " + diff[i]);

            }
        }
        else if (CurrentDegree >= 180 && CurrentDegree < 360)
        {

            for (int i = 0; i < trans.Length; i++)
            {

                float tmpSliceSize = sliceSize * (i + imagesPerEye);

                float angularDiff = CurrentDegree - tmpSliceSize;

                if (angularDiff < 0)
                {
                    angularDiff = -angularDiff;
                }

                diff[i] = angularDiff;

                if (angularDiff <= 18 && angularDiff >= 0)
                {

                    trans[i] = ((sliceSize - angularDiff) * normalTrans) * transCorrector;

                }

                else
                {
                    trans[i] = 0;
                }

                Debug.Log("Angular Difference for img " + i + " = " + diff[i]);
                

            }

        }
        else if(CurrentDegree > 342 && CurrentDegree <= 360 || CurrentDegree >= 0 && CurrentDegree < 18)
        {


            float tmpSliceSize;

            if (CurrentDegree >= 0 && CurrentDegree <= 18)
            {
                tmpSliceSize = sliceSize;
            }
            else
            {
                tmpSliceSize = sliceSize * 20;
            }

            float angularDiff = CurrentDegree - tmpSliceSize;

            if (angularDiff < 0)
            {
                angularDiff = -angularDiff;
            }

            

            if (angularDiff <= 18 && angularDiff >= 0)
            {

                trans[0] = ((sliceSize - angularDiff) * normalTrans) * transCorrector;

            }

            else
            {
                trans[0] = 0;
            }
        }

        Debug.Log("Images pr eye = " + trans.Length);

        //Debug.Log("trans.length = " + trans.Length);


    }

    void flipLayer()
    {


        GameObject[] leftSpheremaps, rightSpheremaps;
        leftSpheremaps = GameObject.FindGameObjectsWithTag("left");
        rightSpheremaps = GameObject.FindGameObjectsWithTag("right");

       // Debug.Log("Current degree = " + CurrentDegree);

        if (CurrentDegree >= 180 && stereoShifted == false)
        {

            for (int i = 0; i < imagesPerEye; i++)
            {

                leftSpheremaps[i].gameObject.layer = 9;
                rightSpheremaps[i].gameObject.layer = 8;

            }

            stereoShifted = true;


        }
        else if (CurrentDegree < 180 && stereoShifted == true)
        {

            for (int i = 0; i < imagesPerEye; i++)
            {

                leftSpheremaps[i].gameObject.layer = 8;
                rightSpheremaps[i].gameObject.layer = 9;

            }

            stereoShifted = false;

        }

    }


}