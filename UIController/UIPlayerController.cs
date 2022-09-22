using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECS.EcsEntity;
using ECS.Component;
using TMPro;
[RequireComponent(typeof(BindingData))]
public class UIPlayerController : MonoBehaviour
{
    private BindingData binding ;
    private GameObject cD1Obj;
    private GameObject cD2Obj;
    private GameObject cD3Obj;
    private Image  cD1Image;
    private Image  cD2Image;
    private Image  cD3Image;
    private TextMeshProUGUI   cD1Text;
    private TextMeshProUGUI   cD2Text;
    private TextMeshProUGUI   cD3Text;  
    private TextMeshProUGUI   hpText; 
    private Ability Skill1;
    private Ability Skill2;
    private Ability Skill3;
    private Slider hpSlider;
    private PropertyComponent propertyC;
    private void Start() 
    {
        binding = this.GetComponent<BindingData>();
        cD1Obj = binding.GetObject("CD1Obj") as GameObject;
        cD2Obj = binding.GetObject("CD2Obj") as GameObject;
        cD3Obj = binding.GetObject("CD3Obj") as GameObject;
        cD1Image = binding.GetObject("CD1Image") as Image;
        cD2Image = binding.GetObject("CD2Image") as Image;
        cD3Image = binding.GetObject("CD3Image") as Image;
        cD1Text = binding.GetObject("CD1Text") as TextMeshProUGUI;
        cD2Text = binding.GetObject("CD2Text") as TextMeshProUGUI;
        cD3Text = binding.GetObject("CD3Text") as TextMeshProUGUI;
        hpText = binding.GetObject("HpText") as TextMeshProUGUI;
        hpSlider = binding.GetObject("Hp") as Slider;
        Entity entity = Locator.entityManager.playerEntity;
        AbilityComponent abilityComponent = entity._components[EcsConst.ComponentConst.ABILITY] as AbilityComponent;
        Skill1 = abilityComponent.abilityDict[EcsConst.SkillID.Skill1];
        Skill2 = abilityComponent.abilityDict[EcsConst.SkillID.Skill2];
        Skill3 = abilityComponent.abilityDict[EcsConst.SkillID.Skill3];
        propertyC = entity._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
    }
    private void Update() 
    {
        UpdateSkill1CD();
        UpdateSkill2CD();
        UpdateSkill3CD();
        UpdateHp();
    }
    private void UpdateSkill1CD()
    {
       if (Skill1.CurrentCd > 0)
       {
         cD1Obj.SetActive(true);
         cD1Image.fillAmount = Skill1.CurrentCd/Skill1.CdCoolNum;
         cD1Text.text = Mathf.Ceil(Skill1.CurrentCd).ToString();
       }else
       {
         cD1Obj.SetActive(false);
       }

    }
    private void UpdateSkill2CD()
    {
       if (Skill2.CurrentCd > 0)
       {
         cD2Obj.SetActive(true);
         cD2Image.fillAmount = Skill2.CurrentCd/Skill2.CdCoolNum;
         cD2Text.text = Mathf.Ceil(Skill2.CurrentCd).ToString();
       }else
       {
         cD2Obj.SetActive(false);
       }        
    }
    private void UpdateSkill3CD()
    {
       if (Skill3.CurrentCd > 0)
       {
         cD3Obj.SetActive(true);
         cD3Image.fillAmount = Skill3.CurrentCd/Skill3.CdCoolNum;
         cD3Text.text = Mathf.Ceil(Skill3.CurrentCd).ToString();
       }else
       {
         cD3Obj.SetActive(false);
       }       
    }
    private void UpdateHp()
    {
      hpSlider.value = propertyC.hp / propertyC.maxHp;
      hpText.text = propertyC.hp + "/" + propertyC.maxHp;
    }       
}
