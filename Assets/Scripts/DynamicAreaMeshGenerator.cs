using UnityEngine;
using System.Collections.Generic;
using Controller;

public class DynamicAreaMeshGenerator : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh dynamicAreaMesh;

    // Reference to your enemies (use a List instead of an array)
    public List<GameObject> enemies;

    // The distance to expand the area from enemies
    public float expansionDistance = 1.0f;

    // Number of vertices per enemy
    private int verticesPerEnemy = 16;

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
        int vertexCount = enemies.Count * verticesPerEnemy; // 16 vertices for each enemy
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 enemyPos = enemies[i].transform.position;

            // Calculate the vertices for a polygon around each enemy
            for (int j = 0; j < verticesPerEnemy; j++)
            {
                float angle = 2 * Mathf.PI * j / verticesPerEnemy;
                float x = enemyPos.x + Mathf.Cos(angle) * expansionDistance;
                float z = enemyPos.z + Mathf.Sin(angle) * expansionDistance;
                vertices[i * verticesPerEnemy + j] = new Vector3(x, 0.1f, z);
            }
        }

        return vertices;
    }

    int[] CalculateTriangles(Vector3[] vertices)
    {
        int vertexCount = vertices.Length;
        int enemyCount = vertexCount / verticesPerEnemy;

        // Ensure we have enough vertices to create triangles
        if (vertexCount < 3)
        {
            return new int[0]; // Not enough vertices for triangles
        }

        List<int> trianglesList = new List<int>();

        // Create triangles connecting vertices of different enemies
        for (int i = 0; i < enemyCount; i++)
        {
            for (int j = 0; j < verticesPerEnemy; j++)
            {
                int currentVertex = i * verticesPerEnemy + j;
                int nextVertex = i * verticesPerEnemy + (j + 1) % verticesPerEnemy;

                int nextEnemyVertex = ((i + 1) % enemyCount) * verticesPerEnemy + j;
                int nextEnemyNextVertex = ((i + 1) % enemyCount) * verticesPerEnemy + (j + 1) % verticesPerEnemy;

                trianglesList.Add(currentVertex);
                trianglesList.Add(nextVertex);
                trianglesList.Add(nextEnemyVertex);

                trianglesList.Add(nextEnemyVertex);
                trianglesList.Add(nextVertex);
                trianglesList.Add(nextEnemyNextVertex);
            }
        }

        return trianglesList.ToArray();
    }







}
