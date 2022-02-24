using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogLogin
    {
        public LogLogin(Guid userId,
                        string email,
                        DateTime data,
                        string unidade

                        )
        {   
            UserId = userId;
            Email = email;
            Data = data;
            Unidade = unidade;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public DateTime Data { get; private set; }
        public string Unidade { get; private set; }

        public LogLogin()
        {

        }
    }
}
