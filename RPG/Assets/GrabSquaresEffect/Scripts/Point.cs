using UnityEngine;

namespace Assets.MoveableLineRenderer.Scripts
{
    internal sealed class Point
    {
        public Vector3 Position;
        private readonly float _timeCreated;

        public Point(Vector3 position)
        {
            Position = position;
            _timeCreated = Time.time;
        }

        public float TimeAlive
        {
            get { return Time.time - _timeCreated; }
        }
    }
}