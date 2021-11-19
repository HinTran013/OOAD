using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ideaDTO
    {
        private string _ID;
        private string _ideaSubject;
        private string _ideaDescription;
        private string _ideaDate;
        private string _staffID;

        public ideaDTO()
        {
            this._ID = null;
            this._ideaSubject = null;
            this._ideaDescription = null;
            this._ideaDate = null;
            this._staffID = null;
        }

        //constructor with params
        public ideaDTO(string ID,
        string ideaSubject,
        string ideaDescription,
        string ideaDate,
        string staffID)
        {
            this._ID = ID;
            this._ideaSubject = ideaSubject;
            this._ideaDescription = ideaDescription;
            this._ideaDate = ideaDate;
            this._staffID = staffID;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string ideaSubject { get => _ideaSubject; set => _ideaSubject = value; }
        public string ideaDescription { get => _ideaDescription; set => _ideaDescription = value; }
        public string ideaDate { get => _ideaDate; set => _ideaDate = value; }
        public string staffID { get => _staffID; set => _staffID = value; }
    }
}
