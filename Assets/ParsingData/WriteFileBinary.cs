using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteFileBinary : MonoBehaviour
{
    const string fileName = "PointList.dat";
    
    [HideInInspector]
    public List<Vector3> points;

    public void WriteFile(List<Vector3> _points)
    {
        points = _points;
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            foreach(Vector3 v in points)
            {
                writer.Write(v.x);
                writer.Write(v.y);
                writer.Write(v.z);
            }
        }

        print("save");
    }

    public void LoadFiles()
    {
        points.Clear();
        points = new List<Vector3>(70_000_000);

        if (File.Exists(fileName))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                try
                {
                    while(true)
                    {
                        var v = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        points.Add(v);
                    }
                }
                catch(Exception e)
                {

                }
            }
        }

        print($"load {points.Count}");
        print($"0 : {points[0]}");
        print($"100 : {points[100]}");
    }
}