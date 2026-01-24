using BGNS_Studios;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIBlur : MonoBehaviour
{
    private Volume _volume;
    private InputManagerService _inputManagerService;
    private DepthOfField _blurEffect;


    private void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _blurEffect);
        _inputManagerService = ServiceLocator.Instance.Get<InputManagerService>();
        if (_inputManagerService)
            _inputManagerService.OnFocusing += OnFocusing;
    }
    private void OnDestroy()
    {
        if (_inputManagerService)
            _inputManagerService.OnFocusing -= OnFocusing;
    }
    private void OnFocusing(bool isfocus)
    {
        if (_blurEffect == null)
            return;

        _blurEffect.mode.value = isfocus ? DepthOfFieldMode.Off : DepthOfFieldMode.Bokeh;

    }
}
