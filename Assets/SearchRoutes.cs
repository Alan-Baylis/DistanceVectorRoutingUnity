using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SearchRoutes : MonoBehaviour {

    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public InputField text;
    private int[,] lineMatrixTemp;
    private GameObject[] Node;

    public void buttonSearch()
    {
        Node = GameObject.FindGameObjectsWithTag("node");
        //text = GameObject.FindGameObjectWithTag("SearchInput").GetComponent<Text>();

        lineMatrixTemp = new int[alpha.Length,alpha.Length];
        for (int j=0; j<alpha.Length; ++j)
        {
            for (int k=0; k<alpha.Length; ++k)
            {
                lineMatrixTemp[j,k] = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix[j,k];
            }
        }

        for (int i=0; i<Node.Length; ++i)
        {
            if (text.text.text.ToLower() == Node[i].GetComponentInChildren<TextMesh>().text.ToLower())
            {

               // lineMatrixTemp = GameObject.FindGameObjectWithTag("background").GetComponent<DrawLine>().lineMatrix;
                Node[i].GetComponent<NodeInfo>().searchAll(lineMatrixTemp);
            }
        }
    }
}
