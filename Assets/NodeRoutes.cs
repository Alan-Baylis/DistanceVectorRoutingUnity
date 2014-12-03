using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeRoutes : MonoBehaviour
{
    private GameObject[] Node;

    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public Button buttonAdd;
    public Button buttonDeleteRoute;
    public Button buttonDeleteNode;

    public InputField inputAddRouteA;
    public InputField inputAddRouteB;
    public InputField inputCost;

    public InputField inputDeleteRouteA;
    public InputField inputDeleteRouteB;

    public InputField inputDeleteNode;

    private int[,] lineMatrix;

    public void addRoute()
    {
        lineMatrix = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
        int first = 0, second = 0;
        bool firstBool = false, secondBool = false;
   
        for (int i=0; i<alpha.Length; ++i)
        {
            if (inputAddRouteA.text.text.ToLower() == alpha[i].ToString().ToLower())
            {
                first = i;
                firstBool = true;
            }
            if (inputAddRouteB.text.text.ToLower() == alpha[i].ToString().ToLower())
            {
                second = i;
                secondBool = true;
            }
        }

        if (secondBool && firstBool)
        {
            lineMatrix[first,second] = int.Parse(inputCost.text.text);
        }

        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix = lineMatrix;
        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().drawLines();
    }

    public void deleteRout ()
    {
        lineMatrix = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;

        int first = 0, second = 0;
        bool firstBool = false, secondBool = false;
        
        for (int i=0; i<alpha.Length; ++i)
        {
            if (inputDeleteRouteA.text.text.ToLower() == alpha[i].ToString().ToLower())
            {
                first = i;
                firstBool = true;
            }
            if (inputDeleteRouteB.text.text.ToLower() == alpha[i].ToString().ToLower())
            {
                second = i;
                secondBool = true;
            }
        }

        if (secondBool && firstBool)
        {
            lineMatrix[first,second] = 0;
            lineMatrix[second,first] = 0;
        }

        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix = lineMatrix;
        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().drawLines();
    }

    public void deleteNode()
    {
        lineMatrix = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
        Node = GameObject.FindGameObjectsWithTag("node");

        int deleteIndex = 0;
        bool deleteIndexBool = false;

        for (int i=0; i<Node.Length; ++i)
        {
            if (inputDeleteNode.text.text.ToLower() == Node[i].GetComponentInChildren<TextMesh>().text.ToLower())
            {
                Destroy(Node[i]);
                break;
            }
        }

        for (int i=0; i<alpha.Length; ++i)
        {
            if (inputDeleteNode.text.text.ToLower() == alpha [i].ToString().ToLower())
            {
                deleteIndex = i;
                deleteIndexBool = true;
                break;
            }
        }
        if (deleteIndexBool)
        {
            for (int i=0; i<alpha.Length; ++i)
            {
                lineMatrix[i, deleteIndex] = 0;
                lineMatrix[deleteIndex, i] = 0;
            }
        }

        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix = lineMatrix;
        GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().drawLines();
    }

}
