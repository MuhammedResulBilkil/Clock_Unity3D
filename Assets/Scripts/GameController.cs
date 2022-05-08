using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _hourInput;
    [SerializeField] private TMP_InputField _minuteInput;
    [SerializeField] private TMP_InputField _secondInput;
    
    [SerializeField] private RectTransform _hourLine;
    [SerializeField] private RectTransform _minuteLine;
    [SerializeField] private RectTransform _secondLine;
    
    [SerializeField] private Image _hourImage;
    [SerializeField] private Image _minuteImage;
    [SerializeField] private Image _secondImage;
    
    private float _hr = 0;
    private float _mn = 0;
    private float _sc = 0;

    private float _tempMn = 0f;

    private bool _isRealTimeOn;
    
    void Update()
    {
        if (_isRealTimeOn)
        {
            _sc = DateTime.Now.Second + DateTime.Now.Millisecond / 1000f;
            _mn = DateTime.Now.Minute + _sc / 60f;
            _hr = DateTime.Now.Hour % 12 + _mn / 60f;

            //Debug.LogFormat($"{_hr}:{_mn}:{_sc}: {DateTime.Now.Millisecond}");
        
            _hourLine.eulerAngles = new Vector3(0f, 0f, -360f * _hourImage.fillAmount);
            _minuteLine.eulerAngles = new Vector3(0f, 0f, -360f * _minuteImage.fillAmount);
            _secondLine.eulerAngles = new Vector3(0f, 0f, -360f * _secondImage.fillAmount);
        
            //Debug.LogFormat($"Angle: {360f * (_sc / 60f)}");
        
            _hourImage.fillAmount = _hr / 12f;
            _minuteImage.fillAmount = _mn / 60f;
            _secondImage.fillAmount = _sc / 60f;
        
            //Debug.LogFormat($"Day Time: {DateTime.Now.ToString("HH:mm:ss")}");
            //Debug.LogFormat($"{_hourImage.fillAmount}:{_minuteImage.fillAmount}:{_secondImage.fillAmount}");
        }
        else
        {
            _sc += Time.deltaTime;
            _mn = _sc / 60f + _tempMn;
            _hr = _hr % 12 + _mn / 60f;

            if (_sc >= 60f)
            {
                _sc = 0f;
                _tempMn++;
            }

            if (_mn >= 60f)
                _mn = 0f;
            
            if (_hr >= 12f)
                _hr = 0f;
            
            _hourLine.eulerAngles = new Vector3(0f, 0f, -360f * _hourImage.fillAmount);
            _minuteLine.eulerAngles = new Vector3(0f, 0f, -360f * _minuteImage.fillAmount);
            _secondLine.eulerAngles = new Vector3(0f, 0f, -360f * _secondImage.fillAmount);
            
            _hourImage.fillAmount = _hr / 12f;
            _minuteImage.fillAmount = _mn / 60f;
            _secondImage.fillAmount = _sc / 60f;
        }
    }

    public void IsRealtimeOn(bool isRealTimeOn)
    {
        _isRealTimeOn = isRealTimeOn;

        _sc = int.Parse(_secondInput.text);
        _mn = int.Parse(_minuteInput.text);
        _hr = int.Parse(_hourInput.text);

        _tempMn = 0f;
    }
}
