using System;
using System.Collections;
using System.Collections.Generic;
using Code.Infrastructure;
using Code.Obsticles;
using UnityEngine;

public class OutboundDisabler : MonoBehaviour
{
    private ICameraService _cameraService;
    
    private IPooled _pooled;

    public void Construct(ICameraService cameraService)
    {
        _cameraService = cameraService;
    }
    
    private void Awake()
    {
        _pooled = GetComponent<IPooled>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _cameraService.HalfHorizontalSize * -2)
            return;
        
        if(_pooled != null)
            _pooled.Return();
        else
            Destroy(this);
    }
}
