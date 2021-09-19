// GENERATED AUTOMATICALLY FROM 'Packages/com.mgsq.ui/Runtime/Inventory/InventoryInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InventoryInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InventoryInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InventoryInput"",
    ""maps"": [
        {
            ""name"": ""RollingInventory"",
            ""id"": ""29167198-1949-4d28-8564-ff5393b526e3"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""b324a751-753b-4434-8aab-b7c7a2f73184"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Button"",
                    ""id"": ""a9d674d1-0f81-4e83-bc0a-fcd7f0f0a3d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ItemSelected"",
                    ""type"": ""Button"",
                    ""id"": ""16eee1fa-47f0-41c9-ac3d-514833e1f8fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""75d5e756-57f2-4254-9b78-157225111591"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87fecc65-114b-42b6-b982-995fe0c98fc0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64ed6c5f-2018-4e43-b342-739a6fa87198"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ItemSelected"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // RollingInventory
        m_RollingInventory = asset.FindActionMap("RollingInventory", throwIfNotFound: true);
        m_RollingInventory_Forward = m_RollingInventory.FindAction("Forward", throwIfNotFound: true);
        m_RollingInventory_Backward = m_RollingInventory.FindAction("Backward", throwIfNotFound: true);
        m_RollingInventory_ItemSelected = m_RollingInventory.FindAction("ItemSelected", throwIfNotFound: true);
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

    // RollingInventory
    private readonly InputActionMap m_RollingInventory;
    private IRollingInventoryActions m_RollingInventoryActionsCallbackInterface;
    private readonly InputAction m_RollingInventory_Forward;
    private readonly InputAction m_RollingInventory_Backward;
    private readonly InputAction m_RollingInventory_ItemSelected;
    public struct RollingInventoryActions
    {
        private @InventoryInput m_Wrapper;
        public RollingInventoryActions(@InventoryInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_RollingInventory_Forward;
        public InputAction @Backward => m_Wrapper.m_RollingInventory_Backward;
        public InputAction @ItemSelected => m_Wrapper.m_RollingInventory_ItemSelected;
        public InputActionMap Get() { return m_Wrapper.m_RollingInventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RollingInventoryActions set) { return set.Get(); }
        public void SetCallbacks(IRollingInventoryActions instance)
        {
            if (m_Wrapper.m_RollingInventoryActionsCallbackInterface != null)
            {
                @Forward.started -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnBackward;
                @ItemSelected.started -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnItemSelected;
                @ItemSelected.performed -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnItemSelected;
                @ItemSelected.canceled -= m_Wrapper.m_RollingInventoryActionsCallbackInterface.OnItemSelected;
            }
            m_Wrapper.m_RollingInventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Backward.started += instance.OnBackward;
                @Backward.performed += instance.OnBackward;
                @Backward.canceled += instance.OnBackward;
                @ItemSelected.started += instance.OnItemSelected;
                @ItemSelected.performed += instance.OnItemSelected;
                @ItemSelected.canceled += instance.OnItemSelected;
            }
        }
    }
    public RollingInventoryActions @RollingInventory => new RollingInventoryActions(this);
    public interface IRollingInventoryActions
    {
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
        void OnItemSelected(InputAction.CallbackContext context);
    }
}
