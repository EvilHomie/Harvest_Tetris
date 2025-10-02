using DI;
using Economy;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArtifactsPanel : MonoBehaviour
{
    [SerializeField] ArtifactsElement _artifactWood; 
    [SerializeField] ArtifactsElement _articfactWheat;
    [SerializeField] ArtifactsElement _artifactIron;
    private ResourcesCollectSystem _resourcesCollectSystem;

    private Artifact _activeArtifact;

    [Inject]
    public void Construct(ResourcesCollectSystem resourcesCollectSystem)
    {
        _resourcesCollectSystem = resourcesCollectSystem;
    }

    private void Start()
    {
        _artifactWood.Disable();
        _articfactWheat.Disable();
        _artifactIron.Disable();
    }

    private void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            if (_activeArtifact == Artifact.WheatWithWood)
            {
                return;
            }

            _artifactWood.Enable();
            _articfactWheat.Disable();
            _artifactIron.Disable();
            _activeArtifact = Artifact.WheatWithWood;
            _resourcesCollectSystem.AplyArtifact(Artifact.WheatWithWood);
        }
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            if (_activeArtifact == Artifact.WheatMultipler)
            {
                return;
            }

            _artifactWood.Disable();
            _articfactWheat.Enable();
            _artifactIron.Disable();
            _activeArtifact = Artifact.WheatMultipler;
            _resourcesCollectSystem.AplyArtifact(Artifact.WheatMultipler);
        }
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            if (_activeArtifact == Artifact.IronOverload)
            {
                return;
            }

            _artifactWood.Disable();
            _articfactWheat.Disable();
            _artifactIron.Enable();
            _activeArtifact = Artifact.IronOverload;
            _resourcesCollectSystem.AplyArtifact(Artifact.IronOverload);
        }
    }
}
