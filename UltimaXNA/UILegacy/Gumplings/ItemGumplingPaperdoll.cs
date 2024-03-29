﻿/***************************************************************************
 *   ItemGumplingPaperdoll.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using Microsoft.Xna.Framework;
using UltimaXNA.Entities;
using UltimaXNA.Graphics;

namespace UltimaXNA.UILegacy.Gumplings
{
    class ItemGumplingPaperdoll : ItemGumpling
    {
        public int SlotIndex = 0;
        public bool IsFemale = false;

        private int _x, _y;
        private bool _isBuilt = false;

        public ItemGumplingPaperdoll(Control owner, int x, int y, Item item)
            : base(owner, item)
        {
            _x = x;
            _y = y;
        }

        protected override void _onPickUp()
        {
            if (_manager.Cursor.HoldingItem != null)
            {
                // fix this to be more centered on the object.
                _manager.Cursor.HoldingOffset = new Point(
                    _manager.Cursor.HoldingTexture.Width / 4,
                    _manager.Cursor.HoldingTexture.Height / 4);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!_isBuilt)
            {
                Position = new Point2D(_x, _y);
            }
        }

        public override void Draw(SpriteBatchUI spriteBatch)
        {
            
            if (_texture == null)
            {
                if (IsFemale)
                    _texture = Data.Gumps.GetGumpXNA(Item.ItemData.AnimID + 60000);
                if (_texture == null)
                    _texture = Data.Gumps.GetGumpXNA(Item.ItemData.AnimID + 50000);
                Size = new Point2D(_texture.Width, _texture.Height);
            }
            spriteBatch.Draw2D(_texture, Position, Item.Hue & 0x7FFF, (Item.Hue & 0x8000) == 0x8000 ? true : false, false);
            base.Draw(spriteBatch);
        }

        protected override void itemDrop(UltimaXNA.Entities.Item item, int x, int y)
        {

        }
    }
}
