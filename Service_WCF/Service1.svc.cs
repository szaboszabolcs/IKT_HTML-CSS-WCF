using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Service_WCF.AdatbazisKezelese;

namespace Service_WCF
{

    public class Service1 : IService1
    {
        public static List<User> userLista = new List<User>();
        Random random = new Random();

        public static HashSet<int> userIndex = new HashSet<int>(); // ID ide kerül //

        public static int Pozicio(int id)
        {
            int index = 0;
            int sorokSzama = userLista.Count;
            while (index < sorokSzama)
            {
                if (userLista[index].ID == id)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        // Adatbázisból lekérdezés //


        public List<User> UserListaDB()
        {
            List<User> userList = new List<User>();
            AdatbazisKezelese.ISQL tableUserKezeles = new AdatbazisKezelese.UserKezeles();
            List<Rekord> rekordok = tableUserKezeles.Select();
            foreach (Rekord egyRekord in rekordok)
            {
                if (egyRekord is User)
                {
                    userList.Add(egyRekord as User);
                }
            }
            return userList;
        }

        // Bejelentkezéshez végpont //

        public User getUsers(string uname, string pwd)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string qry = "SELECT * FROM `users` WHERE uname=@uname AND pwd=@pwd";

            MySqlConnection connection = Kapcsolat.connection;
            connection.Open();
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@uname", uname);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            cmd.CommandText = qry;

            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                return null;
            }
            dr.Read();

            User Users = new User();

            Users.ID = dr.GetInt32(0);
            Users.Uname = dr.GetString(1);
            Users.Email = dr.GetString(2);
            Users.Password = dr.GetString(3);
            Users.Fullname = dr.GetString(4);
            Users.Active = dr.GetByte(5);
            Users.Rank = dr.GetInt32(6);
            Users.Banned = dr.GetBoolean(7);


            dr.Close();
            connection.Close();
            return Users;
        }

        // Hozzáadás Adatbázisban //
        public string UserPostDB(User user)
        {
            AdatbazisKezelese.UserKezeles tableUserKezeles = new AdatbazisKezelese.UserKezeles();
            if (tableUserKezeles.Insert(user) > 0)
            {
                return "A felhasználó adatainak a tárolása megtörtént.";

            }
            else
            {
                return "A felhasználó adatainak a tárolása sikertelen!";
            }
        }

        public string UserPostDBCS(User user)
        {
            return UserPostDB(user);
        }

       



        // Módosítás adatbázisban //

        public string UserPutDB(User user)
        {
            AdatbazisKezelese.UserKezeles tableUserKezeles = new AdatbazisKezelese.UserKezeles();
            if (tableUserKezeles.Update(user) > 0)
            {
                return "A felhasználó adatainak módosítása megtörtént.";
            }
            else
            {
                return "A felhasználó adatainak a módosítása sikertelen!";
            }
        }

        public string UserPutDBCS(User user)
        {
            return UserPostDB(user);
        }


        // Törlés adatbázisban //

        public string UserDeleteDB(int id)
        {
            AdatbazisKezelese.UserKezeles tableUserKezeles = new AdatbazisKezelese.UserKezeles();
            if (tableUserKezeles.Delete(id) <= 0)
            {
                return "A felhasználó adatainak törlése megtörtént.";

            }
            else
            {
                
                return "A felhasználó adatainak törlése sikertelen!";
            }
        }

        public string UserDeleteDBCS(int id)
        {
            return UserDeleteDB(id);
        }

    }
}