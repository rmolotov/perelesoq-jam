using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;
using Metro.Infrastructure.SceneManagement;

namespace Metro.Infrastructure.AssetManagement
{ public interface IAssetProvider : IInitializable
    {
        public Task<T> Load<T>(string key) where T : class;
        public Task<SceneInstance> LoadScene(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        public void Release(string key);
        public void Cleanup();
    }
}