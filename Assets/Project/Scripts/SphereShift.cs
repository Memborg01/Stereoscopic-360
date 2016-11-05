using UnityEngine;
using System.Collections;

public class SphereShift : MonoBehaviour {

    public SphereMapList tpList;
    public GameObject tpManager;
    int smAmount;
    public int tpPoint;

	// Use this for initialization
	void Start () {

        tpManager = this.gameObject;
        tpList = tpManager.GetComponent<SphereMapList>();
        smAmount = tpList.sphereMaps.Length;

        SetStartPos();
	}
	
	// Update is called once per frame
	void Update () {

        Teleport();

	}

    void Teleport()
    {

        if(tpPoint >= smAmount)
        {
            tpPoint = smAmount - 1;
        }
        else if(tpPoint < 0)
        {
            tpPoint = 0;
        }

        for(int i = 0; i < smAmount; i++)
        {
            if (i != tpPoint)
            {
                tpList.sphereMaps[i].SetActive(false);
            }
            else if(i == tpPoint)
            {
                tpList.sphereMaps[i].SetActive(true);
            }
        }

    }

    void SetStartPos()
    {

        tpList.sphereMaps[0].SetActive(true);

        for(int i = 1; i < smAmount; i++)
        {

            tpList.sphereMaps[i].SetActive(false);

        }

    }

}
