using UnityEngine;
using System.Collections.Generic;
using Controller;

public class DynamicAreaController : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh dynamicAreaMesh;

    // Reference to your enemies (use a List instead of an array)
    public List<GameObject> enemies;

    // The distance to expand the area from enemies
    public float expansionDistance = 1.0f;

    void Start()
    {
        enemies = EnemyController.Instance.enemyList;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        dynamicAreaMesh = new Mesh();

        GenerateDynamicAreaMesh();
    }

    void Update()
    {
        GenerateDynamicAreaMesh();
    }

    void GenerateDynamicAreaMesh()
    {
        // Calculate vertices, triangles, and other mesh data based on enemy positions
        Vector3[] vertices = CalculateVertices();
        int[] triangles = CalculateTriangles(vertices);

        // Assign the mesh data to the dynamicAreaMesh
        dynamicAreaMesh.vertices = vertices;
        dynamicAreaMesh.triangles = triangles;

        // Calculate normals (you might need to recalculate normals based on your mesh)

        //dynamicAreaMesh.vertices = vertices.ToArray();
        //dynamicAreaMesh.RecalculateNormals();
        //dynamicAreaMesh.RecalculateBounds();

        // Assign the mesh to the MeshFilter
        meshFilter.mesh = dynamicAreaMesh;
    }

    Vector3[] CalculateVertices()
    {
        // Calculate vertices based on enemy positions
        int vertexCount = enemies.Count * 5; // Five vertices for each enemy
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 enemyPos = enemies[i].transform.position;

            // Calculate the vertices for a pentagon around each enemy
            float x = enemyPos.x;
            float z = enemyPos.z;

            // Vertices order: center, top, bottom left, bottom right, top left, top right
            vertices[i * 5] = new Vector3(x, 0.1f, z);
            vertices[i * 5 + 1] = new Vector3(x, 0.1f, z + expansionDistance);
            vertices[i * 5 + 2] = new Vector3(x - expansionDistance, 0.1f, z - expansionDistance);
            vertices[i * 5 + 3] = new Vector3(x + expansionDistance, 0.1f, z - expansionDistance);
            vertices[i * 5 + 4] = new Vector3(x - expansionDistance, 0.1f, z + expansionDistance);
        }

        return vertices;
    }

    int[] CalculateTriangles(Vector3[] vertices)
    {
        // Calculate triangles based on a grid pattern around the enemies

        int vertexCount = vertices.Length;

        // Ensure we have enough vertices to create triangles
        if (vertexCount < 6)
        {
            return new int[0]; // Not enough vertices for triangles
        }

        int gridSize = Mathf.FloorToInt(Mathf.Sqrt(vertexCount)); // Grid size along one axis

        List<int> trianglesList = new List<int>();

        // Create triangles within the grid
        for (int row = 0; row < gridSize - 1; row++)
        {
            for (int col = 0; col < gridSize - 1; col++)
            {
                int center = row * gridSize + col;
                int top = center + 1;
                int bottomLeft = (row + 1) * gridSize + col;
                int bottomRight = bottomLeft + 1;
                int topLeft = row * gridSize + col + gridSize;
                int topRight = topLeft + 1;

                // Define triangles for each grid cell with clockwise winding order
                trianglesList.Add(center);
                trianglesList.Add(bottomLeft);
                trianglesList.Add(topLeft);

                trianglesList.Add(center);
                trianglesList.Add(bottomRight);
                trianglesList.Add(bottomLeft);

                trianglesList.Add(center);
                trianglesList.Add(top);
                trianglesList.Add(bottomRight);

                trianglesList.Add(center);
                trianglesList.Add(topRight);
                trianglesList.Add(top);

                trianglesList.Add(center);
                trianglesList.Add(topLeft);
                trianglesList.Add(topRight);

                trianglesList.Add(center);
                trianglesList.Add(bottomLeft);
                trianglesList.Add(topLeft);
            }
        }

        return trianglesList.ToArray();
    }

}