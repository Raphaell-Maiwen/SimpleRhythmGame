// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Instruments"",
            ""id"": ""17d02d40-6473-4e71-af1a-360d948d0789"",
            ""actions"": [
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""6880ce5a-52a9-42ef-9a09-c05bb917c01a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""d1c2cce5-ff0b-4207-9ed0-1732302c7846"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""77578df3-9698-4ad9-8822-a6ae5c46714b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""d0b8e4f2-14f2-4458-ab07-a8f4378ac7f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""S"",
                    ""type"": ""Button"",
                    ""id"": ""60c1417a-28a9-4106-a2bd-0211c174869a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""696d533a-0a6e-4849-b6b2-5c6891438989"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""D"",
                    ""type"": ""Button"",
                    ""id"": ""f2c3f9dc-2acc-4cfe-afbe-159bc829e73e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""W"",
                    ""type"": ""Button"",
                    ""id"": ""8946f60c-f05f-485b-9e26-30d9c5c2e978"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""666f2d2b-4a4b-40eb-81d0-feca629ab941"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d77c3b6b-cb12-4b6b-901a-664013ee7c6a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d23f71b-4cd2-4e33-b78b-9b162ee9942c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f521a21d-23ae-48c7-9538-a724a07c4e57"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65d1e107-941a-4cd3-8232-cba73aa6758d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""S"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51819dd0-7782-47f2-bc0e-7f31c789da5d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9970b9a9-6b6d-4d4b-b6c0-b2dbaf8b2806"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec42f8d7-625c-425f-9ec5-9d8d031aee02"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""W"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Instruments
        m_Instruments = asset.FindActionMap("Instruments", throwIfNotFound: true);
        m_Instruments_Down = m_Instruments.FindAction("Down", throwIfNotFound: true);
        m_Instruments_Left = m_Instruments.FindAction("Left", throwIfNotFound: true);
        m_Instruments_Right = m_Instruments.FindAction("Right", throwIfNotFound: true);
        m_Instruments_Up = m_Instruments.FindAction("Up", throwIfNotFound: true);
        m_Instruments_S = m_Instruments.FindAction("S", throwIfNotFound: true);
        m_Instruments_A = m_Instruments.FindAction("A", throwIfNotFound: true);
        m_Instruments_D = m_Instruments.FindAction("D", throwIfNotFound: true);
        m_Instruments_W = m_Instruments.FindAction("W", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Instruments
    private readonly InputActionMap m_Instruments;
    private IInstrumentsActions m_InstrumentsActionsCallbackInterface;
    private readonly InputAction m_Instruments_Down;
    private readonly InputAction m_Instruments_Left;
    private readonly InputAction m_Instruments_Right;
    private readonly InputAction m_Instruments_Up;
    private readonly InputAction m_Instruments_S;
    private readonly InputAction m_Instruments_A;
    private readonly InputAction m_Instruments_D;
    private readonly InputAction m_Instruments_W;
    public struct InstrumentsActions
    {
        private @Controls m_Wrapper;
        public InstrumentsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Down => m_Wrapper.m_Instruments_Down;
        public InputAction @Left => m_Wrapper.m_Instruments_Left;
        public InputAction @Right => m_Wrapper.m_Instruments_Right;
        public InputAction @Up => m_Wrapper.m_Instruments_Up;
        public InputAction @S => m_Wrapper.m_Instruments_S;
        public InputAction @A => m_Wrapper.m_Instruments_A;
        public InputAction @D => m_Wrapper.m_Instruments_D;
        public InputAction @W => m_Wrapper.m_Instruments_W;
        public InputActionMap Get() { return m_Wrapper.m_Instruments; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InstrumentsActions set) { return set.Get(); }
        public void SetCallbacks(IInstrumentsActions instance)
        {
            if (m_Wrapper.m_InstrumentsActionsCallbackInterface != null)
            {
                @Down.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnRight;
                @Up.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnUp;
                @S.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnS;
                @S.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnS;
                @S.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnS;
                @A.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnA;
                @D.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnD;
                @D.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnD;
                @D.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnD;
                @W.started -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnW;
                @W.performed -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnW;
                @W.canceled -= m_Wrapper.m_InstrumentsActionsCallbackInterface.OnW;
            }
            m_Wrapper.m_InstrumentsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @S.started += instance.OnS;
                @S.performed += instance.OnS;
                @S.canceled += instance.OnS;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @D.started += instance.OnD;
                @D.performed += instance.OnD;
                @D.canceled += instance.OnD;
                @W.started += instance.OnW;
                @W.performed += instance.OnW;
                @W.canceled += instance.OnW;
            }
        }
    }
    public InstrumentsActions @Instruments => new InstrumentsActions(this);
    public interface IInstrumentsActions
    {
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnS(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnD(InputAction.CallbackContext context);
        void OnW(InputAction.CallbackContext context);
    }
}
