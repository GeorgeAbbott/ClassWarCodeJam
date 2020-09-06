using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Class_War
{
    class InputHandler
    {
        private KeyboardState _prevState;
        public KeyboardState PreviousState { get => _prevState; set => _prevState = value; }

        private KeyboardState _currentState;
        public KeyboardState CurrentState { get => _currentState; set => _currentState = value; }

        public void ReadInput ()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public bool IsKeyDown (Keys key)
        {
            if (CurrentState[key] == KeyState.Down) return true;
            else return false;
        }


        private DateTime _isKeyDownLastCheck = DateTime.Now;
        public DateTime KeyDownLastCheck
        {
            get { return _isKeyDownLastCheck; }
            private set { _isKeyDownLastCheck = DateTime.Now; } 
        }



        /// <summary>
        /// Checks if a key is down, and will return false if the last keypress was earlier than
        /// the timegap parameter. Keep changeKeyDownLastCheckIfFalse as false,
        /// otherwise calling this function will interrupt the checking process.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timegap"></param>
        /// <param name="changeKeyDownLastCheckIfFalse"></param>
        /// <returns></returns>
        public bool IsKeyDown (Keys key, double timegap, bool changeKeyDownLastCheckIfFalse = false)
        {
            // Takes a DateTime, will return false if the DateTime is not after the last check.
            // If enough time has passed, it will perform the check and return either true or false.
            // Will not change KeyDownLastCheck unless true returned, except if changeKeyDownLastCheckIfFalse is true.

            if (!((DateTime.Now - KeyDownLastCheck).TotalSeconds > timegap))
            { // if not enough time has passed since last check
                if (changeKeyDownLastCheckIfFalse) KeyDownLastCheck = DateTime.Now;
                return false;
            }
            else // enough time has passed
            {
                if (CurrentState[key] == KeyState.Down)
                { // Key has been pressed
                    KeyDownLastCheck = DateTime.Now;
                    return true;
                }
                else
                { // Key has not been pressed
                    if (changeKeyDownLastCheckIfFalse)
                    {
                        KeyDownLastCheck = DateTime.Now;
                        return false;
                    }
                    else return false;
                }
            }



            

        }

        public bool OnKeyPress (Keys key)
        {
            if (CurrentState[key] == KeyState.Down && PreviousState[key] == KeyState.Up) return true;
            else return false;
        }

        public bool OnKeyRelease (Keys key)
        {
            if (CurrentState[key] == KeyState.Up && PreviousState[key] == KeyState.Down) return true;
            else return false;
        }

    }
}
