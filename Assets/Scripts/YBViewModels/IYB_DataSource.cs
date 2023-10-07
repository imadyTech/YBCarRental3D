﻿using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace YBCarRental3D
{
    using LIST_HEAD_TEMPLATE = List<KeyValuePair<string, int>>;
    using LIST_ITEM_VALUE = KeyValuePair<string, string>;
    using LIST_ITEM_VALUES = List<KeyValuePair<string, string>>;
    using FORMATED_LIST_ITEM_VALUE = Tuple<string, string, int>;
    using FORMATED_LIST_VIEW_VALUES = List<Tuple<string, string, int>>;

    public interface IYB_DataSource
    {
        string Get_PropertyValue(string bindNamePtr);
        //query an object and fill the result as indicated in a List ref
        //bool Get_QuerySingle(int Id, ref List<KeyValuePair<string, string>> pairs);
        //query the source with a index (NOT Id) and fill the result to a List
        //bool Get_QueryByIndex(int index, List<LIST_ITEM_VALUE> result);
        //???
        List<YB_DataBasis> Get_QueryList(int page, int pageSize);
        //return the current principalData only.
        YB_DataBasis Get_PrincipalData();
        //query an return an object by the object.Id, AND store the object as principalData at meantime.
        //YB_DataBasis Get_PrincipalData(int Id);
        //void Set_PropertyValue(string bindNamePtr, string valuePtr);
        //void Set_PropertyValues(Dictionary<string, string> valuesPtr);

        //Fired in ViewBasis.Init()
        //Dictionary<string, string> onListInitiated(string tableHeadNames);                              //tableheadNames format: Model/Make/Mileage
        //Dictionary<string, string> onListInitiated(string tableHeadNames, int pageNum, int size);       //Table paging, Todo...
        void onInit();
        void onSubmit(Dictionary<string, string> values);
        void onViewForwarded(YB_DataBasis fromData);
        void onContentUpdated(string bindName, string newValue);
        //void onItemFocused(string bindName);
        //void onItemSelected(string bindName);
        void onButtonClicked(string buttonName);
        void onYesClicked();
        void onNoClicked();
        void onOkClicked();

        void ConfigViewDef(YB_ViewBasis view);

        void ConfigViewItemObj(YB_ViewItemBasis viewItemDef, ref GameObject itemObj);
    }
}