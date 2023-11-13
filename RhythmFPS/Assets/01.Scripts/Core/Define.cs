using UnityEngine;

namespace Core{
    public enum PlayerType{
        Normal = 0,
    }
    public enum SceneNames
    {
        intro,
        main
    }
    public class Define{
        private static Camera mainCam = null;
        public static Camera MainCam {
            get{
                if(mainCam ==null)
                {
                    mainCam = Camera.main;
                }
                return mainCam;
            }
        }
        public static string PLAYER = "Player";
        public static string GROUND = "Ground";
    }

    
}