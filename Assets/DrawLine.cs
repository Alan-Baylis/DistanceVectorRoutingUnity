using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    private GameObject[] Node;
    private List<LineRenderer> Line;
    private List<GameObject> Text;

    public int[,] lineMatrix;

    public Font font;
    public Material materialLine;
    public Material material;

    public GameObject newNode;

    void Start()
    {
        lineMatrix = new int[alpha.Length,alpha.Length];

        Text = new List<GameObject>();
        Line = new List<LineRenderer>();
        Node = GameObject.FindGameObjectsWithTag("node");
        lineMatrix [0, 1] = 5;
        lineMatrix [1, 2] = 10;
        drawLines();

    }

    void Update()
    {
        Node = GameObject.FindGameObjectsWithTag("node");
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click.");
            CreateNode();
        }

    }

    public void drawLines ()
    {
        ///cia nezinia ar veiks////
        for (int i=0; i<Line.Count; ++i)
        {
            Destroy(Text[i]);
            Destroy(Line[i]);
        }
        Line.Clear();
        Text.Clear();

        for (int i=0; i<alpha.Length; ++i)
        {
            for (int j=0; j<alpha.Length; ++j)
            {
                if (lineMatrix[i,j]!=0)
                {
                    int first = 0, second = 0;

                    for (int k=0; k<Node.Length; ++k)
                    {
                        if(Node [k].GetComponentInChildren<TextMesh>().text == alpha[i].ToString())
                        {
                            first=k;
                        }
                        if(Node [k].GetComponentInChildren<TextMesh>().text == alpha[j].ToString())
                        {
                            second=k;
                        }
                    }
                    line (Node [first].transform.position, Node[second].transform.position, lineMatrix[i,j].ToString());
                }
            }
        }
    }

    public void line(Vector3 pointA, Vector3 pointB, string linkCost)
    {
        LineRenderer lRend = new GameObject().AddComponent("LineRenderer") as LineRenderer;
        Line.Add(lRend);
        lRend.material = materialLine;
                    
        lRend.SetPosition(0, pointA);
        lRend.SetPosition(1, pointB);

        Vector3 temp = new Vector3((pointA.x + pointB.x)/2, ((pointA.y + pointB.y)/2)+4, -1);

        GameObject myTextObject = new GameObject("Text");
        Text.Add(myTextObject);

        myTextObject.transform.position = temp;

        myTextObject.AddComponent("TextMesh");
        //myTextObject.AddComponent("MeshRenderer");
        //myTextObject.renderer.material.color = Color.black;
        
        // Get components
        TextMesh textMeshComponent = myTextObject.GetComponent(typeof(TextMesh)) as TextMesh;
        MeshRenderer meshRendererComponent = myTextObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        
        // Set font of TextMesh component (it works according to the inspector)
        textMeshComponent.font = font;
        
        // Create an array of materials for the MeshRenderer component (it works according to the inspector)
        meshRendererComponent.material = material;
        
        // Set the text string of the TextMesh component (it works according to the inspector)
        textMeshComponent.text = linkCost;
        textMeshComponent.color = Color.red;
        textMeshComponent.fontSize = 30;
       
    }

    void CreateNode ()
    {
        bool isThere = false;

        Vector3 v3 = Input.mousePosition;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        v3.z = -1;

        GameObject test = Instantiate(newNode, v3, newNode.transform.rotation)as GameObject;

        for (int i=0; i<alpha.Length; ++i)
        {
            isThere=false;
            for(int j=0; j<Node.Length; ++j)
            {
                if (Node[j].GetComponentInChildren<TextMesh>().text == alpha[i].ToString())
                {
                    isThere = true;
                }
            }
            if (!isThere)
            {
                test.GetComponentInChildren<TextMesh>().text = alpha[i].ToString();
                break;
            }
        }

    }
    
}
