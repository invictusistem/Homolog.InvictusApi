using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invictus.Dtos.Identity
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }
    }

    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string SenhaConfirmacao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Role { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public Guid UnidadeId { get; set; }
    }

    public class UserResponseLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Nome { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}