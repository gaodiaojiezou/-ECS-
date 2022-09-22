using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
using ECS.EcsEntity;
using ECS.EcsSystem;
using SunShineAudio;
public static class Locator 
{
    public static EntityManager entityManager;
    public static World world;
    public static BattleInit battle;
    public static ResourceLoader ResLoader;
    public static ActionManager action;
    public static GameActionFunction actionFunc;
    public static SystemFunction systemFunction;
    public static ResourcePool pool;
    public static EnemyEntityManager enemyManager;
    public static SunShineAudioPlayer audioPlayer;
}
