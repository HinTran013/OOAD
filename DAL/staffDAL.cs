using System;
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
                {"staffID", newStaff.ID },
                { "name" , newStaff.name },
                {"birthDate", newStaff.birthDate },
                {"gender", newStaff.gender },
                {"email", newStaff.email },
                {"phoneNumber", newStaff.phoneNumber },
                {"salary", newStaff.salary },
                { "type", newStaff.type }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public staffDTO getStaffByID(string ID)
        //{
        //    staffDTO staff;

            

        //    return staff; 
        //}
    }
}
