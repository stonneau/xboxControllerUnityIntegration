using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
    public class AdvanceGamePad
    {
        public readonly PlayerIndex playerNumber_;

        private GamePadState old_;


        public AdvanceGamePad(PlayerIndex playerNumber)
        {
            playerNumber_ = playerNumber;
        }
	
	/*Requests*/
	bool IsConnected() const;	
	/*Checks leftstick first*/
	void GetLeftStickMovingVector(float& /*x*/, float& /*y*/) const;	
	/*Checks rightstick*/
	void GetRightStickMovingVector(float& /*x*/, float& /*y*/) const;
	// true if above treshold
	bool GetRightTriggerPressure(float& /*x*/) const;
	bool GetLeftTriggerPressure(float& /*x*/) const;
	/*true if combinaison of all buttons are pressed*/
	/*bool AllTriggered(const int button) const;*/ // this one is never going to happen
	bool AllPressed(const int button) const;
	/*bool AllReleased(const int button) const; */ // this one is never going to happen

	/*true if any of button combinaison is working*/
	bool AnyTriggered(const int button) const;
	bool AnyPressed(const int button) const;
	/*true only if all buttons were pressed*/
	bool AnyReleased(const int button) const;

	/*none of the buttons indicated are pressed*/
	bool NonePressed(const int button) const;
	/*No button at all is pressed*/
	bool NonePressed() const;
	bool AnyPressed() const;

	/*0 if not pressed, else says for how long*/
	float ButtonPressedTime(const Buttons::Buttons /*button*/) const;

	/*0 means no vibration, 1 is max*/
	int Vibrate(const float /*leftVal*/, const float /*rightVal*/);

	void Update(float gameTime = 1.f);
    }
}
