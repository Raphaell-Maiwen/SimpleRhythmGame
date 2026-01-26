using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private Slider _bpmSlider;
    [SerializeField] private TextMeshProUGUI _bpmText;

    [SerializeField] private Slider _beatPerBarSlider;
    [SerializeField] private TextMeshProUGUI _beatPerBarText;

    [SerializeField] private Slider _barsPerSoloSlider;
    [SerializeField] private TextMeshProUGUI _barsPerSoloText;

    [SerializeField] private TMP_Dropdown _controlsSchemeDropdown;
    [SerializeField] private Toggle _FKeysOnToggle;
    [SerializeField] private GameObject _fKeysWarning;

    [SerializeField] private TMP_Dropdown _player1SoundsSetDropdown;
    [SerializeField] private TMP_Dropdown _player2SoundsSetDropdown;

    [Header("Default settings")] 
    [SerializeField] private int bpm;
    [SerializeField] private int beatPerBar;
    [SerializeField] private int bars;
    [SerializeField] private InputMode inputMode;
    [SerializeField] private bool FKeysOn;
    [SerializeField] private string soundSet;

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
        
        _controlsSchemeDropdown.onValueChanged.AddListener(delegate {InputModeDropdownValueChanged ();});

        _player1SoundsSetDropdown.onValueChanged.AddListener(delegate { SoundSetPlayer1DropdownValueChanged() ;});
        _player2SoundsSetDropdown.onValueChanged.AddListener(delegate { SoundSetPlayer2DropdownValueChanged() ;});

        SetInputMode();
        SetPlayer1SoundSet();
        SetPlayer2SoundSet();

        _FKeysOnToggle.isOn = _parameters.fKeysOn;
        _fKeysWarning.SetActive(_FKeysOnToggle.isOn);
        _FKeysOnToggle.onValueChanged.AddListener(delegate { FKeysOnToggled ();});
    }

    private void OnDisable()
    {
        _bpmSlider.onValueChanged.RemoveListener(delegate {BPMValueChanged ();});
        _beatPerBarSlider.onValueChanged.RemoveListener(delegate {BeatPerBarValueChanged ();});
        _barsPerSoloSlider.onValueChanged.RemoveListener(delegate { BarsPerSoloValueChanged(); });
        _controlsSchemeDropdown.onValueChanged.RemoveListener(delegate { InputModeDropdownValueChanged(); });
        _player1SoundsSetDropdown.onValueChanged.RemoveListener(delegate { SoundSetPlayer1DropdownValueChanged(); });
        _player2SoundsSetDropdown.onValueChanged.RemoveListener(delegate { SoundSetPlayer2DropdownValueChanged(); });
        _FKeysOnToggle.onValueChanged.RemoveListener(delegate { FKeysOnToggled(); });
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

    void InputModeDropdownValueChanged()
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

    void SoundSetPlayer1DropdownValueChanged()
    {
        _parameters.SetPlayerSoundSet(0, _player1SoundsSetDropdown.value);
    }

    void SoundSetPlayer2DropdownValueChanged()
    {
        _parameters.SetPlayerSoundSet(1, _player2SoundsSetDropdown.value);
    }

    void FKeysOnToggled() 
    {
        _parameters.fKeysOn = _FKeysOnToggle.isOn;
        _fKeysWarning.SetActive(_FKeysOnToggle.isOn);
    }

    private void SetInputMode()
    {
        switch (_parameters.inputMode)
        {
            case InputMode.keyboard:
                _controlsSchemeDropdown.value = 0;
                break;
            case InputMode.gamepad:
                _controlsSchemeDropdown.value = 1;
                break;
            case InputMode.keytar:
                _controlsSchemeDropdown.value = 2;
                break;
        }
    }

    private void SetPlayer1SoundSet()
    {
        if (_parameters._player1SoundSetValue > 0)
        {
            _player1SoundsSetDropdown.value = _parameters._player1SoundSetValue;
        }
        else
        {
            _player1SoundsSetDropdown.value = 0;
        }
    }

    private void SetPlayer2SoundSet()
    {
        if (_parameters._player2SoundSetValue > 0)
        {
            _player2SoundsSetDropdown.value = _parameters._player2SoundSetValue;
        }
        else
        {
            _player2SoundsSetDropdown.value = 0;
        }
    }

    public void ResetToDefault()
    {
        _parameters.bpm = bpm;
        _parameters.beatPerBar = beatPerBar;
        _parameters.bars = bars;
        _parameters.inputMode = inputMode;
        _parameters.fKeysOn = FKeysOn;
        _parameters.player1SoundsSet = soundSet;
        _parameters.player2SoundsSet = soundSet;
        _parameters._player1SoundSetValue = 0;
        _parameters._player2SoundSetValue = 0;

        _bpmSlider.value = _parameters.bpm;
        _beatPerBarSlider.value = _parameters.beatPerBar;
        _barsPerSoloSlider.value = _parameters.bars;
        
        _bpmText.text = _parameters.bpm.ToString();
        _beatPerBarText.text = _parameters.beatPerBar.ToString();
        _barsPerSoloText.text = _parameters.bars.ToString();
        
        SetInputMode();
        SetPlayer1SoundSet();
        SetPlayer2SoundSet();
    }
}
