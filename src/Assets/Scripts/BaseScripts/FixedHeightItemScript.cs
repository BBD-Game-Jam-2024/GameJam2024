using UnityEngine;

namespace BaseScripts
{
    public class FixedHeightItemScript : MonoBehaviour, IFixedHeight
    {
        [SerializeField] private float fixedHeight = 5f;

        public float GetHeightSpecified()
        {
            return fixedHeight;
        }
    }
}