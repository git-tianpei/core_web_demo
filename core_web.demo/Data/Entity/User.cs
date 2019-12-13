using System;

namespace core_web.demo.Data.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
