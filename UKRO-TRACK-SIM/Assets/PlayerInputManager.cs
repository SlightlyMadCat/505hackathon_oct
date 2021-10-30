// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputManager"",
    ""maps"": [
        {
            ""name"": ""PlayerMap"",
            ""id"": ""3e810eed-15c2-4f43-b6be-d8839397bb00"",
            ""actions"": [
                {
                    ""name"": ""Lights"",
                    ""type"": ""Button"",
                    ""id"": ""5f6fed8b-e9a7-4181-91f7-d0c3b225a2d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sounds"",
                    ""type"": ""Button"",
                    ""id"": ""cd97f336-9f48-4dd5-82e4-72366036f868"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a3f3df5a-fa97-45a4-b6a4-b8eeb595fc63"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lights"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1a018da-89ea-45de-bb8b-3e022d03bd41"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sounds"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMap
        m_PlayerMap = asset.FindActionMap("PlayerMap", throwIfNotFound: true);
        m_PlayerMap_Lights = m_PlayerMap.FindAction("Lights", throwIfNotFound: true);
        m_PlayerMap_Sounds = m_PlayerMap.FindAction("Sounds", throwIfNotFound: true);
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

    // PlayerMap
    private readonly InputActionMap m_PlayerMap;
    private IPlayerMapActions m_PlayerMapActionsCallbackInterface;
    private readonly InputAction m_PlayerMap_Lights;
    private readonly InputAction m_PlayerMap_Sounds;
    public struct PlayerMapActions
    {
        private @PlayerInputManager m_Wrapper;
        public PlayerMapActions(@PlayerInputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Lights => m_Wrapper.m_PlayerMap_Lights;
        public InputAction @Sounds => m_Wrapper.m_PlayerMap_Sounds;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMapActions instance)
        {
            if (m_Wrapper.m_PlayerMapActionsCallbackInterface != null)
            {
                @Lights.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLights;
                @Lights.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLights;
                @Lights.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLights;
                @Sounds.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSounds;
                @Sounds.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSounds;
                @Sounds.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSounds;
            }
            m_Wrapper.m_PlayerMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Lights.started += instance.OnLights;
                @Lights.performed += instance.OnLights;
                @Lights.canceled += instance.OnLights;
                @Sounds.started += instance.OnSounds;
                @Sounds.performed += instance.OnSounds;
                @Sounds.canceled += instance.OnSounds;
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);
    public interface IPlayerMapActions
    {
        void OnLights(InputAction.CallbackContext context);
        void OnSounds(InputAction.CallbackContext context);
    }
}
