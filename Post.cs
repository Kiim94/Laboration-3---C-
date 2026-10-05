using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift_3
{
    class Post
    {
        //egenskaper för Post: namn och inlägg.
        //De kan läsas av andra klasser via public get,
        //men bara ändras inne i Post klassen via private set
        public string Name { get; private set; }
        public string Message { get; private set; }

        //konstruktör för Post: vad ska finnas med/instruktioner för objektet Post
        public Post(string name, string message)
        {
            Name = name;
            Message = message;
        }

    }
}
