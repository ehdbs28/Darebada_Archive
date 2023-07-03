using System;
using System.Collections;
using UnityEngine;

namespace Core{
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

        private static Vector3 _north = Vector3.forward;
        public static Vector3 North => _north;
    }

    public class GameTime{
        public static readonly float DayDelay = 720f;
        public static float HourDelay => DayDelay / 24;
        public static float MinuteDelay => HourDelay / 12;
        
        public static int[] DayPerMonth = { 31, 31, IsLeapYear(GameManager.Instance.GetManager<TimeManager>().DateTime.Year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30 };

        public static bool IsLeapYear(int year){
            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }
    }

    public struct UtilFunc{
        static IEnumerator DelayCoroutine(float delay, Action Callback){
            yield return new WaitForSeconds(delay);
            Callback?.Invoke();
        }
    }

    public class GameDate{
        public int Year;
        public int Month;
        public int Day;

        public GameDate(int year, int month, int day){
            Year = year;
            Month = month;
            Day = day;
        }
    }
}