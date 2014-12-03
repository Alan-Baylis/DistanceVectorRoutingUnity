using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeInfo : MonoBehaviour
{
    public Text text;
    private int[,] lineMatrix;
    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    public List<char> routeNode;
    public List<int> routeCost;

    void Start()
    {
        routeNode = new List<char>();
        routeCost = new List<int>();
        GenerateDistanceVector();
    }

    void Update()
    {
        GenerateDistanceVector();
    }

    void OnMouseDown()
    {
        text.text = "NODE NAME : " + gameObject.GetComponentInChildren<TextMesh>().text + "\n";
        for (int i=0; i<routeNode.Count; ++i)
        {
            text.text += routeNode[i] + "  -  " + routeCost[i].ToString() + "\n";
        }
    }

    void GenerateDistanceVector()
    {
        routeCost.Clear();
        routeNode.Clear();

        int RouteIndex =0;

        lineMatrix = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
        for (int i=0; i<alpha.Length; ++i)
        {
            if (gameObject.GetComponentInChildren<TextMesh>().text == alpha [i].ToString())
            {
                RouteIndex = i;
            }
        }

        int Index = 0;

        for (int i=0; i<alpha.Length; ++i)
        {
            if (lineMatrix[i, RouteIndex] != 0 || lineMatrix[RouteIndex, i] != 0)
            {
                routeNode.Add(alpha [i]);
                if (lineMatrix [i, RouteIndex] != 0)
                {
                    routeCost.Add (lineMatrix [i, RouteIndex]);
                } else
                {
                    routeCost.Add (lineMatrix [RouteIndex, i]);
                }
                ++Index;
            }
        }
    }
}
