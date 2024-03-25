using UnityEngine;
using UnityEngine.Splines;

public class LevelSpline : GenericSingleton<LevelSpline>
{
    [SerializeField]private SplineContainer levelSplineContainer;
    
    public Spline GetLevelSpline()
    {
        return levelSplineContainer.Spline;
    }

    public SplineContainer GetLevelSplineContainer()
    {
        return levelSplineContainer;
    }
}
