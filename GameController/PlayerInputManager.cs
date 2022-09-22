using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inputClickEffect;
    private GameObject EffectParents;
    private Vector3 leftClickPos;
    private PlayerInputComponent playerInputC;
    private void Start() 
    {
        EffectParents = GameObject.Find("Effects");
        playerInputC = Locator.world.GetCommonComponent(EcsConst.ComponentConst.INPUT) as PlayerInputComponent;
    }
    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
           //产生特效
           leftClickPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
           GameObject Effect = Instantiate(inputClickEffect);
           Effect.transform.position = leftClickPos;
           Effect.transform.SetParent(EffectParents.transform);
           Destroy(Effect,0.5f);
           //记录位置
           Vector2 pos = Camera.main.ScreenToWorldPoint(leftClickPos);
           playerInputC.aimPositon = pos;
           playerInputC.SkillInput.Enqueue("move");
        }
        //技能操作录入
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           playerInputC.SkillInput.Enqueue(EcsConst.SkillID.Skill1);
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
           playerInputC.SkillInput.Enqueue(EcsConst.SkillID.Skill2);
        }else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
           playerInputC.SkillInput.Enqueue(EcsConst.SkillID.Skill3);
        }

    }
}
