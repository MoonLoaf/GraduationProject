using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Utility
{
    public class LevelSpline : GenericSingletonDOL<LevelSpline>
    { 
        [SerializeField] private SplineContainer _levelSplineContainer;
        [SerializeField] private float _trackWidth;
        [SerializeField] private float _mapHorizontalBorder;
        private float _splineLength;
        
        private float3 _nearestPoint;
        public float TrackWidth => _trackWidth;

        public float SplineLength => _splineLength;

        private void Start()
        {
            _splineLength = _levelSplineContainer.CalculateLength();
        }

        public Spline GetLevelSpline()
        {
            return _levelSplineContainer.Spline;
        }

        public SplineContainer GetLevelSplineContainer()
        {
            return _levelSplineContainer;
        }
        
        public Vector3 GetStartPositionWorldSpace()
        {
            if (_levelSplineContainer != null)
            {
                Vector3 localStartPosition = _levelSplineContainer.Spline[0].Position;
                return _levelSplineContainer.transform.TransformPoint(localStartPosition);
            }
            Debug.LogWarning("Level spline container or spline is not set.");
            return Vector3.zero;
        }

        public bool CanPlace(Vector2 position, float towerRadius)
        {
            Spline spline = _levelSplineContainer.Spline;

            Vector3 pos3 = new Vector3(position.x, position.y, spline[0].Position.z);

            SplineUtility.GetNearestPoint(spline, pos3, out _nearestPoint, out _);

            float distanceToPosition = Vector2.Distance(position, new Vector2(_nearestPoint.x, _nearestPoint.y)) - towerRadius;

            return distanceToPosition > _trackWidth && position.x < _mapHorizontalBorder;
        }


        private void OnDrawGizmos()
        {
            if (_levelSplineContainer == null || _levelSplineContainer.Spline == null)
                return;

            Spline spline = _levelSplineContainer.Spline;
            Gizmos.color = Color.red;

            // Draw lines representing the track width around each spline point
            for (int i = 0; i < spline.Count; i++)
            {
                Vector3 localSplinePoint = spline[i].Position;
                Vector3 worldSplinePoint = _levelSplineContainer.transform.TransformPoint(localSplinePoint);

                // Draw a circle at each spline point to represent the track width
                Gizmos.DrawWireSphere(worldSplinePoint, _trackWidth);
            }
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_nearestPoint, 0.1f);
        }
    }
}
