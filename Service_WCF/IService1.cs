using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace Service_WCF
{
    [ServiceContract]
    public interface IService1
    {

        // Login végpont működik //
        [OperationContract]
        [WebInvoke(Method = "*",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "getUsers/{uname}/{pwd}")
           ]
        User getUsers(string uname, string pwd);

        // Adatok lekérése adatbázisból működik //
        [OperationContract]
        [WebInvoke(Method = "*",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "UserListaDB"
           )]
        List<User> UserListaDB();

        // Adatok hozzáadása adatbázisból működik //
        [OperationContract]
        [WebInvoke(Method = "*",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "UserPostDB"
            )]
        string UserPostDB(User user);


        [OperationContract]
        string UserPostDBCS(User user);


        // Adatok módosítása adatbázisból működik //
        [OperationContract]
        [WebInvoke(Method = "*",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "UserPutDB"
           )]
        string UserPutDB(User user);

        [OperationContract]

        string UserPutDBCS(User user);


        // Adatok törlése adatbázisból működik //

        [OperationContract]
        [WebInvoke(Method = "*",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "/UserDeleteDB?Id={Id}"
           )]
        string UserDeleteDB(int id);

        [OperationContract]

        string UserDeleteDBCS(int id);

    }

}

    [DataContract]

public class Rekord
{
    [DataMember]

    public int? ID { get; set; }
}

public class User : Rekord
{
    [DataMember(IsRequired = true)]
    public string Uname { get; set; }

    [DataMember(IsRequired = true)]
    public string Email { get; set; }

    [DataMember(IsRequired = true)]
    public string Password { get; set; }

    [DataMember(IsRequired = true)]
    public string Fullname { get; set; }

    [DataMember(IsRequired = true)]
    public byte Active { get; set; }

    [DataMember(IsRequired = true)]
    public int Rank { get; set; }

    [DataMember(IsRequired = true)]
    public bool Banned { get; set; }
}