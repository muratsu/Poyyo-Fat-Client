﻿/***************************************************************************
 *   LoginGump.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using UltimaXNA.Graphics;
using UltimaXNA.UILegacy.Gumplings;

namespace UltimaXNA.UILegacy.ClientsideGumps
{
    public delegate void LoginEvent(string server, int port, string account, string password);

    enum LoginGumpButtons
    {
        QuitButton = 0,
        LoginButton = 1
    }
    enum LoginGumpTextFields
    {
        AccountName = 0,
        Password = 1
    }

    public class LoginGump : Gump
    {
        public LoginEvent OnLogin;

        public LoginGump()
            : base(0, 0)
        {
            int hue = 1132; // dark brown
            _renderFullScreen = false;
            // backdrop
            AddControl(new GumpPic(this, 0, 0, 0, 9001,0));
            // quit button
            AddControl(new Button(this, 0, 554, 2, 5513, 5515, ButtonTypes.Activate, 0, (int)LoginGumpButtons.QuitButton));
            ((Button)LastControl).GumpOverID = 5514;
            // Log in to Ultima Online
            AddControl(new TextLabelAscii(this, 0, 254, 305, hue, 2, Data.StringList.Entry(3000038)));
            // Account Name
            AddControl(new TextLabelAscii(this, 0, 181, 346, hue, 2, Data.StringList.Entry(3000099)));
            // Password
            AddControl(new TextLabelAscii(this, 0, 181, 386, hue, 2, Data.StringList.Entry(3000103)));
            // name field
            TextEntry g1 = new TextEntry(this, 0, 332, 346, 200, 20, 0, (int)LoginGumpTextFields.AccountName, 32, "Admin");
            g1.HtmlTag = "<basefont color=#000000><big>";
            AddControl(new ResizePic(this, g1));
            AddControl(g1);
            // password field
            TextEntry g2 = new TextEntry(this, 0, 332, 386, 200, 20, 0, (int)LoginGumpTextFields.Password, 32, "123");
            g2.IsPasswordField = true;
            g2.HtmlTag = "<basefont color=#000000><big>";
            AddControl(new ResizePic(this, g2));
            AddControl(g2);
            // login button
            AddControl(new Button(this, 0, 610, 435, 5540, 5542, ButtonTypes.Activate, 0, (int)LoginGumpButtons.LoginButton));
            ((Button)LastControl).GumpOverID = 5541;
            // Version information
            AddControl(new TextLabelAscii(this, 0, 183, 421, hue, 9, Utility.VersionString));
        }

        public override void ActivateByButton(int buttonID)
        {
            switch ((LoginGumpButtons)buttonID)
            {
                case LoginGumpButtons.QuitButton:
                    Quit();
                    break;
                case LoginGumpButtons.LoginButton:
                    string accountName = getTextEntry((int)LoginGumpTextFields.AccountName);
                    string password = getTextEntry((int)LoginGumpTextFields.Password);
                    OnLogin("localhost", 2593, accountName, password);
                    break;
            }
        }
        public override void ActivateByKeyboardReturn(int textID, string text)
        {
            ActivateByButton((int)LoginGumpButtons.LoginButton);
        }

        public override void Draw(SpriteBatchUI spriteBatch)
        {
            base.Draw(spriteBatch);
            // DEBUG !!! Draws a dragon
            // Data.FrameXNA[] f = Data.AnimationsXNA.GetAnimation(59, 0, 0, 0, false);
            // spriteBatch.Draw(f[0].Texture, new Microsoft.Xna.Framework.Vector2(10, 10), 0, false);
        }
    }
}
