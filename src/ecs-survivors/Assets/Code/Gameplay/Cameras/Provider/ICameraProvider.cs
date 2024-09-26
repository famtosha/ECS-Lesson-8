using Code.Gameplay.Common.Random;
using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
  public interface ICameraProvider
  {
    Camera MainCamera { get; }
    float WorldScreenHeight { get; }
    float WorldScreenWidth { get; }

        Vector2 GetRandomPositionOnScreen(IRandomService random);
        void SetMainCamera(Camera camera);
  }
}