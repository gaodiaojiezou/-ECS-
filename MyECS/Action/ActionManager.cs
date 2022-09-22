using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public Queue<PerformanceAction> performanceActions;
    public Queue<DataAction> dataActions;
    private GameActionFunction gameActionFunction;
    public void Start()
    {
        Locator.action  = this;
        performanceActions = new Queue<PerformanceAction>();
        dataActions = new Queue<DataAction>();
        gameActionFunction = new GameActionFunction();
        Locator.actionFunc = gameActionFunction;
    }
    public  void Update() 
    {
       UpdatePerformanceActions();
       UpdateDataActions();
    }
    private void UpdatePerformanceActions()
    {
       Queue<PerformanceAction> Actions  = new Queue<PerformanceAction>();
       foreach (var item in performanceActions)
       {
         Actions.Enqueue(item);
       }
       performanceActions.Clear();
       foreach (var item in Actions)
       {
         StartCoroutine(item.StartAction());
       }
    }
    private void UpdateDataActions()
    {
       Queue<DataAction> Actions  = new Queue<DataAction>();
       foreach (var item in dataActions)
       {
         Actions.Enqueue(item);
       }
       dataActions.Clear();
       foreach (var item in Actions)
       {
         StartCoroutine(item.StartAction());
       }
    }    
}
