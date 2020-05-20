using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//6x4
// 0 1 2 3 4 5
// 6 7 8 9 10 11
// 12 13 14 15 16 17
// 18 19 20 21 22 23


public class FallDetectSystem : MySystem
{
    private int width;
    private int height;

    public int GetIndex(int x, int y)
    {
        return x + width * y;
    }

    public int2 GetCoord(int index)
    {
        return new int2(index % width, index / width);
    }
    
    protected override void OnCreate()
    {
        base.OnCreate();
        //RequireSingletonForUpdate<FieldComponent>();
    }

    // i = x + width*y;
    // x = i % width;
    // y = i / width;
    
    
    
    protected override void OnUpdate()
    {
        var cb = createCommandBuffer();
        
        EntityQuery eqCells = GetEntityQuery(ComponentType.ReadWrite<JewelCell>());
        EntityQuery eqField = GetEntityQuery(ComponentType.ReadOnly<FieldComponent>());
        
        FieldComponent fieldComponent = eqField.GetSingleton<FieldComponent>();
        width = fieldComponent.width;
        height = fieldComponent.height;

        using (var cells = eqCells.ToEntityArray(Allocator.TempJob))
        {
            NativeArray<Entity> cellsMatrix = new NativeArray<Entity>(fieldComponent.width * fieldComponent.height, Allocator.TempJob);

            //fill matrix
            foreach (var entity in cells)
            {
                JewelCell cell = EntityManager.GetComponentData<JewelCell>(entity);

                int index = GetIndex(cell.x, cell.y);
                cellsMatrix[index] = entity;
            }

            for (int x = 0; x < width; x++)
            {
                int emptyY = -1;
                
                for (int y = height - 1; y >= 0; y--)
                {
                    int index = GetIndex(x, y);

                    if (emptyY == -1 && cellsMatrix[index] == Entity.Null)
                    {
                        emptyY = y;
                    }

                    if (cellsMatrix[index] != Entity.Null && emptyY != -1)
                    {
                        Entity e = cellsMatrix[index];
                        JewelCell cell = EntityManager.GetComponentData<JewelCell>(e);

                        cb.SetComponent(e, new JewelCell
                        {
                            x = cell.x,
                            y = emptyY
                        });
                        
                        cb.AddComponent(e, new FallComponent
                        {
                            position = SpawnerMono.IndexToPosition(cell.x, emptyY),
                            speed = 10
                        });
                        
                        emptyY--;
                    }
                }

                if (emptyY < height - 1 && emptyY != -1)
                {
                    for (int sy = emptyY; sy >= 0; sy--)
                    {
                        var entity = cb.CreateEntity();
                        cb.AddComponent(entity, new SpawnNeedComponent
                        {
                            x = x,
                            y = sy
                        });
                    }
                }
                
                
            }



            cellsMatrix.Dispose();
        }
        
        
    }
}
