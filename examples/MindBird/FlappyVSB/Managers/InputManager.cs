using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird.Managers
{
    public class InputManager
    {
        // Input states
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        private MouseState _previousMouseState;
        private MouseState _currentMouseState;

        private MouseState _previousRightMouseState;
        private MouseState _currentRightMouseState;

        private GamePadState _previousGamepadState;
        private GamePadState _currentGamepadState;

        public InputManager()
        {
            Statics.MANAGER_INPUT = this;
        }

        public void Update()
        {
            // Get gamepad state

            if (_currentGamepadState != null)
                _previousGamepadState = _currentGamepadState;

            //_currentGamepadState = GamePad.GetState(PlayerIndex.One);

            // Get keyboard state
            
            if (_currentKeyboardState != null)
                _previousKeyboardState = _currentKeyboardState;

            _currentKeyboardState = Keyboard.GetState();

            // Get left mouse button state
            if (_currentMouseState != null)
                _previousMouseState = _currentMouseState;

            _currentMouseState = Mouse.GetState();

            // Get right mouse button state
            if (_currentRightMouseState != null)
                _previousRightMouseState = _currentRightMouseState;

            _currentRightMouseState = Mouse.GetState();
        }

        #region Mouse

        public bool IsLeftMouseClicked()
        {
            if (_previousMouseState.LeftButton == ButtonState.Released && _currentMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
                return false;
        }

        public bool IsRightMouseClicked()
        {
            if (_previousMouseState.RightButton == ButtonState.Released && _currentMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else
                return false;
        }

        public MouseState CurrentMouseState()
        {
            return _currentMouseState;
        }

        public Vector2 GetCursorPosition()
        {
            return new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
        }

        #endregion

        #region Keyboard

        public bool IsKeyPressed(Keys k)
        {
            return (_previousKeyboardState.IsKeyUp(k) && _currentKeyboardState.IsKeyDown(k));
        }

        public bool IsKeyReleased(Keys k)
        {
            return (_previousKeyboardState.IsKeyDown(k) && _currentKeyboardState.IsKeyUp(k));
        }

        public KeyboardState CurrentKeyboardState()
        {
            return _currentKeyboardState;
        }

        #endregion

        #region Gamepad

        public bool IsGamepadPressed(Buttons button)
        {
            return (_previousGamepadState.IsButtonDown(button) && _currentGamepadState.IsButtonUp(button));
        }

        public bool IsGamepadReleased(Buttons button)
        {
            return (_previousGamepadState.IsButtonUp(button) && _currentGamepadState.IsButtonDown(button));
        }

        public GamePadState CurrentGamePadState()
        {
            return _currentGamepadState;
        }

        #endregion
    }
}
