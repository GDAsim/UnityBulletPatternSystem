using UnityEngine;

public class Ammo : MonoBehaviour
{
    TransformAction[] patterns;
    int currentIndex = 0;

    TransformAction currentAction;
    float moveTimer = 0;

    public void Setup(TransformAction[] patterns)
    {
        this.patterns = patterns;
    }

    void Update()
    {
        if (patterns.Length == 0)
        {
            Debug.LogWarning("No Pattern Set", this);
            return;
        }

        var dt = Time.deltaTime;

        if (moveTimer <= 0)
        {
            currentAction.EndAction();

            currentAction = patterns[currentIndex];
            currentAction.GetReady(transform);
            moveTimer = currentAction.Duration;

            currentIndex++;
            if(currentIndex == patterns.Length) currentIndex = 0;
        }

        currentAction.DoTransformAction(dt);
        moveTimer -= dt;
    }
}