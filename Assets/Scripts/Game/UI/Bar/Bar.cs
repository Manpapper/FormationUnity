using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    private Slider _bar;
    private CC2D _playerController;

    private bool _hideOnChange = false;
    private float _timer = 4f;

    public Slider bar { get => _bar; set => _bar = value; }
    public CC2D playerController { get => _playerController; set => _playerController = value; }
    public bool hideOnChange { get => _hideOnChange; set => _hideOnChange = value; }
    public float timer { get => _timer; set => _timer = value; }

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CC2D>();
        bar = GetComponent<Slider>();
        Init();
    }

    protected virtual void Init()
    {
        
    }

    public void SetValue(float value)
    {
        bar.value = value;
        if (hideOnChange)
        {
            SetActive(true);
            StartCoroutine(HideOnChange());
        }
    }

    public void SetMaxValue(float maxValue)
    {
        bar.maxValue = maxValue;
    }

    IEnumerator HideOnChange()
    {
        yield return new WaitForSeconds(timer);
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        bar.gameObject.SetActive(active);
    }
}
