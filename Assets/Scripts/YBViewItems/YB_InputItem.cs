﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_InputItem : YB_ViewItemBasis
    {
        public YB_InputItem() { }
        public YB_InputItem(string def) : this()
        {
            base.Deserialize(def);
        }

        public override YB_ViewItemBasis BindContent()
        {
            return base.BindContent();
        }
    }
}
