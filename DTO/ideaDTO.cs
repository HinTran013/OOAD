using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ideaDTO
    {
        private string _ideaSubject;
        private string _ideaDescription;
        private string _ideaDate;
        private string _staffUsername;
        private ObjectId? _objectId;

        public ideaDTO()
        {
            this._ideaSubject = null;
            this._ideaDescription = null;
            this._ideaDate = null;
            this._staffUsername = null;
            this._objectId = null;
        }

        //constructor with params
        public ideaDTO(
        string ideaSubject,
        string ideaDescription,
        string ideaDate,
        string staffUsername,
        ObjectId? objectId = null)
        {
            this._ideaSubject = ideaSubject;
            this._ideaDescription = ideaDescription;
            this._ideaDate = ideaDate;
            this._staffUsername = staffUsername;
            this._objectId = objectId;
        }

        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
        public string ideaSubject { get => _ideaSubject; set => _ideaSubject = value; }
        public string ideaDescription { get => _ideaDescription; set => _ideaDescription = value; }
        public string ideaDate { get => _ideaDate; set => _ideaDate = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
    }
}
