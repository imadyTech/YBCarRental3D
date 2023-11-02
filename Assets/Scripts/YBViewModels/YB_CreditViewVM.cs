using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace YBCarRental3D
{
    public class YB_CreditViewVM : YB_ViewModelBasis<YB_User>
    {
        private void Update()
        {
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
            {
                YB_Window.Instance.Goto(this.viewDef.GotoView);
            }
        }

    }
}
