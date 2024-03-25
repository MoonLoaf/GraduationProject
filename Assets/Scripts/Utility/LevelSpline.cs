using UnityEngine.Splines;

public class LevelSpline : GenericSingleton<LevelSpline>
{
    private SplineContainer levelSplineContainer;

    protected override void Awake()
    {
        base.Awake();
        levelSplineContainer = FindObjectOfType<SplineContainer>();
    }

    public Spline GetLevelSpline()
    {
        return levelSplineContainer.Spline;
    }

    public SplineContainer GetLevelSplineContainer()
    {
        return levelSplineContainer;
    }
}
