using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{


    /// <summary>
    /// define a base struct of custom erros -- to make life easier
    /// </summary>
    public class YB_ErrorBasis : Exception
    {
        public YB_ErrorBasis() { }

        public YB_ErrorBasis(string message) : base(message) { }
    }



    //------------------------Repository custom error types------------------------
    public class YB_RepositoryError : YB_ErrorBasis
    {
        public YB_RepositoryError() : base("YB_RepositoryError") { }
        public YB_RepositoryError(string msg) : base(msg) { }
    };

    public class YB_RepoRecordExistedError : YB_ErrorBasis
    {
        public YB_RepoRecordExistedError() : base("YB_Repo:Record Already Existed.") { }
        public YB_RepoRecordExistedError(string msg) : base(msg) { }
    };

    public class YB_RepoRecordNotExistedError : YB_ErrorBasis
    {
        public YB_RepoRecordNotExistedError() : base("YB_Repo:Record Does Not Existed.") { }
        public YB_RepoRecordNotExistedError(string msg) : base(msg) { }
    };


    //------------------------Datamodels custom error types------------------------
    public class YB_DataModelError : YB_ErrorBasis
    {
        public YB_DataModelError() : base("YB_DataModelError") { }
        public YB_DataModelError(string msg) : base(msg) { }
    };

    public class YB_SerializeError : YB_ErrorBasis
    {
        public YB_SerializeError() : base("YB_SerializeError") { }
        public YB_SerializeError(string msg) : base(msg) { }
    };

    public class YB_DeSerializeError : YB_ErrorBasis
    {
        public YB_DeSerializeError() : base("YB_DeSerializeError") { }
        public YB_DeSerializeError(string msg) : base(msg) { }
    };

    public class YB_BindingError : YB_ErrorBasis
    {
        public YB_BindingError() : base("YB_BindingError") { }
        public YB_BindingError(string msg) : base(msg) { }
    };

    public class YB_ReverseBindingError : YB_ErrorBasis
    {
        public YB_ReverseBindingError() : base("YB_ReverseBindingError") { }
        public YB_ReverseBindingError(string msg) : base(msg) { }
    };



    public class YB_PersistorError : YB_ErrorBasis
    {
        public YB_PersistorError() : base("YB_PersistorError") { }
        public YB_PersistorError(string msg) : base(msg) { }
    };

    public class YB_FactoryError : YB_ErrorBasis
    {
        public YB_FactoryError() : base("YB_FactoryError") { }
        public YB_FactoryError(string msg) : base(msg){ }
    };

    public class YB_ManagerLogicError : YB_ErrorBasis
    {
        public YB_ManagerLogicError() : base("YB_ManagerLogicError") { }
        public YB_ManagerLogicError(string msg) : base(msg) { }
    };

    public class YB_ViewError : YB_ErrorBasis
    {
        public YB_ViewError() : base("YB_ViewError") { }
        public YB_ViewError(string msg) : base(msg) { }
    };

}