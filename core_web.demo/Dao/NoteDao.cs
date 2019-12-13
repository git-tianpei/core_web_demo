using System;
using System.Collections.Generic;
using System.Linq;
using core_web.demo.Data.Entity;
using Dapper;
using MySql.Data.MySqlClient;

namespace core_web.demo.Dao
{
    public class NoteDao : MysqlBase
    {
        public bool AddNote(Note note)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                note.CreateTime = DateTime.Now;
                var sql = "INSERT INTO `wx_note` (`userId`, `title`, `content`, `createTime`) VALUES (@userId, @title, @content, @createTime)";
                return conn.Execute(sql, note) > 0;
            }
        }

        public List<Note> GetNotes(int userId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var list = conn.Query<Note>("select * from wx_note where userId = @userId", new { userId }).ToList();

                list.ForEach(x =>
                {
                    x.Date = x.CreateTime.Date.ToString("MM-dd");
                    x.Time = x.CreateTime.ToString("hh:mm");
                    x.Year = x.CreateTime.ToString("yyyy");
                });
                return list;
            }
        }

        public Note GetNoteById(int userId, int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                return conn.Query<Note>("select * from wx_note where userId = @userId and id = @id", new { userId, id }).SingleOrDefault();
            }
        }

        internal bool DeleteNote(int userId, int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                return conn.Execute(
                           "delete from wx_note where @userId and id = @id",
                           new { userId, id }) > 0;
            }
        }

        public bool UpdateNote(int userId, Note req)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                return conn.Execute(
                           "update wx_note set title=@title,content=@content where userId = @userId and id = @id",
                           new { req.Title, req.Content, req.Id, userId }) > 0;
            }
        }
    }
}
