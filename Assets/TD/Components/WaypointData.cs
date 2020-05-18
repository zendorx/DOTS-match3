﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct WaypointData : IComponentData
    {
        public Entity next;
    }
}