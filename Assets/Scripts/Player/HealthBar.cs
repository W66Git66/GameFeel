using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Singleton<HealthBar>
{
    public Image _hpImg;
    public Image _hpEffectImg;

    public float _maxHealth;
    public float _currentHealth;
    public float buffTime = 0.5f;

    private Coroutine updateCoroutinel;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);

        UpdateHealthBar();

        if(_currentHealth<=0)
        {
            //Die
        }
    }

    public IEnumerator IncreaseMyHP(float amont)
    { 
        yield return new WaitForSeconds(1f);
        SetHealth(_currentHealth + amont);
       
    }

    public IEnumerator DecreaseMyHP(float amont)
    {
        yield return new WaitForSeconds(1f);
        SetHealth(_currentHealth - amont);
        
    }


    /// <summary>
    /// 增加血量脚本，下面是减少血量
    /// </summary>
    /// <param name="amont">伤害数值</param>
    public void IncreaseHP(float amont)
    {
        StopCoroutine(DecreaseMyHP(amont));
        if (_currentHealth >= _maxHealth)
        {
            StopCoroutine(IncreaseMyHP(amont));
            return;
        }

        StartCoroutine(IncreaseMyHP(amont));
    }

    public void DecreaseHP(float amont)
    {
        StopCoroutine(IncreaseMyHP(amont));
        if (_currentHealth <=0)
        {
            StopCoroutine(IncreaseMyHP(amont));
            return;
        }

        StartCoroutine(DecreaseMyHP(amont));
    }

    private void UpdateHealthBar()
    {
        _hpImg.fillAmount = _currentHealth/_maxHealth;

        if(updateCoroutinel!=null)
        {
            StopCoroutine(updateCoroutinel);
        }
        updateCoroutinel = StartCoroutine(UpdateHPEffect());
    }

    private IEnumerator UpdateHPEffect()
    {
        float effectLength = _hpEffectImg.fillAmount - _hpImg.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < buffTime && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            _hpEffectImg.fillAmount = Mathf.Lerp(_hpImg.fillAmount + effectLength, _hpImg.fillAmount, elapsedTime / buffTime);
            yield return null;
        }

        _hpEffectImg.fillAmount = _hpImg.fillAmount; 
    }


}
