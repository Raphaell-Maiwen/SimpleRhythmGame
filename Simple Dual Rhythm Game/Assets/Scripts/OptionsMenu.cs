using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //TODO: Change to Scriptable Object?
    [SerializeField] private Parameters _parameters;
    [SerializeField] private Slider _bpmSlider;
    [SerializeField] private TextMeshProUGUI _bpmText;

    [SerializeField] private Slider _beatPerBarSlider;
    [SerializeField] private TextMeshProUGUI _beatPerBarText;

    [SerializeField] private Slider _barsPerSoloSlider;
    [SerializeField] private TextMeshProUGUI _barsPerSoloText;

    [SerializeField] private TMP_Dropdown _controlsSchemeDropdown;

    private void OnEnable()
    {
        _bpmText.text = _parameters.bpm.ToString();
        _bpmSlider.value = _parameters.bpm;
        _bpmSlider.onValueChanged.AddListener(delegate {BPMValueChanged ();});

        _beatPerBarText.text = _parameters.beatPerBar.ToString();
        _beatPerBarSlider.value = _parameters.beatPerBar;
        _beatPerBarSlider.onValueChanged.AddListener(delegate {BeatPerBarValueChanged ();});

        _barsPerSoloText.text = _parameters.bars.ToString();
        _barsPerSoloSlider.value = _parameters.bars;
        _barsPerSoloSlider.onValueChanged.AddListener(delegate {BarsPerSoloValueChanged ();});
        
        _controlsSchemeDropdown.onValueChanged.AddListener(delegate {DropdownValueChanged ();});
    }

    private void OnDisable()
    {
        _bpmSlider.onValueChanged.RemoveListener(delegate {BPMValueChanged ();});
        _beatPerBarSlider.onValueChanged.RemoveListener(delegate {BeatPerBarValueChanged ();});
    }

    void BPMValueChanged()
    {
        _parameters.bpm = (int)_bpmSlider.value;
        _bpmText.text = _parameters.bpm.ToString();
    }

    void BeatPerBarValueChanged()
    {
        _parameters.beatPerBar = (int)_beatPerBarSlider.value;
        _beatPerBarText.text = _parameters.beatPerBar.ToString();
    }

    void BarsPerSoloValueChanged()
    {
        _parameters.bars = (int)_barsPerSoloSlider.value;
        _barsPerSoloText.text = _parameters.bars.ToString();
    }

    void DropdownValueChanged()
    {
        switch (_controlsSchemeDropdown.value)
        {
            case 0:
                _parameters.inputMode = InputMode.keyboard;
                break;
            case 1:
                _parameters.inputMode = InputMode.gamepad;
                break;
            case 2:
                _parameters.inputMode = InputMode.keytar;
                break;
        }
    }
}
