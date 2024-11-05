using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nova
{
    [Serializable]
    public class MinigameParameters{
        public readonly bool real;

        public MinigameParameters(){
            real = true;
        }
    }
    [Serializable]
    public class MinigameEvent : UnityEvent<MinigameParameters> { }

    
    public class Minigame : MonoBehaviour//[ExportCustomType]
    {
        public MinigameEvent minigameFunction;
        public static MinigameEvent _minigame;

        private void Awake(){
            if(minigameFunction == null){
                Debug.Log("미니게임 오류");
                Utils.Quit();
            }

            _minigame = minigameFunction;
        }

        public static void GameStart(){
            _minigame.Invoke(new MinigameParameters());
        }
    }
}
