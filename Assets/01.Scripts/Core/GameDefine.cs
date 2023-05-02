using UnityEngine;

namespace Core{
    public enum CameraState{
        BOAT_FOLLOW = 0,
        BOBBER_FOLLOW,
        ROTATE,
        FINISH
    }

    public class Define{
        private static Camera _mainCam = null;
        public static Camera MainCam{
            get{
                if(_mainCam == null){
                    _mainCam = Camera.main;
                }

                return _mainCam;
            }
        }

        private static Vector2 _screenSize = Vector2.zero;
        public static Vector2 ScreenSize{
            get{
                if(_screenSize == Vector2.zero){
                    _screenSize = new Vector2(Screen.width, Screen.height);
                }

                return _screenSize;
            }
        }
    }
}