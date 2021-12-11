﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using DTO;

namespace DAL
{
    public class staffDAL
    {
        private IMongoDatabase db;

        public staffDAL()
        {
           var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
           this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InserNewStaffRecord (staffDTO newStaff)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("staffs");
                var newDoc = new BsonDocument
            {
                { "name" , newStaff.name },
                {"birthDate", newStaff.birthDate },
                {"gender", newStaff.gender },
                {"email", newStaff.email },
                {"phoneNumber", newStaff.phoneNumber },
                {"salary", newStaff.salary },
                {"type", newStaff.type },
                {"username", newStaff.type },
                {"password", newStaff.type }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Login (string uname, string pass)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("staffs");
                var findExist = collection.Find(x => ((string)x["username"]).ToLower() == uname).SingleAsync().Result;
                if (findExist != null)
                {
                    if (pass == ((string)findExist["password"]).ToLower())
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public staffDTO GetStaffByUsername (string uname)
        {
            try
            {
                staffDTO res = new staffDTO();
                var collection = db.GetCollection<BsonDocument>("staffs");
                var staff = collection.Find(x => ((string)x["username"]).ToLower() == uname).SingleAsync().Result;
                if (staff != null)
                {
                    res = new staffDTO(
                            (string)staff["name"],
                            (string)staff["birthDate"],
                            (bool)staff["gender"],
                            (string)staff["email"],
                            (string)staff["phoneNumber"],
                            (int)staff["salary"],
                            (string)staff["type"],
                            (string)staff["username"],
                            (string)staff["password"]);
                    return res;
                }
                else return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new staffDTO();
            }
        }
    }
}
