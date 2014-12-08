using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeInfo : MonoBehaviour
{
    private GameObject[] Node;
    public Text text;
    public Text text2;
    private int[,] lineMatrix;
    private int[,] lineMatrixWithoutReference;
    // private int[,] lineMatrixTemp;
    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    public List<char> routeNode;
    public List<int> routeCost;
    public List<char> routeNodeFinal;
    public List<int> routeCostFinal;

    void Start()
    {
        text = GameObject.FindGameObjectWithTag("RouteInfo").GetComponent<Text>();
        routeNode = new List<char>();
        routeCost = new List<int>();
        routeNodeFinal = new List<char>();
        routeCostFinal = new List<int>();
    }

    void Update()
    {
        //generateDistanceVector();
    }

    void OnMouseDown()
    {
        text.text = "NODE NAME : " + gameObject.GetComponentInChildren<TextMesh>().text + "\n";
        for (int i=0; i<routeNodeFinal.Count; ++i)
        {
            text.text += routeNodeFinal [i] + "  -  " + routeCostFinal [i].ToString() + "\n";
        }
    }

    public void generateDistanceVector(int[,] lineMatrixTemp)
    {
        routeCost.Clear();
        routeNode.Clear();

        int RouteIndex = 0;

        //lineMatrixTemp = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
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
            if (lineMatrixTemp [i, RouteIndex] != 0 || lineMatrixTemp [RouteIndex, i] != 0)
            {
                routeNode.Add(alpha [i]);
                if (lineMatrixTemp [i, RouteIndex] != 0)
                {
                    routeCost.Add(lineMatrixTemp [i, RouteIndex]);
                } else
                {
                    routeCost.Add(lineMatrixTemp [RouteIndex, i]);
                }
                ++Index;
            }
        }
    }

    public void getDistanceVector(char neighborNode, int[,] lineMatrixTemp, int cost)
    {
        Node = GameObject.FindGameObjectsWithTag("node");
        GameObject temp = Node [0];
        for (int i=0; i<Node.Length; ++i)
        {
            if (Node [i].GetComponentInChildren<TextMesh>().text == neighborNode.ToString())
            {
                temp = Node [i];
                break;
            } else
            {
                temp = null;
            }
        }
        if (temp != null)
        {
            //call next node
            temp.GetComponent<NodeInfo>().searchAll(lineMatrixTemp);

            int RouteIndex = 0;
        
            //lineMatrixTemp = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
            for (int i=0; i<alpha.Length; ++i)
            {
                if (neighborNode == alpha [i])
                {
                    RouteIndex = i; 
                    break;
                }
            }
           
            int Index = 0;
        
            for (int i=0; i<temp.GetComponent<NodeInfo>().routeCostFinal.Count; ++i)
            {      
                routeNodeFinal.Add(temp.GetComponent<NodeInfo>().routeNodeFinal [i]);
                routeCostFinal.Add(temp.GetComponent<NodeInfo>().routeCostFinal [i] + cost);
            }
        }
    }

    public void searchAll(int[,] lineMatrixTemp)
    {
        int first = 0, second = 0, costTemp = 0;
   

        routeNodeFinal.Clear();
        routeCostFinal.Clear();

        fillFirstNodes(lineMatrixTemp);
        generateDistanceVector(lineMatrixTemp);

        //line matrix without possibilyt ro search this node again - for reference
        lineMatrixWithoutReference = new int[alpha.Length, alpha.Length];
        for (int j=0; j<alpha.Length; ++j)
        {
            for (int k=0; k<alpha.Length; ++k)
            {
                lineMatrixWithoutReference [j, k] = lineMatrixTemp [j, k];
            }
        }

        for (int i=0; i<routeNode.Count; ++i)
        {
            for (int j=0; j<alpha.Length; ++j)
            {
                if (gameObject.GetComponentInChildren<TextMesh>().text == alpha [j].ToString())
                {
                    first = j;
                    break;
                }
            }
            for (int j=0; j<alpha.Length; ++j)
            {
                if (routeNode [i] == alpha [j])
                {
                    second = j;
                    break;
                }
            }

            lineMatrixWithoutReference [first, second] = 0;
            lineMatrixWithoutReference [second, first] = 0;
        }



        for (int i=0; i<routeNode.Count; ++i)
        {
            for (int j=0; j<alpha.Length; ++j)
            {
                if (gameObject.GetComponentInChildren<TextMesh>().text == alpha [j].ToString())
                {
                    first = j;
                    break;
                }
            }
            for (int j=0; j<alpha.Length; ++j)
            {
                if (routeNode [i] == alpha [j])
                {
                    second = j;
                    break;
                }
            }
            if (lineMatrixTemp [first, second] != 0)
            {
                costTemp = lineMatrixTemp [first, second];
            } else
            {
                costTemp = lineMatrixTemp [second, first];
            }
            //lineMatrixTemp [first, second] = 0;
            //lineMatrixTemp [second, first] = 0;
            getDistanceVector(routeNode [i], lineMatrixWithoutReference, costTemp);
        }
    }

    private void fillFirstNodes(int[,] lineMatrixTemp)
    {
        int RouteIndex = 0;
        
        //lineMatrixTemp = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
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
            if (lineMatrixTemp [i, RouteIndex] != 0 || lineMatrixTemp [RouteIndex, i] != 0)
            {
                routeNodeFinal.Add(alpha [i]);
                if (lineMatrixTemp [i, RouteIndex] != 0)
                {
                    routeCostFinal.Add(lineMatrixTemp [i, RouteIndex]);
                } else
                {
                    routeCostFinal.Add(lineMatrixTemp [RouteIndex, i]);
                }
                ++Index;
            }
        }
    }


}
