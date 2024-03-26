using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace Utility
{
    public class LevelSpline : GenericSingleton<LevelSpline>
    { 
        [SerializeField]private SplineContainer _levelSplineContainer;
    
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
    }
}
