// GENERATED AUTOMATICALLY FROM 'Assets/Project/InputData/InputMenu.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMenu : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMenu()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMenu"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""502823c5-0eab-4e37-80f0-70f15647bb60"",
            ""actions"": [
                {
                    ""name"": ""WallAction"",
                    ""type"": ""Button"",
                    ""id"": ""189c60d0-047d-4377-bdf3-4c5c4013d25a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OtherAction"",
                    ""type"": ""Button"",
                    ""id"": ""090bb18b-786c-410b-afd2-421672e2418e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1d738120-9bc1-4524-85ca-bb9e2725163a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Menu"",
                    ""action"": ""WallAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""492a62a3-c9ad-466f-940b-6cff1b1365d9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Menu"",
                    ""action"": ""OtherAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Esc"",
            ""id"": ""9f3dbd2c-a390-4c28-8a8f-42a64955465e"",
            ""actions"": [
                {
                    ""name"": ""CloseAction"",
                    ""type"": ""Button"",
                    ""id"": ""059c476e-b004-4b75-b4f9-d9c4d43dcf04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b4a68cab-9dee-4a30-bed4-fe28264d2584"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Menu"",
                    ""action"": ""CloseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Menu"",
            ""bindingGroup"": ""Menu"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_WallAction = m_Menu.FindAction("WallAction", throwIfNotFound: true);
        m_Menu_OtherAction = m_Menu.FindAction("OtherAction", throwIfNotFound: true);
        // Esc
        m_Esc = asset.FindActionMap("Esc", throwIfNotFound: true);
        m_Esc_CloseAction = m_Esc.FindAction("CloseAction", throwIfNotFound: true);
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

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_WallAction;
    private readonly InputAction m_Menu_OtherAction;
    public struct MenuActions
    {
        private @InputMenu m_Wrapper;
        public MenuActions(@InputMenu wrapper) { m_Wrapper = wrapper; }
        public InputAction @WallAction => m_Wrapper.m_Menu_WallAction;
        public InputAction @OtherAction => m_Wrapper.m_Menu_OtherAction;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @WallAction.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnWallAction;
                @WallAction.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnWallAction;
                @WallAction.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnWallAction;
                @OtherAction.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnOtherAction;
                @OtherAction.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnOtherAction;
                @OtherAction.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnOtherAction;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WallAction.started += instance.OnWallAction;
                @WallAction.performed += instance.OnWallAction;
                @WallAction.canceled += instance.OnWallAction;
                @OtherAction.started += instance.OnOtherAction;
                @OtherAction.performed += instance.OnOtherAction;
                @OtherAction.canceled += instance.OnOtherAction;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Esc
    private readonly InputActionMap m_Esc;
    private IEscActions m_EscActionsCallbackInterface;
    private readonly InputAction m_Esc_CloseAction;
    public struct EscActions
    {
        private @InputMenu m_Wrapper;
        public EscActions(@InputMenu wrapper) { m_Wrapper = wrapper; }
        public InputAction @CloseAction => m_Wrapper.m_Esc_CloseAction;
        public InputActionMap Get() { return m_Wrapper.m_Esc; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EscActions set) { return set.Get(); }
        public void SetCallbacks(IEscActions instance)
        {
            if (m_Wrapper.m_EscActionsCallbackInterface != null)
            {
                @CloseAction.started -= m_Wrapper.m_EscActionsCallbackInterface.OnCloseAction;
                @CloseAction.performed -= m_Wrapper.m_EscActionsCallbackInterface.OnCloseAction;
                @CloseAction.canceled -= m_Wrapper.m_EscActionsCallbackInterface.OnCloseAction;
            }
            m_Wrapper.m_EscActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CloseAction.started += instance.OnCloseAction;
                @CloseAction.performed += instance.OnCloseAction;
                @CloseAction.canceled += instance.OnCloseAction;
            }
        }
    }
    public EscActions @Esc => new EscActions(this);
    private int m_MenuSchemeIndex = -1;
    public InputControlScheme MenuScheme
    {
        get
        {
            if (m_MenuSchemeIndex == -1) m_MenuSchemeIndex = asset.FindControlSchemeIndex("Menu");
            return asset.controlSchemes[m_MenuSchemeIndex];
        }
    }
    public interface IMenuActions
    {
        void OnWallAction(InputAction.CallbackContext context);
        void OnOtherAction(InputAction.CallbackContext context);
    }
    public interface IEscActions
    {
        void OnCloseAction(InputAction.CallbackContext context);
    }
}
