﻿using System;
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

        // TODO: Add your service operations here

        //=================== Add Element to database=============
        
        [OperationContract]
        void addDepartamento(string descripcion);
        [OperationContract]
        void addRole(string descripcion);
        [OperationContract]
        void addUsuario(int talento_humano, string email, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, DateTime fecha_ingreso, string password);
        [OperationContract]
        void addPermiso(string descripcion);





        //=================== Edit Element from database=============

        [OperationContract]
        void editPermiso(int id, string descripcion, bool activo);
        [OperationContract]
        void editRol(int id, string descripcion);
        [OperationContract]
        void editDepartamento(int id, string descripcion);





        //=================== Delete Element from database=============
        [OperationContract]
        void deletePermiso(int id);




        //=================== Get Elements from database=============
        [OperationContract]
        bool confirmarLogin(string email, string password);
        [OperationContract]
        Permisos getPermiso(int id);
        [OperationContract]
        Roles getRol(int id);
        [OperationContract]
        Departamento getDepartamento(int id);
        [OperationContract]
        Usuario getUsuario(int talento_humano);
        [OperationContract]
        List<Calendario> getTbl_calendario();
        [OperationContract]
        List<Departamento> getTbl_departamentos();
        [OperationContract]
        List<Jerarquia> getTbl_jerarquia();
        [OperationContract]
        List<Log_Vacaciones> getTbl_log_vacaciones();
        [OperationContract]
        List<Permisos> getTbl_permisos();
        [OperationContract]
        List<Roles> getTbl_roles();
        [OperationContract]
        List<Status> getTbl_status();
        [OperationContract]
        List<Tipo_Dia> getTbl_tipo_dia();
        [OperationContract]
        List<Usuario> getTbl_usuarios();
        [OperationContract]
        List<Vacaciones> getTbl_vacaciones();

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