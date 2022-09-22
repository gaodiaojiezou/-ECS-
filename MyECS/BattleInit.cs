using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.EcsSystem;
using ECS.Component;
public class BattleInit : MonoBehaviour
{
    private World currentWorld;
    public Transform[] entityCreator;
    public Transform enemyParent;
    public Transform damageParent;
    public Transform UIParent;
    private void Awake()
    {
        Locator.battle = this;
        Locator.pool = new ResourcePool();
        CreateGame();
        PlayerEntityManager playerEntity = new PlayerEntityManager();
        InitPlayerEntity();
        InitEnemyEntity();
    }
    private void InitPlayerEntity() 
    {
        PlayerEntityManager playerEntity = new PlayerEntityManager();
        playerEntity.initialize();
    }
    private void InitEnemyEntity()
    {
        Locator.enemyManager = new EnemyEntityManager();
        Locator.enemyManager.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        Locator.world.Update(Time.deltaTime);
        Locator.enemyManager.Update(Time.deltaTime);
    }
    void LateUpdate() 
    {
       Locator.world.LateUpdate(Time.deltaTime); 
    }

    /// <summary>
    /// 创建游戏
    /// </summary>
    public void CreateGame()
    {
        currentWorld = new World();
        currentWorld.initialize();
        Locator.world = currentWorld;
        Locator.systemFunction  = new SystemFunction();
        MoveSystem moveSystem = new MoveSystem();
        currentWorld.AddSystem(moveSystem);
        FsmStateSystem fsmStateSystem = new FsmStateSystem();
        currentWorld.AddSystem(fsmStateSystem);
        TimerSystem timerSystem = new TimerSystem();
        currentWorld.AddSystem(timerSystem);
        DeadSystem deadSystem =new DeadSystem();
        currentWorld.AddSystem(deadSystem);
    }




}
