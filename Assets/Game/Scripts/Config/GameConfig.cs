using UnityEngine;

namespace Game.Scripts.Config
{
    public class GameConfig : MonoBehaviour
    {
        [SerializeField] private string apiInventoryStatusUrl;
        [SerializeField] private string apiAuthKey;

        public string ApiInventoryStatusUrl => apiInventoryStatusUrl;
        public string ApiAuthKey => apiAuthKey;
    }
}
