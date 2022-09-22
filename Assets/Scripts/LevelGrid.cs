using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGrid : MonoBehaviour
{
    [SerializeField] private GameObject plane;

    [SerializeField, Range( 5, 30 )] private int gridWidth;
    [SerializeField, Range( 5, 30 )] private int gridHeight;

    public int GridWidth => gridWidth;
    public int GridHeight => gridHeight;

    private const float CellSize = 1.1f;

    private void Start()
    {
        UpdateSize();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        UpdateSize();
        #endif
    }

    public Vector3 GetPositionByGridCell( int x, int y )
    {
        Vector3 localPosition = new Vector3( 
            x - (float)gridWidth / 2 + CellSize / 2, 0,
            y - (float)gridHeight / 2 + CellSize / 2 );
        localPosition *= CellSize;
        return transform.position + localPosition;
    }

    private void UpdateSize() =>
        transform.localScale = new Vector3( gridWidth, 1, gridHeight ) * CellSize;
}