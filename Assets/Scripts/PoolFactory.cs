using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory has pool of pre-instantiated objects
/// for building level at runtime.
/// It reuses the oldest objects, keeping them in queue.
/// </summary>
public class PoolFactory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Range( 1, 1024 )] private int poolSize = 128;

    private readonly Queue<GameObject> _poolQueue = new Queue<GameObject>();

    #region Pool initialization

    private void Awake()
    {
        InitializePoolQueue();
    }

    private void InitializePoolQueue()
    {
        for ( int i = 0; i < poolSize; i++ )
        {
            GameObject obj = Instantiate( prefab );
            obj.SetActive( false );
            _poolQueue.Enqueue( obj );
        }
    }

    #endregion


    /// <summary> Position and rotation args are treated as LOCAL </summary>
    public GameObject NextInstance( Transform parentTransform, Vector3 position, Quaternion rotation )
    {
        GameObject instance = _poolQueue.Dequeue();
        _poolQueue.Enqueue( instance );
        instance.SetActive( true );
        instance.transform.parent = parentTransform;
        instance.transform.localPosition = position;
        instance.transform.localRotation = rotation;
        return instance;
    }
}