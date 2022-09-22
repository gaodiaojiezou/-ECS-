using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
namespace ECS.EcsEntity
{
    public class PlayerEntityManager 
    {
        public Dictionary<string,PlayerPropertyItem> playerDataDict;
        public Dictionary<string,AbilityItem> AbilityDataDict;
        public void initialize()
        {
            PlayerPropertyData playerPropertyData = Locator.ResLoader.LoadResource<PlayerPropertyData>("Assets/ScriptableData/playerPropertyData.asset");
            playerDataDict = new Dictionary<string, PlayerPropertyItem>();
            for (int i = 0; i < playerPropertyData.playerItems.Length; i++)
            {
                playerDataDict.Add(playerPropertyData.playerItems[i].name,playerPropertyData.playerItems[i]);
            }
            AbilityData abilityData = Locator.ResLoader.LoadResource<AbilityData>("Assets/ScriptableData/AbilityData.asset");
            AbilityDataDict = new Dictionary<string, AbilityItem>();
            for (int i = 0; i < abilityData.AbilityItems.Length; i++)
            {
                AbilityDataDict.Add(abilityData.AbilityItems[i].AbilityID,abilityData.AbilityItems[i]);
            }            
            CreatePlayerEntity();
        }
        /// <summary>
        /// 初始化玩家Entity实例
        /// </summary>
        public  void CreatePlayerEntity()
        {
            Locator.entityManager.playerEntity = new Entity();
            GameObject gameObject  = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/EntityObj/PlayerAnimator.prefab");
            GameObject playerObj = GameObject.Instantiate(gameObject);
            playerObj.SetActive(false);
            Entity entity =  Locator.entityManager.playerEntity;  
            playerDataDict.TryGetValue("女武神",out PlayerPropertyItem item);
            TransformComponent transformC = new TransformComponent()
            {
                position = playerObj.transform.position,
                speed = item.moveSpeed,
            };
            Locator.entityManager.AddEntityComponent(entity, transformC);
            PropertyComponent PropertyC = new PropertyComponent()
            {
                hp         = item.hp,
                maxHp      = item.hp,
                attack     = item.attack,
                attackTime = item.attackTime,
            };
            Locator.entityManager.AddEntityComponent(entity, PropertyC);  
            UnityInstanceComponent unityInstanceC = new UnityInstanceComponent()
            {
                m_Trans = playerObj.transform,
            };
            unityInstanceC.Initialize();
            Locator.entityManager.AddEntityComponent(entity, unityInstanceC);
            AbilityComponent abilityComponent = new AbilityComponent();
            Ability skill1 =  GetAbilityComponent(item.Skill1ID);
            abilityComponent.abilityDict.Add(skill1.AbilityID,skill1);
            Ability skill2 =  GetAbilityComponent(item.Skill2ID);
            abilityComponent.abilityDict.Add(skill2.AbilityID,skill2);
            Ability skill3 =  GetAbilityComponent(item.Skill3ID);
            abilityComponent.abilityDict.Add(skill3.AbilityID,skill3);                        
            Locator.entityManager.AddEntityComponent(entity, abilityComponent);
            BehaviourStateComponent behaviourStateC = new BehaviourStateComponent()
            {
                 state = EcsConst.EntityState.Init,
                 battleState = EcsConst.EntityBattleState.Idle,   
            };
            Locator.entityManager.AddEntityComponent(entity,behaviourStateC);

                 
        }
        private Ability GetAbilityComponent(string AbilityID)
        {
            AbilityItem abilityItem = AbilityDataDict[AbilityID];
            Ability abilityC = new Ability()
            {
                type = EcsConst.AbilityType.Skill,
                AbilityID = abilityItem.AbilityID,
                CdCoolNum = abilityItem.AbilityCD,
                CurrentCd = 0
            };
            abilityC.actions = new Dictionary<EcsConst.ABilityActionType, ABilityAction>();
            if(abilityItem.isHaveChangeHpAction == true)
            {
                ChangeHpAction changeHpAction =  new ChangeHpAction();
                changeHpAction.actionType = abilityItem.changeHpData.actionType;
                changeHpAction.camp = abilityItem.changeHpData.camp;
                changeHpAction.AreaType = abilityItem.changeHpData.AreaType;
                changeHpAction.availDistance = abilityItem.changeHpData.availDistance;
                changeHpAction.HpNum = abilityItem.changeHpData.HpNum;
                abilityC.actions.Add(EcsConst.ABilityActionType.ChangeHp,changeHpAction);
            }
            if(abilityItem.isHaveChangePropertyAction == true)
            {
                ChangePropertyAction changePropertyAction = new ChangePropertyAction();
                changePropertyAction.actionType = abilityItem.changePropertyData.actionType;
                changePropertyAction.camp = abilityItem.changePropertyData.camp;
                changePropertyAction.availTime = abilityItem.changePropertyData.availTime;
                changePropertyAction.changePropertyType = abilityItem.changePropertyData.changePropertyType;
                changePropertyAction.propertyNum = abilityItem.changePropertyData.propertyNum;   
                abilityC.actions.Add(EcsConst.ABilityActionType.ChangeProperty,changePropertyAction);
            }
            if(abilityItem.isHaveChangePostionAction == true)
            {
                ChangePostionAction changePostionAction = new ChangePostionAction();
                changePostionAction.actionType = abilityItem.changePostionData.actionType;
                changePostionAction.camp = abilityItem.changePostionData.camp;
                changePostionAction.distanceNum = abilityItem.changePostionData.distanceNum;
                abilityC.actions.Add(EcsConst.ABilityActionType.ChangePostion,changePostionAction);
            } 
            return  abilityC;         
        }
    }
}