using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController :Controller
    {

        public string Index()
        {
            return "Hello Harmesh";
        }

        public string GetBook(int id)
        {
            return $"you are getting book ={id}";
        }
    }
}
