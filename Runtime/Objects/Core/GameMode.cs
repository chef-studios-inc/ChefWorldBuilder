using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chef
{
    public class GameMode : MonoBehaviour
    {
        public enum Mode
        {
            Default,
            ScavengerHunt,
        }

        public Mode mode = Mode.Default;
    }
}
