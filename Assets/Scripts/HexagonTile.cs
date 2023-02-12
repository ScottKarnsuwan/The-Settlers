using UnityEngine;


public class HexagonTile : MonoBehaviour
{
    private float radius = 4;
    private MeshFilter filter;
    private Mesh mesh;
    private Quaternion rotation = Quaternion.identity;
    private Vector3 scale = Vector3.one;
    private int rowCount = 0;
    private int rowLength = 3;
    private int totalClones = 18;


    private void Awake()
    {
        CreateNewHexagon();
        CreateRowOfClones();
    }
    private void CreateRowOfClones()
    {
        rowCount++;
        if (rowCount > 5)
        {
            return;
        }

        float offset = 0;
        if (rowLength == 4)
        {
            offset = radius;
        }
        else if (rowLength == 5)
        {
            offset = radius * 2;
        }

        for (int i = 0; i < rowLength; i++)
        {
            if (totalClones <= 0)
            {
                return;
            }
            GameObject clone = Instantiate(gameObject, transform.position + new Vector3(radius * Mathf.Sqrt(3), radius * 1.5f * rowCount, 0), Quaternion.identity);
            totalClones--;
        }

        if (rowLength == 3)
        {
            rowLength = 4;
        }
        else if (rowLength == 4)
        {
            rowLength = 5;
        }
        else
        {
            rowLength = 3;
        }

        CreateRowOfClones();
        
    }




private void CreateNewHexagon()
    {
        filter = GetComponent<MeshFilter>();
        if (filter == null)
        {
            filter = gameObject.AddComponent<MeshFilter>();
        }
       
        transform.eulerAngles = new Vector3(0, 0, 90);
        transform.localScale = scale;


        mesh = new Mesh();
        filter.mesh = mesh;

        Vector3[] vertices = new Vector3[7];
        int[] triangles = new int[18];

        vertices[6] = Vector3.zero;

        //creating the triangles
        for (int i = 0; i < 6; i++)
        {
            float angle = 2 * Mathf.PI / 6 * i;
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);

            triangles[3 * i] = i;
            triangles[3 * i + 1] = (i + 1) % 6;
            triangles[3 * i + 2] = 6;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        //setting material for mesh
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        //check if shader is available in the project
        Shader diffuseShader = Shader.Find("Diffuse");
        if (diffuseShader != null)
        {
            renderer.material = new Material(diffuseShader);
        }
        else
        {
            Debug.LogError("Diffuse shader not found");
        }
    }
}
