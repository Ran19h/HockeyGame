using UnityEngine;

public class GameModeConfig : MonoBehaviour
{
        public enum Mode { TwoPlayers, VsCPU }
        public static Mode Selected = Mode.TwoPlayers;
    }