using System;
using System.Collections.Generic;

namespace Invictus.Dtos.PedagDto
{
    public class SaveProfsCommand
    {
        public Guid turmaId { get; set; }
        public IEnumerable<Guid> listProfsIds { get; set; }
    }
}
