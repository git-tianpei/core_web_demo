using System;

namespace core_web.demo.Data.Entity
{
    public class Note
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }



        #region 扩展字段

        public string Date { get; set; }

        public string Time { get; set; }

        public string Year { get; set; }

        #endregion
    }
}
