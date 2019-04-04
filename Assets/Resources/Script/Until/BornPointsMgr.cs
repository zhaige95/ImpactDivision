using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornPointsMgr : MonoBehaviour {
    public Transform interimPoint;
    public List<Transform> camp1 = new List<Transform>();
    public List<Transform> camp2 = new List<Transform>();

    Dictionary<int, List<Transform>> points = new Dictionary<int, List<Transform>>();

    private void Awake()
    {
        Battle.bornMgr = this;
    }

    void Start () {
        var p = new List<Transform>();
        foreach (var point in camp1)
        {
            p.Add(point);
        }
        points.Add(1, p);

        p = new List<Transform>();
        foreach (var point in camp2)
        {
            p.Add(point);
        }
        points.Add(2, p);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetPoint(int camp)
    {
        List<Transform> data = points[camp];
        int range = Random.Range(0, data.Count);
        return data[range];
    }

}
