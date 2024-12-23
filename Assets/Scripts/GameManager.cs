using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private VRInputManager vrInputManagerPrefab;
    [SerializeField]
    private KeyboardMouseInputManager keyboardMouseInputManager;
    [SerializeField]
    private bool useKeyboardMouse = false;
    [SerializeField]
    private ParticleSystem leftHandParticleSystem;
    [SerializeField]
    private ParticleSystem rightHandParticleSystem;

    private IInputProvider inputProvider;

    void Start()
    {
#if !UNITY_EDITOR
        useKeyboardMouse = false;
#endif
        if (useKeyboardMouse)
        {
            inputProvider = Instantiate(keyboardMouseInputManager);
        }
        else
        {
            inputProvider = Instantiate(vrInputManagerPrefab);
        }

        if (!inputProvider.TryInitialize())
        {
            Debug.LogError("Failed to initialize input provider");
            gameObject.SetActive(false);
            return;
        }

        // Set particle systems to follow controllers
        var leftController = inputProvider.GetLeftController();
        leftHandParticleSystem.transform.SetParent(leftController.GetTransform());
        leftHandParticleSystem.transform.localPosition = Vector3.zero;
        leftHandParticleSystem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        var rightController = inputProvider.GetRightController();
        rightHandParticleSystem.transform.SetParent(rightController.GetTransform());
        rightHandParticleSystem.transform.localPosition = Vector3.zero;
        rightHandParticleSystem.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (inputProvider.GetLeftController().IsTriggerPressed())
        {
            RandomizeParticleColor(leftHandParticleSystem);
            leftHandParticleSystem.Emit(1);
        }

        if (inputProvider.GetRightController().IsTriggerPressed())
        {
            RandomizeParticleColor(rightHandParticleSystem);
            rightHandParticleSystem.Emit(1);
        }
    }
    private void RandomizeParticleColor(ParticleSystem particleSystem)
    {
        var mainModule = particleSystem.main;
        mainModule.startColor = new Color(
            Random.value,
            Random.value,
            Random.value,
            1.0f
            // So how this works now is that the void update takes into consideration a new private void which is "RandomizeParticleColor".
            // This is then within a private void that chooses a random value three times, since it works with RGB. So a random value of Red, Green and Blue.
            // The final value of "1.0f" is for the Alpha, since we want a full alpha color. 
        );
    }
}