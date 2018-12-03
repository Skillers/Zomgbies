using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScanner : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sound")
        {
            int Loudness = (int)other.GetComponent<SphereCollider>().radius;
            Vector3 originP = new Vector3(Mathf.Round(other.transform.position.x), Mathf.Round(other.transform.position.y), Mathf.Round(other.transform.position.z));
            Vector3 myEarsP = new Vector3(Mathf.Round (transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            int Distance = (int)Vector3.Magnitude(originP - myEarsP);
            float MaxFlux = Loudness - Distance;
            int xLayers = (int)(other.transform.position.x - transform.position.x);
            int yLayers = (int)(other.transform.position.y - transform.position.y);
            int zLayers = (int)(other.transform.position.z - transform.position.z);
            if (xLayers < yLayers)
            {
                if(yLayers < zLayers)
                {
                    Debug.Log("We are moving over Z");
                }else
                {
                    Debug.Log("We are moving over Y");
                }
            }else
            {
                if(xLayers < zLayers)
                {
                    Debug.Log("We are  moving over Z");
                }else
                {
                    Debug.Log("We are moving over X " + MaxFlux);
                    LayerScanX(originP, myEarsP, Distance, (int)Mathf.Ceil(MaxFlux * 2 + 1), MaxFlux, yLayers, zLayers);

                }
            }
          
             
            
        }
    }

    private void LayerScanX(Vector3 Start, Vector3 end, int movingLayer, int GridSize, float maxFlux, int axisOneStep, int axisTwoStep)
    {
        
        List<float> layerOld = new List<float>();
        List<Vector3> layerPOld = new List<Vector3>();
        for (int i = 1; i <= movingLayer; i++)
        {
            int axisOneCurrentStep = (int)Mathf.Round((float)i/(float)movingLayer * (float)axisOneStep);
            int axisTwoCurrentStep = (int)Mathf.Round((float)i / (float)movingLayer * (float)axisOneStep);
            List<float> layer = new List<float>();
            List<Vector3> layerP = new List<Vector3>();
            if (layerOld.Count == 0)
            {
                for(int j = 0; j < GridSize; j++)
                {
                    for(int l = 0; l < GridSize; l++)
                    {
                        float tempValue = Vector3.Magnitude(new Vector3(i, j - (int)Mathf.Floor(GridSize / 2), l - (int)Mathf.Floor(GridSize / 2))) - 1;
                        Vector3 tempCords = new Vector3(Start.x + i, Start.y + j + axisTwoCurrentStep, Start.z + l +  axisTwoCurrentStep);
                        if (tempValue <= maxFlux)
                        {
                            layer.Add(tempValue);
                            layerP.Add(tempCords);
                        }
                    }
                }
            }else if(i < movingLayer)
            {
                int tempOldVal = -1;
                foreach (float oldValue in layerOld)
                {
                    tempOldVal++;
                    for (int j = 0; j < GridSize; j++)
                    {
                        for (int l = 0; l < GridSize; l++)
                        {
                            float tempValue = Vector3.Magnitude(new Vector3(Start.x + i, Start.y + j + axisOneCurrentStep, Start.z + l + axisTwoCurrentStep) - layerPOld[tempOldVal]) - 1;
                            Vector3 tempCords = new Vector3(Start.x + i, Start.y + j + axisTwoCurrentStep, Start.z + l + axisTwoCurrentStep);
                            if (tempValue + oldValue <= maxFlux)
                            {
                                layer.Add(tempValue + oldValue);
                                layerP.Add(tempCords);
                            }
                        }
                    }
                }
            }else{
                Debug.Log("Last Chance");
            }
            layerOld.Clear();
            layerPOld.Clear();
            layerOld = layer;
            layerPOld = layerP;
            Debug.Log("Routes : " + layerOld.Count);
            Debug.Log("Count : " + i);
         }
    }

 
}
