// GENERATED AUTOMATICALLY FROM 'Assets/Project/InputData/InputSave.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSave : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSave()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSave"",
    ""maps"": [
        {
            ""name"": ""Save"",
            ""id"": ""bd0dc39e-5eef-4e40-8fe6-9e45a2f90124"",
            ""actions"": [
                {
                    ""name"": ""SaveAction"",
                    ""type"": ""Button"",
                    ""id"": ""abbd29a0-4c51-4e41-9664-ff9469e1374b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""3ff017be-60c7-49ad-8243-b309b6e0bb6e"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4acb95e8-ef44-4270-959d-d9f45cc41886"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""504880ad-4122-48d5-8bd9-25a887b75e87"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""1e5a7241-e3b6-48ac-ad6d-e01e09fdfb13"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""10aed3c2-4317-4241-b3a0-5e50e3f78d39"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""save"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""b165ea6d-9cb8-418e-905e-bd371330eb2f"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""save"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""36641a7c-f733-4c36-b575-905effd4aa4f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""save"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""68961c2b-0916-4f71-a53f-411b40c3f9ce"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""save"",
                    ""action"": ""SaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""save"",
            ""bindingGroup"": ""save"",
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
        // Save
        m_Save = asset.FindActionMap("Save", throwIfNotFound: true);
        m_Save_SaveAction = m_Save.FindAction("SaveAction", throwIfNotFound: true);
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

    // Save
    private readonly InputActionMap m_Save;
    private ISaveActions m_SaveActionsCallbackInterface;
    private readonly InputAction m_Save_SaveAction;
    public struct SaveActions
    {
        private @InputSave m_Wrapper;
        public SaveActions(@InputSave wrapper) { m_Wrapper = wrapper; }
        public InputAction @SaveAction => m_Wrapper.m_Save_SaveAction;
        public InputActionMap Get() { return m_Wrapper.m_Save; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SaveActions set) { return set.Get(); }
        public void SetCallbacks(ISaveActions instance)
        {
            if (m_Wrapper.m_SaveActionsCallbackInterface != null)
            {
                @SaveAction.started -= m_Wrapper.m_SaveActionsCallbackInterface.OnSaveAction;
                @SaveAction.performed -= m_Wrapper.m_SaveActionsCallbackInterface.OnSaveAction;
                @SaveAction.canceled -= m_Wrapper.m_SaveActionsCallbackInterface.OnSaveAction;
            }
            m_Wrapper.m_SaveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SaveAction.started += instance.OnSaveAction;
                @SaveAction.performed += instance.OnSaveAction;
                @SaveAction.canceled += instance.OnSaveAction;
            }
        }
    }
    public SaveActions @Save => new SaveActions(this);
    private int m_saveSchemeIndex = -1;
    public InputControlScheme saveScheme
    {
        get
        {
            if (m_saveSchemeIndex == -1) m_saveSchemeIndex = asset.FindControlSchemeIndex("save");
            return asset.controlSchemes[m_saveSchemeIndex];
        }
    }
    public interface ISaveActions
    {
        void OnSaveAction(InputAction.CallbackContext context);
    }
}
