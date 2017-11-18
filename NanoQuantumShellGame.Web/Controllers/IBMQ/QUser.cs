using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoQuantumShellGame.Web
{
    public class QUser
    {
        //result of login with token looks like:
        //    {
        //        "id": "5OyHC6kXXMP0CSSaY1mSuFmpZbcZaADLjOQmUvCgG11KwoiqWwDNW1FQtuVrCSOk",
        //        "ttl": 1209600,
        //        "created": "2017-07-26T15:30:30.836Z",
        //        "userId": "a3e5c196cb90688ba9a50dd7607999a6"
        //    }


        public string id = "";
        public int ttl = 0;
        public DateTime created = DateTime.Now;
        public string userid = "";


    }
}
