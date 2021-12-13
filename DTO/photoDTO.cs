using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class photoDTO
    {
        private string _invoiceID;
        private byte[] _photoContent;

        public photoDTO()
        {
            _invoiceID = null;
            _photoContent = null;
        }

        public photoDTO(string invoiceID, byte[] photoContent)
        {
            this._invoiceID = invoiceID;
            this._photoContent = photoContent;
        }

        public string invoiceID { get => _invoiceID; set => _invoiceID = value; }
        public byte[] photoContent { get => _photoContent; set => _photoContent = value; }
    }
}
