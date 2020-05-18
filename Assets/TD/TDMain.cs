using System;
using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Entities;
using UnityEngine;

public class TDMain : MonoBehaviour
{
    static public  TDMain instance;
    public GameObject UnitPrefab;

    public Entity unitEntity;

    //private Entity UnitEntity; 
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //UnityEntity = ConvertToEntity(UnitPrefab);
        
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        var world = World.DefaultGameObjectInjectionWorld;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, null);
        unitEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(UnitPrefab, settings);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
