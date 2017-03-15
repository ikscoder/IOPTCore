using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOPTCore.Models
{
    public class User
    {
        long _id;
        string _login;
        string _password;
        public long id { get { return _id; } set { _id = value; } }
        [Required]
        public string login { get { return _login; } set { _login = value; } }
        [Required]
        public string password { get { return _password; } set { _password = value; } }

        public List<Model> Models { get; set; } = new List<Model>();
    }

    public class AppKey
    {
        long _id;
        Model _model;
        string _appKey;

        public long id { get { return _id; } set { _id = value; } }
        public string appKey { get { return _appKey; } set { _appKey = value; } }
        public Model model { get { return _model; } set { _model = value; } }
    }
}
