using UnityEngine;

public interface IInteractMenuItem
{
    string GetText();
    void Execute();

    void GetConfig(PlantConfig plantConfig, Plant plant, Transform plantTransform);
}
