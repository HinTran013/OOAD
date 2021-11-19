﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class ideaBUS
    {
        static ideaDAL idea = new ideaDAL();

        public static bool AddNewIdea(ideaDTO newIdea)
        {
            return idea.InserNewIdeaRecord(newIdea);
        }
    }
}
