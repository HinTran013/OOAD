using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class photoDAL
    {
        private IMongoDatabase db;

        public photoDAL()
        {
            var client = new MongoClient("mongodb+srv://HienTranOOAD:123123123@cluster0.guxtk.mongodb.net/PhotographyManagement?retryWrites=true&w=majority");
            this.db = client.GetDatabase("PhotographyManagement");
        }

        public bool InsertNewPhoto(photoDTO newPhoto)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("photos");
                var newDoc = new BsonDocument
            {
                { "invoiceID", newPhoto.invoiceID },
                { "photoContent", newPhoto.photoContent }
            };

                collection.InsertOneAsync(newDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //lấy tất cả records photo từ database
        public List<photoDTO> getListOfPhotoDTOsByInvoiceID(ObjectId invoiceID)
        {
            try
            {
                var collection = db.GetCollection<BsonDocument>("photos");
                var inv = collection.Find(x => ((ObjectId)x["invoiceID"]) == invoiceID).ToListAsync().Result;
                var photos = new List<photoDTO>();

                foreach (BsonDocument photo in inv)
                {
                    photos.Add(new photoDTO
                    (
                        (ObjectId)photo["invoiceID"],
                        (byte[])photo["photoContent"]
                    ));
                }
                return photos;
            }
            catch
            {
                return null;
            }
        }

        //chuyển list photoDTO từ database thành 1 list các byte hình
        public List<byte[]> convertListPhotoDTOsToListPhotoBytes (List<photoDTO> photos)
        {
            List<byte[]> listByte = new List<byte[]>();

            for(int i = 0; i < photos.Count; i++)
            {
                listByte.Add(photos[i].photoContent);
            }

            return listByte;
        }

        
    }
}
