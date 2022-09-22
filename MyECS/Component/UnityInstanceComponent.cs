using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ECS.Component
{
    /// <summary>
    /// 负责保存Unity组件实例的Ecs组件
    /// </summary>
    public class UnityInstanceComponent : IComponentData
    {
       public string name => EcsConst.ComponentConst.UNITYINSTANCE;
       public void Initialize()
       {
         m_Animator = m_Trans.GetComponent<Animator>();
         m_Rigidbody2D = m_Trans.GetComponent<Rigidbody2D>();
         Transform hpTransform = m_Trans.Find("Canvas/HP"); 
         if (hpTransform != null)
         {
          hpSlider  =hpTransform.GetComponent<Slider>();
         }
         Transform damageTransform = m_Trans.Find("Canvas/Damage");
         if (damageTransform != null)
         {
          showDamage = damageTransform.GetComponent<ShowDamage>();
         }

       }
       /// <summary>
       /// Entity组件自己的Transform实例
       /// </summary>
       public Transform m_Trans;
       /// <summary>
       /// Entity组件目标的Transform实例
       /// </summary>
       public Transform aim_Trans; 
       /// <summary>
       /// Entity组件目标的Animator实例
       /// </summary>
       public Animator m_Animator;   
       public Rigidbody2D m_Rigidbody2D;
       public Slider hpSlider;
       public ShowDamage showDamage;

    }
}