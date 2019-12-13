using System.Collections.Generic;
using core_web.demo.Common;
using core_web.demo.Dao;
using core_web.demo.Data.Entity;
using core_web.demo.Data.Req;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace core_web.demo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        [HttpGet]
        [Route("getnotes")]
        public List<Note> GetNotes()
        {
            var user = HttpContext.GetUser();
            return new NoteDao().GetNotes(user.Id);
        }

        [HttpPost]
        [Route("addnote")]
        public int GetNotes([FromBody]NoteReq note)
        {
            var model = new Note { Title = note.Title, Content = note.Content };
            var user = HttpContext.GetUser();
            model.UserId = user.Id;
            return new NoteDao().AddNote(model) ? 1 : 0;
        }

        [HttpGet]
        [Route("getnote/{id:int}")]
        public Note GetNote(int id)
        {
            var user = HttpContext.GetUser();
            return new NoteDao().GetNoteById(user.Id, id);
        }

        [HttpPost]
        [Route("updatenote")]
        public int UpdateNote([FromBody]Note note)
        {
            var user = HttpContext.GetUser();
            return new NoteDao().UpdateNote(user.Id, note) ? 1 : 0;
        }

        [HttpPost]
        [Route("deletenote/{id:int}")]
        public int DeleteNote(int id)
        {
            var user = HttpContext.GetUser();
            return new NoteDao().DeleteNote(user.Id, id) ? 1 : 0;
        }
    }
}
