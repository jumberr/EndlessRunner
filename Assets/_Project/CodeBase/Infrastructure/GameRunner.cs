using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] public Bootstrapper _bootstrapperPrefab;
        
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();

            if (bootstrapper is null) 
                Instantiate(_bootstrapperPrefab);
        }
    }
}