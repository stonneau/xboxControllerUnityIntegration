using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XInputTest : MonoBehaviour
{
	AdvanceGamePad pad = new AdvanceGamePad(PlayerIndex.One, GamePadDeadZone.Circular);
	string previousText;
    GameObject textObject;
    GameObject cubeObject;
    float cubeAngle = 0.0f;

    // Use this for initialization
    void Start()
    {
        // No need to initialize anything for the plugin
		previousText = "";
        // However, I need that for the purpose of the demo
        textObject = GameObject.Find("GUI Text");
        cubeObject = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
       	if(!pad.IsConnected)
		{
			Debug.Log(string.Format("GamePad not found"));
			return;
		}
		pad.Update();
		string text = "Use left stick to turn the cube\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", pad.IsConnected, pad.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", pad.Triggers.Left, pad.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", pad.DPad.Up, pad.DPad.Right, pad.DPad.Down, pad.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1}\n", pad.Buttons.Start, pad.Buttons.Back);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", pad.Buttons.LeftStick, pad.Buttons.RightStick, pad.Buttons.LeftShoulder, pad.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", pad.Buttons.A, pad.Buttons.B, pad.Buttons.X, pad.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", pad.ThumbSticks.Left.X, pad.ThumbSticks.Left.Y, pad.ThumbSticks.Right.X, pad.ThumbSticks.Right.Y);
		LogAdvancedCommands();
		text += previousText;
        pad.Vibrate(pad.Triggers.Left, pad.Triggers.Right);
        
		LogAdvancedCommands();

        // Display the pad information
        textObject.guiText.text = text;
        
        // Make the cube turn
        cubeAngle += pad.ThumbSticks.Left.X * 25.0f * Time.deltaTime;
        cubeObject.transform.localRotation = Quaternion.Euler(0.0f, cubeAngle, 0.0f);
    }

	void LogAdvancedCommands()
	{
		string oldText = previousText;
		previousText = "";
		if(pad.IsConnected)
		{
			// TRIGGERED
			if(pad.AnyTriggered((uint)ButtonsConstants.A))
			{
				previousText +="A Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.B))
			{
				previousText +="B Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.X))
			{
				previousText +="X Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.Y))
			{
				previousText +="Y Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.DPadDown))
			{
				previousText +=" DPadDown Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.DPadLeft))
			{
				previousText +=" DPadLeft Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.DPadRight))
			{
				previousText +=" DPadRight Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.DPadUp))
			{
				previousText +=" DPadUp Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.Back))
			{
				previousText +="back Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.Start))
			{
				previousText +="start Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.LeftShoulder))
			{
				previousText +="LeftShoulder Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.RightShoulder))
			{
				previousText +="RightShoulder Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.LeftTrigger))
			{
				previousText +="LeftTrigger Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.RightTrigger))
			{
				previousText +="RightTrigger Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.LeftTrigger | (uint)ButtonsConstants.RightTrigger))
			{
				previousText +="RightTrigger OR LeftTrigger Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.A | (uint)ButtonsConstants.B))
			{
				previousText +="A OR B Triggered\n";
			}
			
			if(pad.AnyPressed((uint)ButtonsConstants.Start | (uint)ButtonsConstants.Back))
			{
				previousText +="Start OR Back pressed\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.LeftThumb))
			{
				previousText +="RightThumb Triggered\n";
			}
			
			if(pad.AnyTriggered((uint)ButtonsConstants.RightThumb))
			{
				previousText +="RightThumb Triggered\n";
			}
			
			// RELEASE
			if(pad.AnyReleased((uint)ButtonsConstants.A))
			{
				previousText +="A Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.B))
			{
				previousText +="B Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.X))
			{
				previousText +="X Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.Y))
			{
				previousText +="Y Released\n";
				pad.Vibrate(0,0);
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.DPadDown))
			{
				previousText +=" DPadDown Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.DPadLeft))
			{
				previousText +=" DPadLeft Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.DPadRight))
			{
				previousText +=" DPadRight Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.DPadUp))
			{
				previousText +=" DPadUp Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.Back))
			{
				previousText +="back Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.Start))
			{
				previousText +="start Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.LeftShoulder))
			{
				previousText +="LeftShoulder Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.RightShoulder))
			{
				previousText +="RightShoulder Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.LeftTrigger))
			{
				previousText +="LeftTrigger Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.RightTrigger))
			{
				previousText +="RightTrigger Released\n";
			}
			if(pad.AnyReleased((uint)ButtonsConstants.LeftTrigger | (uint)ButtonsConstants.RightTrigger))
			{
				previousText +="LeftTrigger OR RightTrigger Released\n";
			}
			if(pad.AnyReleased((uint)ButtonsConstants.A | (uint)ButtonsConstants.B))
			{
				previousText +="A OR B Released\n";
			}
			if(pad.AnyReleased((uint)ButtonsConstants.LeftThumb))
			{
				previousText +="RightThumb Released\n";
			}
			
			if(pad.AnyReleased((uint)ButtonsConstants.RightThumb))
			{
				previousText +="RightThumb Released\n";
			}
			
			// Pressed
			if(pad.AllPressed((uint)ButtonsConstants.A | (uint)ButtonsConstants.LeftTrigger))
			{
				previousText +="A and LeftTrigger Pressed\n";
			}
			
			if(pad.AllPressed((uint)ButtonsConstants.LeftTrigger | (uint)ButtonsConstants.RightTrigger))
			{
				previousText +="LeftTrigger AND right Pressed\n";
			}
			
			if(pad.AnyPressed((uint)ButtonsConstants.DPadDown | (uint)ButtonsConstants.DPadLeft))
			{
				previousText +="down or left pressed Pressed\n";
			}
			
			// VIBRATION + MULTITEST
			if(pad.AllPressed((uint)ButtonsConstants.A | (uint)ButtonsConstants.B))
			{
				previousText +="A and B Pressed\n";
			}
			if(pad.AllPressed((uint)ButtonsConstants.A | (uint)ButtonsConstants.X))
			{
				previousText +="A and X Pressed\n";
			}
			if(pad.AllPressed((uint)ButtonsConstants.X | (uint)ButtonsConstants.B))
			{
				previousText +="X and B Pressed\n";
			}
			if(pad.AnyPressed())
			{
				//previousText +="one pressed Pressed\n";
			}
			if(pad.NonePressed())
			{
				//previousText +="None Pressed\n";
			}
			if(pad.NonePressed((uint)ButtonsConstants.X | (uint)ButtonsConstants.B))
			{
				//previousText +="X and B not Pressed\n";
			}
			if(previousText == "")
			{
				previousText = oldText;
			}
		}
	}

}
