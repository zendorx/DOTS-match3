﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct AssignCurrentWaypointTag : IComponentData
    {
        public Entity next;
    }
}