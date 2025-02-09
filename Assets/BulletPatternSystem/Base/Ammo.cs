using UnityEngine;

public class Ammo : MonoBehaviour
{
    public IAction[] patterns { get; private set; }
    int currentIndex = 0;

    ActionTypes currentActionType;
    TransformAction currentTransformAction;
    DelayAction currentDelayAction;
    SplitAction currentSplitAction;

    float currentActionTimer = 0;

    public void Setup(IAction[] patterns)
    {
        this.patterns = patterns;

        GetNextAction();

        ReadyAction();

        currentActionTimer = 0;
    }

    void Update()
    {
        if (patterns == null || patterns.Length == 0)
        {
            //Debug.LogWarning("No Pattern Set", this);
            return;
        }

        if (DoAction())
        {
            EndAction();

            GetNextAction();

            ReadyAction();

            currentActionTimer = 0;
        }

        currentActionTimer += Time.deltaTime;
    }

    void GetNextAction()
    {
        switch (patterns[currentIndex])
        {
            case TransformAction action:
                currentTransformAction = action;
                currentActionType = ActionTypes.TransformAction;
                break;
            case DelayAction action:
                currentDelayAction = action;
                currentActionType = ActionTypes.DelayAction;
                break;
            case SplitAction action:
                currentSplitAction = action;
                currentActionType = ActionTypes.SplitAction;
                break;
        }

        if (++currentIndex == patterns.Length) currentIndex = 0;
    }

    void ReadyAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.ReadyAction(transform);
                return;
            case ActionTypes.DelayAction:
                currentDelayAction.ReadyAction();
                return;
            case ActionTypes.SplitAction:
                currentSplitAction.ReadyAction(this);
                return;
        }
    }
    bool DoAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.DoAction(Time.deltaTime);
                return currentActionTimer >= currentTransformAction.Duration;
            case ActionTypes.DelayAction:
                currentDelayAction.DoAction();
                return currentActionTimer >= currentDelayAction.Duration;
            case ActionTypes.SplitAction:
                currentSplitAction.DoAction();
                return true;
            default:
                throw new System.Exception("Not Implemented");
        }
    }
    void EndAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.EndAction();
                return;
            case ActionTypes.DelayAction:
                currentDelayAction.EndAction();
                return;
            case ActionTypes.SplitAction:
                currentSplitAction.EndAction();
                return;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}