using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace InputExample
{
    public  class InputManager
    {
        KeyboardState currentkeyboardState;
        KeyboardState priorKeyboardState;
        MouseState currentMouseState;
        MouseState priorMouseState;
        GamePadState currentPadState;
        GamePadState priorPadState;
        /// <summary>
        /// the current direction
        /// </summary>
        public Vector2 Direction { get; private set; }
        /// <summary>
        /// if the warp functionally has been requested
        /// </summary>
        public bool Warp { get; private set; }
        /// <summary>
        /// if the user has requested the game end
        /// </summary>
        public bool Exit { get; private set; } = false;
  
        public void Update(GameTime gameTime)
        {
            float Negative = -100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float Positive = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            #region Input State 
            priorKeyboardState = currentkeyboardState;
            priorMouseState = currentMouseState;
            priorPadState = currentPadState;

            currentkeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            currentPadState = GamePad.GetState(0);
            #endregion

            #region Direction State
            //get position from gamepad 

            Direction = currentPadState.ThumbSticks.Right * Positive;

            //position from keyboard
            
           
            if (currentkeyboardState.IsKeyDown(Keys.Left) || currentkeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(Negative, 0);
            }
            if (currentkeyboardState.IsKeyDown(Keys.Right) || currentkeyboardState.IsKeyDown(Keys.D))
            {
               Direction += new Vector2(Positive, 0);
            }
            if (currentkeyboardState.IsKeyDown(Keys.Up) || currentkeyboardState.IsKeyDown(Keys.W))
            {
               Direction += new Vector2(0, Negative);
            }
            if (currentkeyboardState.IsKeyDown(Keys.Down) || currentkeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, Positive);
            }
            #endregion

            #region Warp input
            Warp = false;
            if (currentkeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                Warp = true;
            }
            if (currentPadState.IsButtonDown(Buttons.A) && priorPadState.IsButtonUp(Buttons.A))
            {
                Warp = true;
            }
            #endregion

            #region exit
            if(currentPadState.Buttons.Back == ButtonState.Pressed || currentkeyboardState.IsKeyDown(Keys.Escape)){
                Exit = true;
            }
            #endregion


        }
    }

}
