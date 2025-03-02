using UnityEngine;

public class Ammo : MonoBehaviour
{
    public IAction[] Patterns { get; private set; }
    int currentIndex;

    ActionTypes currentActionType;
    TransformAction currentTransformAction;
    DelayAction currentDelayAction;
    SplitAction currentSplitAction;

    float currentActionTimer;

    public void Setup(Ammo ammo)
    {
        Patterns = ammo.Patterns;

        currentIndex = ammo.currentIndex;
        currentActionTimer = ammo.currentActionTimer;

        GetNextAction();

        ReadyAction();
    }
    public void Setup(IAction[] patterns)
    {
        Patterns = patterns;

        currentIndex = 0;
        currentActionTimer = 0;

        GetNextAction();

        ReadyAction();
    }

    void Update()
    {
        if (Patterns == null || Patterns.Length == 0) return;

        currentActionTimer += Time.deltaTime;

        if (DoAction())
        {
            EndAction();

            GetNextAction();

            ReadyAction();

            currentActionTimer = 0;
        }
    }

    void GetNextAction()
    {
        switch (Patterns[currentIndex])
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

        if (++currentIndex == Patterns.Length) currentIndex = 0;
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