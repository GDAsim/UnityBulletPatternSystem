using UnityEngine;

public class Ammo : MonoBehaviour
{
    TransformAction[] patterns;
    int currentIndex = 0;

    TransformAction currentAction;
    float actionTimer = 1;

    public void Setup(TransformAction[] patterns)
    {
        this.patterns = patterns;
    }

    void Update()
    {
        if (patterns == null || patterns.Length == 0)
        {
            Debug.LogWarning("No Pattern Set", this);
            return;
        }

        var dt = Time.deltaTime;

        if (actionTimer <= 0)
        {
            currentAction.EndAction();

            currentAction = patterns[currentIndex];
            currentAction.GetReady(transform);
            actionTimer = currentAction.Duration;

            currentIndex++;
            if(currentIndex == patterns.Length) currentIndex = 0;
        }

        currentAction.DoTransformAction(dt);
        actionTimer -= dt;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}