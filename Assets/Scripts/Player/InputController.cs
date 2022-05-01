//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Player
{
    public partial class @InputController : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputController()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a4290103-c6ce-46ea-b209-2cd31e27a21a"",
            ""actions"": [
                {
                    ""name"": ""WASD"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2c4b0fe9-1761-4f0f-8140-30c57f3c67b0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""16744e5b-771f-4932-9a1b-108ac897a700"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cbb2890e-a09d-4959-8fb3-d8c8d214b3ab"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLeft"",
                    ""type"": ""Button"",
                    ""id"": ""819beae4-cf4e-4591-8dc6-5be4db94660c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseRight"",
                    ""type"": ""Button"",
                    ""id"": ""6152782a-f709-4521-9c0a-59f7e250bf0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""cd634698-b475-4819-bd97-2732a60b0748"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9accb6ef-2589-4452-9f5b-293c3e7f83d9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e73ebec3-7252-4f7c-be3b-388e188ea587"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8cfdbd43-26af-45d8-a023-b866e50137ed"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e8704f9-6362-4f54-bf96-54ede834a092"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0afa5bf1-6dee-40c6-ae2a-6a96f93896cc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b9923f77-e2bf-4776-b9b7-7219a5214b65"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""434df9b8-6d4e-4165-8371-8149acbafed9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2"",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfbad326-dd54-448d-be40-53efe59a9352"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80acb44d-c439-4299-b63c-9d3df86b92c1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""207fa399-b750-4414-a0ce-2bf3a939cfb8"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_WASD = m_Player.FindAction("WASD", throwIfNotFound: true);
            m_Player_Space = m_Player.FindAction("Space", throwIfNotFound: true);
            m_Player_MouseDelta = m_Player.FindAction("MouseDelta", throwIfNotFound: true);
            m_Player_MouseLeft = m_Player.FindAction("MouseLeft", throwIfNotFound: true);
            m_Player_MouseRight = m_Player.FindAction("MouseRight", throwIfNotFound: true);
            m_Player_Shift = m_Player.FindAction("Shift", throwIfNotFound: true);
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
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_WASD;
        private readonly InputAction m_Player_Space;
        private readonly InputAction m_Player_MouseDelta;
        private readonly InputAction m_Player_MouseLeft;
        private readonly InputAction m_Player_MouseRight;
        private readonly InputAction m_Player_Shift;
        public struct PlayerActions
        {
            private @InputController m_Wrapper;
            public PlayerActions(@InputController wrapper) { m_Wrapper = wrapper; }
            public InputAction @WASD => m_Wrapper.m_Player_WASD;
            public InputAction @Space => m_Wrapper.m_Player_Space;
            public InputAction @MouseDelta => m_Wrapper.m_Player_MouseDelta;
            public InputAction @MouseLeft => m_Wrapper.m_Player_MouseLeft;
            public InputAction @MouseRight => m_Wrapper.m_Player_MouseRight;
            public InputAction @Shift => m_Wrapper.m_Player_Shift;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @WASD.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                    @WASD.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                    @WASD.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                    @Space.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                    @Space.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                    @Space.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                    @MouseDelta.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseDelta;
                    @MouseLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLeft;
                    @MouseLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLeft;
                    @MouseLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLeft;
                    @MouseRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRight;
                    @MouseRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRight;
                    @MouseRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRight;
                    @Shift.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                    @Shift.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                    @Shift.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @WASD.started += instance.OnWASD;
                    @WASD.performed += instance.OnWASD;
                    @WASD.canceled += instance.OnWASD;
                    @Space.started += instance.OnSpace;
                    @Space.performed += instance.OnSpace;
                    @Space.canceled += instance.OnSpace;
                    @MouseDelta.started += instance.OnMouseDelta;
                    @MouseDelta.performed += instance.OnMouseDelta;
                    @MouseDelta.canceled += instance.OnMouseDelta;
                    @MouseLeft.started += instance.OnMouseLeft;
                    @MouseLeft.performed += instance.OnMouseLeft;
                    @MouseLeft.canceled += instance.OnMouseLeft;
                    @MouseRight.started += instance.OnMouseRight;
                    @MouseRight.performed += instance.OnMouseRight;
                    @MouseRight.canceled += instance.OnMouseRight;
                    @Shift.started += instance.OnShift;
                    @Shift.performed += instance.OnShift;
                    @Shift.canceled += instance.OnShift;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnWASD(InputAction.CallbackContext context);
            void OnSpace(InputAction.CallbackContext context);
            void OnMouseDelta(InputAction.CallbackContext context);
            void OnMouseLeft(InputAction.CallbackContext context);
            void OnMouseRight(InputAction.CallbackContext context);
            void OnShift(InputAction.CallbackContext context);
        }
    }
}
