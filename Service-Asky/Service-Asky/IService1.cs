using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Service_Asky.Tables;

namespace Service_Asky
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        VSystem_AskiDataBase getVSystem(VSystem_AskiDataBase vsystem);

        [OperationContract]
        Usuario getUser(Usuario user);

        // TODO: Add your service operations here

        [OperationContract]
        void addDepartamentos(string descripcion);
        [OperationContract]
        void addRoles(string descripcion);

        [OperationContract]
        void addUsuario(int talento_humano, string email, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, DateTime fecha_ingreso, string password);

        [OperationContract]
        void addPermisos(string descripcion);

        [OperationContract]
        void editPermisos(int id, string descripcion, bool Test);
        [OperationContract]
        void editRoles(int id, string descripcion);
        [OperationContract]
        void editDepartamentos(int id, string descripcion);

        [OperationContract]
        void deletePermisos(int id);

        [OperationContract]
        bool confirmarLogin(string email, string password);

        [OperationContract]
        string getPermisosInfo(int id);
        [OperationContract]
        string getRolesInfo(int id);

        [OperationContract]
        string getDepartamentosInfo(int id);

      
    }
    
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    
}
