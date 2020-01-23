using System;
using UnityEngine;

namespace StefTools
{
    public interface IWorker
    {
        void MoveTo(Vector3 position, Action onArrival);
    }
}
