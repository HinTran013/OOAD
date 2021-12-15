using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using MongoDB.Bson;

namespace BUS
{
    public class photoBUS
    {
        static photoDAL photo = new photoDAL();

        public static bool AddNewPhoto(photoDTO newPhoto)
        {
            return photo.InsertNewPhoto(newPhoto);
        }

        public static List<photoDTO> getListOfPhotoDTOsByInvoiceID(ObjectId invoiceID)
        {
            return photo.getListOfPhotoDTOsByInvoiceID(invoiceID);
        }

        public static List<byte[]> convertListPhotoDTOsToListPhotoBytes(List<photoDTO> photos)
        {
            return photo.convertListPhotoDTOsToListPhotoBytes(photos);
        }
    }
}
