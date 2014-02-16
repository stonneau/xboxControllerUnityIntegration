using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
    public class AdvanceGamePad
    {
        public readonly PlayerIndex playerIndex_;

        private GamePadState old_;
        private GamePadState new_;
        private GamePadDeadZone deadZone_;

        public AdvanceGamePad(PlayerIndex playerIndex)
            : this(playerIndex, GamePadDeadZone.None)
        {
            // NOTHING
        }

        public AdvanceGamePad(PlayerIndex playerIndex, GamePadDeadZone deadZone)
        {
            playerIndex_ = playerIndex;
            deadZone_ = deadZone;
            old_ = GamePad.GetState(playerIndex, deadZone);
            new_ = old_;
        }

        public void Update()
        {
            old_ = new_;
            new_ = GamePad.GetState(playerIndex_, deadZone_);
        }

        public bool AllPressed(uint buttons)
        {
            return old_.AllButtons(buttons) & new_.AllButtons(buttons);
        }

	    public bool AnyTriggered(uint buttons)
        {
            return new_.AnyButton(buttons) & (!old_.AnyButton(buttons));
        }
        
	    public bool AnyPressed(uint buttons)
        {
            return new_.AnyButton(buttons) & old_.AnyButton(buttons);
        }

        /*true only if all buttons were pressed*/
	    public bool AnyReleased(uint buttons)
        {
            return old_.AllButtons(buttons) & !new_.AllButtons(buttons);
        }
	    
	    /*none of the buttons indicated are pressed*/
	    public bool NonePressed(uint buttons)
        {
            return !new_.AnyButton(buttons);
        }

        /*No button at all is pressed*/
        public bool NonePressed()
        {
            return !new_.AnyButton();
        }

	    public bool AnyPressed()
        {
	        return  new_.AnyButton();
        }

	    /*0 means no vibration, 1 is max*/
        public void Vibrate(float leftVal, float rightVal)
        {
            GamePad.SetVibration(playerIndex_, leftVal, rightVal);
        }

        #region redirecting all accessors from gamePadState
        public uint PacketNumber
        {
            get { return new_.PacketNumber; }
        }

        public bool IsConnected
        {
            get { return new_.IsConnected; }
        }

        public GamePadButtons Buttons
        {
            get { return new_.Buttons; }
        }

        public GamePadDPad DPad
        {
            get { return new_.DPad; }
        }

        public GamePadTriggers Triggers
        {
            get { return new_.Triggers; }
        }

        public GamePadThumbSticks ThumbSticks
        {
            get { return new_.ThumbSticks; }
        }
        #endregion
    }
	
}
