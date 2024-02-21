using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemSOAPI : ScriptableObject
    {
        public int MyProAPI { get; set; }

        [field: SerializeField]
        public bool IsStackableAPI { get; set; }

        public int IDAPI => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSizeAPI { get; set; } = 1;

        [field: SerializeField]
        public string NameAPI { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string DescriptionAPI { get; set; }

        [field: SerializeField]

        public Sprite IteamImageAPI { get; set; }


    }
}