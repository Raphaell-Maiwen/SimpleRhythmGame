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
            ""name"": ""Arrows"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""id"": ""8e39d542-97b6-4431-be21-7f7badd682c1"",
            ""actions"": [
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""6e5d1b27-f04d-422b-b172-edd80a489d17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""f1bedbac-7e62-4ee0-91ae-24f8b3026f64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""05b749ba-d56f-4f1d-906f-641aa08dc5e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Button"",
                    ""id"": ""2c0a5a18-003c-485b-a970-4a533924639a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""50f79c9b-6cca-4472-8c88-6256754bf63f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a537d923-ae2c-4576-b279-14df9565f410"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27f7d5d4-84ce-4279-b169-8884da9f56e0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8af5fef6-c309-47e7-b17f-ccd09399f96a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Arrows
        m_Arrows = asset.FindActionMap("Arrows", throwIfNotFound: true);
        m_Arrows_Down = m_Arrows.FindAction("Down", throwIfNotFound: true);
        m_Arrows_Left = m_Arrows.FindAction("Left", throwIfNotFound: true);
        m_Arrows_Right = m_Arrows.FindAction("Right", throwIfNotFound: true);
        m_Arrows_Up = m_Arrows.FindAction("Up", throwIfNotFound: true);
        m_Arrows_S = m_Arrows.FindAction("S", throwIfNotFound: true);
        m_Arrows_A = m_Arrows.FindAction("A", throwIfNotFound: true);
        m_Arrows_D = m_Arrows.FindAction("D", throwIfNotFound: true);
        m_Arrows_W = m_Arrows.FindAction("W", throwIfNotFound: true);
        // Gamepad
        m_Gamepad = asset.FindActionMap("Gamepad", throwIfNotFound: true);
        m_Gamepad_A = m_Gamepad.FindAction("A", throwIfNotFound: true);
        m_Gamepad_X = m_Gamepad.FindAction("X", throwIfNotFound: true);
        m_Gamepad_B = m_Gamepad.FindAction("B", throwIfNotFound: true);
        m_Gamepad_Y = m_Gamepad.FindAction("Y", throwIfNotFound: true);
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

    // Arrows
    private readonly InputActionMap m_Arrows;
    private IArrowsActions m_ArrowsActionsCallbackInterface;
    private readonly InputAction m_Arrows_Down;
    private readonly InputAction m_Arrows_Left;
    private readonly InputAction m_Arrows_Right;
    private readonly InputAction m_Arrows_Up;
    private readonly InputAction m_Arrows_S;
    private readonly InputAction m_Arrows_A;
    private readonly InputAction m_Arrows_D;
    private readonly InputAction m_Arrows_W;
    public struct ArrowsActions
    {
        private @Controls m_Wrapper;
        public ArrowsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Down => m_Wrapper.m_Arrows_Down;
        public InputAction @Left => m_Wrapper.m_Arrows_Left;
        public InputAction @Right => m_Wrapper.m_Arrows_Right;
        public InputAction @Up => m_Wrapper.m_Arrows_Up;
        public InputAction @S => m_Wrapper.m_Arrows_S;
        public InputAction @A => m_Wrapper.m_Arrows_A;
        public InputAction @D => m_Wrapper.m_Arrows_D;
        public InputAction @W => m_Wrapper.m_Arrows_W;
        public InputActionMap Get() { return m_Wrapper.m_Arrows; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArrowsActions set) { return set.Get(); }
        public void SetCallbacks(IArrowsActions instance)
        {
            if (m_Wrapper.m_ArrowsActionsCallbackInterface != null)
            {
                @Down.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnRight;
                @Up.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnUp;
                @S.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnS;
                @S.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnS;
                @S.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnS;
                @A.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnA;
                @D.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnD;
                @D.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnD;
                @D.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnD;
                @W.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnW;
                @W.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnW;
                @W.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnW;
            }
            m_Wrapper.m_ArrowsActionsCallbackInterface = instance;
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
    public ArrowsActions @Arrows => new ArrowsActions(this);

    // Gamepad
    private readonly InputActionMap m_Gamepad;
    private IGamepadActions m_GamepadActionsCallbackInterface;
    private readonly InputAction m_Gamepad_A;
    private readonly InputAction m_Gamepad_X;
    private readonly InputAction m_Gamepad_B;
    private readonly InputAction m_Gamepad_Y;
    public struct GamepadActions
    {
        private @Controls m_Wrapper;
        public GamepadActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @A => m_Wrapper.m_Gamepad_A;
        public InputAction @X => m_Wrapper.m_Gamepad_X;
        public InputAction @B => m_Wrapper.m_Gamepad_B;
        public InputAction @Y => m_Wrapper.m_Gamepad_Y;
        public InputActionMap Get() { return m_Wrapper.m_Gamepad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadActions instance)
        {
            if (m_Wrapper.m_GamepadActionsCallbackInterface != null)
            {
                @A.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnA;
                @X.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnX;
                @B.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnB;
                @Y.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnY;
                @Y.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnY;
                @Y.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnY;
            }
            m_Wrapper.m_GamepadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
                @Y.started += instance.OnY;
                @Y.performed += instance.OnY;
                @Y.canceled += instance.OnY;
            }
        }
    }
    public GamepadActions @Gamepad => new GamepadActions(this);
    public interface IArrowsActions
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
    public interface IGamepadActions
    {
        void OnA(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
    }
}
