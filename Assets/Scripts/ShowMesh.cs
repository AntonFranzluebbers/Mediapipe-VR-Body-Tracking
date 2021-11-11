using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowMesh : MonoBehaviour
{
    public DataServer client;

    public float scale = .01f;
    public List<GameObject> pool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var data = client.GetData();
        if (data != null)
        {
            GeneratePoints(data);
        }
    }


    void GeneratePoints(BodyTrackingData featureData)
    {

        // make new objects
        while (featureData.body.Length > pool.Count)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.SetParent(transform);
            obj.transform.localScale = Vector3.one * scale;

            pool.Add(obj);
        }

        // set body positions
        for (int i = 0; i < featureData.body.Length; i++)
        {
            Vector3 feature = featureData.body[i];
            var t = pool[i].transform;
            t.localPosition = Vector3.Lerp(t.localPosition, new Vector3(feature.x, -feature.y, feature.z), .2f);
            t.localScale = Vector3.one * scale;
            t.name = "body_" + i;
        }

        Debug.DrawRay(transform.TransformPoint(featureData.HeadPos), transform.TransformDirection(featureData.HeadForward), Color.blue);
    }
}
