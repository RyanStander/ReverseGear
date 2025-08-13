using System;
using UnityEngine;

namespace Car
{
    [Serializable]
    public struct Wheel
    {
        public GameObject WheelModel;
        public WheelCollider WheelCollider;
        public Axel Axel;
    }
}
