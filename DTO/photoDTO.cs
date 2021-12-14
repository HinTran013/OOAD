using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class photoDTO
    {
        private ObjectId _invoiceID;
        private byte[] _photoContent;

        public photoDTO()
        {
            _invoiceID = new ObjectId("");
            _photoContent = null;
        }

        public photoDTO(ObjectId invoiceID, byte[] photoContent)
        {
            this._invoiceID = invoiceID;
            this._photoContent = photoContent;
        }

        public ObjectId invoiceID { get => _invoiceID; set => _invoiceID = value; }
        public byte[] photoContent { get => _photoContent; set => _photoContent = value; }
    }
}
