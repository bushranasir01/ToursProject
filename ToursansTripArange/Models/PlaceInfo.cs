using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToursansTripArange.Models
{
    public class places
    {
        public string name  
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }

    }
    public class Muree
    {
        public string name
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }

    }

    public class islamabad
    {
        public string name
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }

    }
    public class rawalpindi
    {
        public string name
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }

    }
    public class nathiaGali
    {
        public string name
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }

    }

    public class placeabout
    {
        public places places
        {
            get;
            set;
        }
        public Muree Muree
        {
            get;
            set;
        }
        public islamabad islamabad
        {
            get;
            set;
        }
        public rawalpindi rawalpindi
        {
            get;
            set;
        }
        public nathiaGali nathiagali
        {
            get;
            set;
        }
    }


}