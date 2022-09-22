using System;
using UnityEngine;
using Zenject;

namespace Managers
{
public class LevelManager : MonoBehaviour
{
    [SerializeField] private PoolFactory sphereFactory;
    [SerializeField] private PoolFactory capsuleFactory;

    [Inject] private LevelGrid _levelGrid;


    private void Start()
    {
        int height = _levelGrid.GridHeight;
        int width = _levelGrid.GridWidth;

        for ( int i = 0; i < width; i++ )
        {
            for ( int j = 0; j < height; j++ )
            {
                sphereFactory.NextInstance( transform, _levelGrid.GetPositionByGridCell( i, j ), Quaternion.identity );
            }
        }
    }
}
}