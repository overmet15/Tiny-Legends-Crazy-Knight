using UnityEngine;

public class Crazy_UIButtonClickEx : TUIButtonClick
{
	public override bool HandleInput(TUIInput input)
	{
		if (disabled)
		{
			return false;
		}
		if (input.inputType == TUIInputType.Began)
		{
			if (PtInControl(input.position))
			{
				pressed = true;
				fingerId = input.fingerId;
				UpdateFrame();
				PostEvent(this, 1, 0f, 0f, null);
				return true;
			}
			return false;
		}
		if (input.fingerId == fingerId)
		{
			if (input.inputType == TUIInputType.Moved)
			{
				if (PtInControl(input.position))
				{
					if (!pressed)
					{
						pressed = true;
						UpdateFrame();
						PostEvent(this, 1, 0f, 0f, null);
					}
				}
				else if (pressed)
				{
					pressed = false;
					UpdateFrame();
					PostEvent(this, 2, 0f, 0f, null);
				}
			}
			else if (input.inputType == TUIInputType.Ended)
			{
				pressed = false;
				fingerId = -1;
				if (PtInControl(input.position))
				{
					UpdateFrame();
					PostEvent(this, 2, 0f, 0f, null);
					PostEvent(this, 3, 0f, 0f, null);
					SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					UpdateFrame();
					PostEvent(this, 2, 0f, 0f, null);
				}
			}
			return true;
		}
		return false;
	}
}
