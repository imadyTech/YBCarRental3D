using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;

namespace YBCarRental3D
{
    public class YB_ViewModelBasis<TData>: IYB_DataSource 
        where TData : YB_DataBasis
    {
        public TData principalObject;

        public YB_ViewModelBasis()
        {
        }

        public YB_Window Window { get => YB_Window.Instance; }

        public virtual string Get_PropertyValue(string bindName)
        {
            return principalObject?.FindValue(bindName);
        }

        public virtual List<YB_DataBasis> Get_QueryList(int page, int pageSize) {
            throw new NotImplementedException();
        }
        public virtual YB_DataBasis Get_PrincipalData() { return this.principalObject; }

        public virtual void onInit() { 
        }
        public virtual void onSubmit(Dictionary<string, string> valuesMapPtr){
        }
        public virtual void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (TData)fromData;
        }
        public virtual void onContentUpdated(string bindName, string newValue)
        {

        }
        public virtual void onButtonClicked(string buttonName)
        { }

        public virtual void onYesClicked() { }
        public virtual void onNoClicked(){}
        public virtual void onOkClicked(){}

    }
}