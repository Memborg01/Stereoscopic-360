using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{
    
    public int CurrentDegree;
    
    int a, HasBeenSwitched = 0;

    public int NumberOfImages;
    

    // New Variables for Dynamic Setup

    public Renderer[] sphereMapRenderers;
    int imagesPerEye;
    float[] trans;

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
        a = CurrentDegree / (360 / NumberOfImages);
        if (CurrentDegree > 360) { CurrentDegree = CurrentDegree - 360; }
        if (CurrentDegree < 0) { CurrentDegree = CurrentDegree + 360; }

        //Debug.Log("Degrees Checked");


        // - al tudom mozgatni elore

        //Trans1 = (CurrentDegree-18) / 18f;
        /*  if (CurrentDegree > 36) { trans[0] = (36f - (CurrentDegree - 36f)) / 36f; } else { trans[0] = CurrentDegree / 36f; }
          if (CurrentDegree > 72) { trans[1] = (54f - (CurrentDegree - 54f)) / 36f; } else { trans[1] = (CurrentDegree - 36) / 36f; }
          if (CurrentDegree > 108) { trans[2] = (72f - (CurrentDegree - 72f)) / 36f; } else { trans[2] = (CurrentDegree - 72) / 36f; }
          if (CurrentDegree > 144) { trans[3] = (90f - (CurrentDegree - 90f)) / 36f; } else { trans[3] = (CurrentDegree - 108) / 36f; }
          if (CurrentDegree > 180) { trans[4] = (108f - (CurrentDegree - 108f)) / 36f; } else { trans[4] = (CurrentDegree - 144) / 36f; }
          if (CurrentDegree > 216) { trans[5] = (144f - (CurrentDegree - 144f)) / 36f; } else { trans[5] = (CurrentDegree - 180) / 36f; }
          if (CurrentDegree > 252) { trans[6] = (180f - (CurrentDegree - 180f)) / 36f; } else { trans[6] = (CurrentDegree - 216) / 36f; }
          if (CurrentDegree > 288) { trans[7] = (216f - (CurrentDegree - 216f)) / 36f; } else { trans[7] = (CurrentDegree - 252) / 36f; }
          if (CurrentDegree > 324) { trans[8] = (252f - (CurrentDegree - 252f)) / 36f; } else { trans[8] = (CurrentDegree - 288) / 36f; }*/
        //if (CurrentDegree>288){Trans8 = (216f-(CurrentDegree-216f)) / 36f;} else {Trans8 = (CurrentDegree-252)/ 36f;}

        // Debug.Log("Transparencies Checked");

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

        SetRenderer();


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

        //Debug.Log("In Set Renderer");

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

            //Debug.Log("Renderer " + i + " left = " + tmpLeftRenderer + "; right = " + tmpRightRenderer);
            //Debug.Log("i = " + i);

        }


    }

    void SetSpheremapTransparency()
    {

        //Debug.Log("In SetSpheremapTrans");

        for (int i = 0; i < imagesPerEye; i++)
        {

            /*
            Material leftMat = sphereMapRenderers[i].GetComponent<Material>();
            Material rightMat = sphereMapRenderers[i + imagesPerEye].GetComponent<Material>();

            leftMat.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            rightMat.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            */

            sphereMapRenderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);
            sphereMapRenderers[i + imagesPerEye].material.color = new Color(1.0f, 1.0f, 1.0f, trans[i]);


            /*Debug.Log("transparency " + i + " = " + trans[i]);
            Debug.Log("i = " + i);*/

        }

    }

    void transHandler()
    {

        float sliceSize = 360 / NumberOfImages;


        for(int i = 0; i < trans.Length; i++)
        {

            float tmpSliceSize = sliceSize * (i + 1);

            float angularDiff = Mathf.Abs(CurrentDegree - tmpSliceSize);

            if(angularDiff <= 18)
            {
                if (CurrentDegree > tmpSliceSize)
                {
                    trans[i] = (tmpSliceSize - (CurrentDegree - tmpSliceSize)) / tmpSliceSize;
                }
                else
                {
                    trans[i] = CurrentDegree / tmpSliceSize;
                }
                Mathf.Clamp01(trans[i]);
            }

            else
            {
                trans[i] = 0;
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