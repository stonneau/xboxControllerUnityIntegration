using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{

    class Imports
    {
        internal const string DLLName = "XInputInterface";

        [DllImport(DLLName)]
        public static extern uint XInputGamePadGetState(uint playerIndex, IntPtr state);
        [DllImport(DLLName)]
        public static extern void XInputGamePadSetState(uint playerIndex, float leftMotor, float rightMotor);
    }

    public enum ButtonState
    {
        Pressed,
        Released
    }

    public struct GamePadButtons
    {
        ButtonState start, back, leftStick, rightStick, leftShoulder, rightShoulder, a, b, x, y;

        internal GamePadButtons(ButtonState start, ButtonState back, ButtonState leftStick, ButtonState rightStick,
                                ButtonState leftShoulder, ButtonState rightShoulder, ButtonState a, ButtonState b,
                                ButtonState x, ButtonState y)
        {
            this.start = start;
            this.back = back;
            this.leftStick = leftStick;
            this.rightStick = rightStick;
            this.leftShoulder = leftShoulder;
            this.rightShoulder = rightShoulder;
            this.a = a;
            this.b = b;
            this.x = x;
            this.y = y;
        }

        public ButtonState Start
        {
            get { return start; }
        }

        public ButtonState Back
        {
            get { return back; }
        }

        public ButtonState LeftStick
        {
            get { return leftStick; }
        }

        public ButtonState RightStick
        {
            get { return rightStick; }
        }

        public ButtonState LeftShoulder
        {
            get { return leftShoulder; }
        }

        public ButtonState RightShoulder
        {
            get { return rightShoulder; }
        }

        public ButtonState A
        {
            get { return a; }
        }

        public ButtonState B
        {
            get { return b; }
        }

        public ButtonState X
        {
            get { return x; }
        }

        public ButtonState Y
        {
            get { return y; }
        }
    }

    public struct GamePadDPad
    {
        ButtonState up, down, left, right;

        internal GamePadDPad(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public ButtonState Up
        {
            get { return up; }
        }

        public ButtonState Down
        {
            get { return down; }
        }

        public ButtonState Left
        {
            get { return left; }
        }

        public ButtonState Right
        {
            get { return right; }
        }
    }

    public struct GamePadThumbSticks
    {
        public struct StickValue
        {
            float x, y;

            internal StickValue(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float X
            {
                get { return x; }
            }

            public float Y
            {
                get { return y; }
            }
        }

        StickValue left, right;

        internal GamePadThumbSticks(StickValue left, StickValue right)
        {
            this.left = left;
            this.right = right;
        }

        public StickValue Left
        {
            get { return left; }
        }

        public StickValue Right
        {
            get { return right; }
        }
    }

    public struct GamePadTriggers
    {
        float left;
        float right;

        internal GamePadTriggers(float left, float right)
        {
            this.left = left;
            this.right = right;
        }

        public float Left
        {
            get { return left; }
        }

        public float Right
        {
            get { return right; }
        }
    }

    public enum ButtonsConstants
    {
        DPadUp = 0x00000001,
        DPadDown = 0x00000002,
        DPadLeft = 0x00000004,
        DPadRight = 0x00000008,
        Start = 0x00000010,
        Back = 0x00000020,
        LeftThumb = 0x00000040,
        RightThumb = 0x00000080,
        LeftShoulder = 0x0100,
        RightShoulder = 0x0200,
        A = 0x1000,
        B = 0x2000,
        X = 0x4000,
        Y = 0x8000,
        LeftTrigger = 0x10000,
        RightTrigger = 0x20000
    }    
    /*{
        DPadUp = 0x00001,
        DPadDown = 0x00002,
        DPadLeft = 0x00004,
        DPadRight = 0x00008,
        Start = 0x00010,
        Back = 0x00020,
        LeftThumb = 0x00040,
        RightThumb = 0x00080,
        LeftShoulder = 0x00100,
        RightShoulder = 0x00200,
        A = 0x01000,
        B = 0x02000,
        X = 0x04000,
        Y = 0x08000,
        LeftTrigger = 0x10000,
        RightTrigger = 0x20000
    }*/

    public struct GamePadState
    {
        internal struct RawState
        {
            public uint dwPacketNumber;
            public GamePad Gamepad;

            public struct GamePad
            {
                public ushort dwButtons;
                public byte bLeftTrigger;
                public byte bRightTrigger;
                public short sThumbLX;
                public short sThumbLY;
                public short sThumbRX;
                public short sThumbRY;
            }
        }

        bool isConnected;
        uint packetNumber;
        GamePadButtons buttons;
        GamePadDPad dPad;
        GamePadThumbSticks thumbSticks;
        GamePadTriggers triggers;
        internal RawState rawState;

        internal GamePadState(bool isConnected, RawState rawState, GamePadDeadZone deadZone)
        {
            this.isConnected = isConnected;
            this.rawState = rawState;
            if (!isConnected)
            {
                rawState.dwPacketNumber = 0;
                rawState.Gamepad.dwButtons = 0;
                rawState.Gamepad.bLeftTrigger = 0;
                rawState.Gamepad.bRightTrigger = 0;
                rawState.Gamepad.sThumbLX = 0;
                rawState.Gamepad.sThumbLY = 0;
                rawState.Gamepad.sThumbRX = 0;
                rawState.Gamepad.sThumbRY = 0;
            }

            packetNumber = rawState.dwPacketNumber;
            buttons = new GamePadButtons(
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.Start) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.Back) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.LeftThumb) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.RightThumb) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.LeftShoulder) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.RightShoulder) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.A) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.B) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.X) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.Y) != 0 ? ButtonState.Pressed : ButtonState.Released
            );
            dPad = new GamePadDPad(
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.DPadUp) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.DPadDown) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.DPadLeft) != 0 ? ButtonState.Pressed : ButtonState.Released,
                (rawState.Gamepad.dwButtons & (uint)ButtonsConstants.DPadRight) != 0 ? ButtonState.Pressed : ButtonState.Released
            );

            thumbSticks = new GamePadThumbSticks(
                Utils.ApplyLeftStickDeadZone(rawState.Gamepad.sThumbLX, rawState.Gamepad.sThumbLY, deadZone),
                Utils.ApplyRightStickDeadZone(rawState.Gamepad.sThumbRX, rawState.Gamepad.sThumbRY, deadZone)
            );
            triggers = new GamePadTriggers(
                Utils.ApplyTriggerDeadZone(rawState.Gamepad.bLeftTrigger, deadZone),
                Utils.ApplyTriggerDeadZone(rawState.Gamepad.bRightTrigger, deadZone)
            );
        }

        public uint PacketNumber
        {
            get { return packetNumber; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public GamePadButtons Buttons
        {
            get { return buttons; }
        }

        public GamePadDPad DPad
        {
            get { return dPad; }
        }

        public GamePadTriggers Triggers
        {
            get { return triggers; }
        }

        public GamePadThumbSticks ThumbSticks
        {
            get { return thumbSticks; }
        }

        public bool AllButtons(uint buttons)
        {
            // ok, first triggers are not included in buttons, so handle this case first;
            if ((buttons & (uint)ButtonsConstants.LeftTrigger) != 0 && triggers.Left == 0)
            {
                return false;
            }
            if ((buttons & (uint)ButtonsConstants.RightTrigger) != 0 && triggers.Right == 0)
            {
                return false;
            }
            return ((buttons & 0xFFFF) == 0) || ((rawState.Gamepad.dwButtons ^ (buttons & 0xFFFF)) == 0);
        }

        public bool AnyButton(uint buttons)
        {
            if ((buttons & (uint)ButtonsConstants.LeftTrigger) != 0 && triggers.Left != 0)
            {
                return true;
            }
            if ((buttons & (uint)ButtonsConstants.RightTrigger) != 0 && triggers.Right != 0)
            {
                return true;
            }
            return ((rawState.Gamepad.dwButtons & (buttons & 0xFFFF)) != 0);
        }

        public bool AnyButton()
        {
	        //uint buttons = ~(uint)(0);
            return AnyButton(0xFFFF);
        }
    }

    public enum PlayerIndex
    {
        One = 0,
        Two,
        Three,
        Four
    }

    public enum GamePadDeadZone
    {
        Circular,
        IndependentAxes,
        None
    }

    public class GamePad
    {
        public static GamePadState GetState(PlayerIndex playerIndex)
        {
            return GetState(playerIndex, GamePadDeadZone.IndependentAxes);
        }

        public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZone)
        {
            IntPtr gamePadStatePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GamePadState.RawState)));
            uint result = Imports.XInputGamePadGetState((uint)playerIndex, gamePadStatePointer);
            GamePadState.RawState state = (GamePadState.RawState)Marshal.PtrToStructure(gamePadStatePointer, typeof(GamePadState.RawState));
            return new GamePadState(result == Utils.Success, state, deadZone);
        }

        public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            Imports.XInputGamePadSetState((uint)playerIndex, leftMotor, rightMotor);
        }
    }
}
