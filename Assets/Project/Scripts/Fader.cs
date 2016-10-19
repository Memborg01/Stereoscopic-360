using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{

    public float CurrentDegree;

    public int NumberOfImages;
    

    // New Variables for Dynamic Setup

    public Renderer[] sphereMapRenderers;
    float[] trans;

    //public float transCorrector = 1.3f;

    //bool stereoShifted = false;


    void Start()
    {

        // Debug.Log("Start Function Run");

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

        NumberOfImages = leftSpheremaps.Length;

        Debug.Log("Left sm = " + leftSpheremaps.Length);
        Debug.Log("Number of Images = " + NumberOfImages);

        sphereMapRenderers = new Renderer[NumberOfImages*2];
        trans = new float[NumberOfImages];

        int tmpIndex = 0;
      
        for(int i = NumberOfImages-1; i >= 0; i--)
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
            tmpRightRenderer = sphereMapRenderers[i + NumberOfImages];


        }


    }

    void SetSpheremapTransparency()
    {

        //Debug.Log("In SetSpheremapTrans");

        for (int i = 0; i < NumberOfImages; i++)
        {


            sphereMapRenderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            sphereMapRenderers[i + NumberOfImages].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);


        }

    }

    void transHandler()
    {

        float sliceSize = 360 / NumberOfImages;

        float[] diff = new float[trans.Length];

        float normalTrans = 1 / sliceSize;

        bool endPoint = false;

        Debug.Log("Current Degree = " + CurrentDegree);

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

                diff[i] = angularDiff;

                if (angularDiff <= sliceSize && angularDiff >= 0)
                {

                    trans[i] = ((sliceSize - angularDiff) * normalTrans);

                }

                else
                {
                    trans[i] = 0;
                }



                //Debug.Log("Angular Difference for img " + i + " = " + diff[i]);

            }



        }


        //Debug.Log("trans.length = " + trans.Length);


    }

    void flipLayer()
    {


        GameObject[] leftSpheremaps, rightSpheremaps;
        leftSpheremaps = GameObject.FindGameObjectsWithTag("left");
        rightSpheremaps = GameObject.FindGameObjectsWithTag("right");

       // Debug.Log("Current degree = " + CurrentDegree);



    }


}