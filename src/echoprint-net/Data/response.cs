using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echoprint_net.Data
{
    /*
     {
  "response": {
    "status": {
      "code": 0,
      "message": "Success",
      "version": "4.2"
    },
    "songs": [
      {
        "title": "Billie Jean",
        "artist_name": "Michael Jackson",
        "artist_id": "ARXPPEY1187FB51DF4",
        "score": 49,
        "message": "OK (match type 5)",
        "id": "SOKHYNL12A8C142FC7"
      }
    ]
  }
}*/
    public class song
    {
        public string title { get; set; }
        public string artist_name { get; set; }
        public string artist_id { get; set; }
        public int score { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class status
    {
        public int code { get; set; }
        public string message { get; set; }
        public string version { get; set; }
    }

    public class response
    {
        public status status { get; set; }
        public List<song> songs { get; set; }
    }
}
