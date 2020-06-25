// GENERATED AUTOMATICALLY FROM 'Assets/Demo/InputData/InputCamera.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputCamera : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputCamera()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputCamera"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""0e0d8284-2ad9-44e8-81fc-56b0388d0b11"",
            ""actions"": [
                {
                    ""name"": ""StartMovement"",
                    ""type"": ""Button"",
                    ""id"": ""4c83d12a-328c-420b-bff8-a8f0485694d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""df334ea7-8176-4c12-9bd1-31d7f9174048"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""1c5fc29f-f757-4c8f-a598-92e6cb69206f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5588fd22-8cfa-4691-a5bb-f47d0bed826b"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e71b5667-702b-4370-a6c0-b332a3572a49"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(pressPoint=0.01)"",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""StartMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27b018a4-8489-4164-a774-425442985d9a"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press(pressPoint=0.01)"",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""StartMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""669eafec-37c7-4ad7-a6bf-5e84173c7670"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""computer"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""computer"",
            ""bindingGroup"": ""computer"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_StartMovement = m_Camera.FindAction("StartMovement", throwIfNotFound: true);
        m_Camera_Movement = m_Camera.FindAction("Movement", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_StartMovement;
    private readonly InputAction m_Camera_Movement;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @InputCamera m_Wrapper;
        public CameraActions(@InputCamera wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartMovement => m_Wrapper.m_Camera_StartMovement;
        public InputAction @Movement => m_Wrapper.m_Camera_Movement;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @StartMovement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
                @StartMovement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
                @StartMovement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
                @Movement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartMovement.started += instance.OnStartMovement;
                @StartMovement.performed += instance.OnStartMovement;
                @StartMovement.canceled += instance.OnStartMovement;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    private int m_computerSchemeIndex = -1;
    public InputControlScheme computerScheme
    {
        get
        {
            if (m_computerSchemeIndex == -1) m_computerSchemeIndex = asset.FindControlSchemeIndex("computer");
            return asset.controlSchemes[m_computerSchemeIndex];
        }
    }
    public interface ICameraActions
    {
        void OnStartMovement(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
