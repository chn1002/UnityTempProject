using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMoniterManager {
    private const int LINE_GAP = 40;
    private int mFontSize = 20;
    private Color mFontColor;

    public IMoniterManager()
    {
        mFontColor = Color.white;
    }

    public void setFontColor(string color)
    {
        if (color.ToLower().Equals("black"))
        {
            mFontColor = Color.black;
        }
        else if (color.ToLower().Equals("red"))
        {
            mFontColor = Color.red;
        }
        else if (color.ToLower().Equals("blue"))
        {
            mFontColor = Color.blue;
        }
        else if (color.ToLower().Equals("white"))
        {
            mFontColor = Color.white;
        }
        else
        {
            mFontColor = Color.white;
        }
    }

    public void setFontSize(int fontSize)
    {
        mFontSize = LINE_GAP + (fontSize - 1);
    }

    public Color getFontColor()
    {
        return mFontColor;
    }

    public Rect showGUIMessage(int line)
    {
        int line_gap_value = (mFontSize * (line));

        return new Rect(10, 10 + line_gap_value, 400, 40);

    }
}
