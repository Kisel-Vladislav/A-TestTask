using CodeBase.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private const string GameSceneName = "Game";

        private void Awake()
        {
            RegisterService();
            LoadGameScene();
        }

        private void RegisterService()
        {
            AllServices.Container.RegisterSingle<IInputService>(new StandaloneInputService());
        }
        private void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}