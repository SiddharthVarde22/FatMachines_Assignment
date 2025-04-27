using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private static Grid s_instance;

    [SerializeField]
    Transform m_gridObject;
    [SerializeField]
    Vector2 m_cellSize;
    [SerializeField]
    int m_gridSizeX = 1, m_gridSizeY = 1;
    [SerializeField]
    Vector3 m_startPosition;
    [SerializeField]
    Vector3 m_cameraPos;
    [SerializeField]
    float m_cameraFov = 60;

    Vector3[,] m_gridPositions;

    private void Awake()
    {
        s_instance = this;
        Initialize();
    }
    private void OnDestroy()
    {
        s_instance = null;
    }
    private void Initialize()
    {
        GenerateGrid();
        m_gridObject.localScale = new Vector3(m_gridSizeX, 1, m_gridSizeY);
        m_gridObject.localPosition = new Vector3((float)m_gridSizeX / 2, 0, (float)m_gridSizeY / 2);
        m_gridObject.GetComponent<MeshRenderer>().materials[1].mainTextureScale = new Vector2((float)m_gridSizeY / 2, (float)m_gridSizeX / 2);
        GameManager.SetCameraPositionAndFov(m_cameraPos, m_cameraFov);
    }
    private void GenerateGrid()
    {
        m_gridPositions = new Vector3[m_gridSizeX, m_gridSizeY];
        Vector3 l_offSet;
        for(int i = 0; i < m_gridSizeX; i++)
        {
            for(int j = 0; j < m_gridSizeY; j++)
            {
                l_offSet = m_startPosition;
                l_offSet.x = i * m_cellSize.x;
                l_offSet.z = j * m_cellSize.y;
                m_gridPositions[i, j] = m_startPosition + l_offSet;
            }
        }
    }
    public static Vector3 FindNearestPositionOnGrid(Vector3 a_position)
    {
        float l_distance = Mathf.Infinity;
        int l_xIndex = 0, l_zIndex = 0;
        for(int i = 0; i < s_instance.m_gridSizeX; i++)
        {
            if(Mathf.Abs(s_instance.m_gridPositions[i,0].x - a_position.x) < l_distance)
            {
                l_xIndex = i;
                l_distance = Mathf.Abs(s_instance.m_gridPositions[i, 0].x - a_position.x);
            }
        }
        l_distance = Mathf.Infinity;
        for(int j = 0; j < s_instance.m_gridSizeY; j++)
        {
            if (Mathf.Abs(s_instance.m_gridPositions[0, j].z - a_position.z) < l_distance)
            {
                l_zIndex = j;
                l_distance = Mathf.Abs(s_instance.m_gridPositions[0, j].z - a_position.z);
            }
        }

        return s_instance.m_gridPositions[l_xIndex, l_zIndex];
    }
}
