using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellApiBusiness.Model;
using System;

namespace SellApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        // armazenamento em memória para demonstração
        private static readonly List<Usuario> _usuarios = new List<Usuario>();
        private static int _nextId = 1;

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAll()
        {
            return Ok(_usuarios);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Usuario> GetById(int id)
        {
            var user = _usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// POST api/usuarios
        /// Cria um novo usuário.
        /// Exemplo de payload:
        /// {
        ///   "nome": "João da Silva",
        ///   "email": "joao@exemplo.com",
        ///   "senha": "senhaSegura123",
        ///   "ativo": true
        /// }
        /// </summary>
        // [HttpPost]
        // public ActionResult<Usuario> Create([FromBody] Usuario usuario     ) 
        //  {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var usuario = new Usuario
        //     {
        //         Id = _nextId++,
        //         Nome = usuario.Nome,
        //         Email = usuario.Email,
        //         Senha = usuario.Senha,
        //         Ativo = usuario.Ativo,
        //         DataCriacao = DateTime.UtcNow
        //     };

        //     _usuarios.Add(usuario);

        //     return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        // }

        // /// <summary>
        // /// PUT api/usuarios/{id}
        // /// Atualiza usuário existente.
        // /// </summary>
        // [HttpPut("{id:int}")]
        // public ActionResult Update(int id, [FromBody] Usuario dto)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        //     if (usuario == null) return NotFound();

        //     usuario.Nome = dto.Nome;
        //     usuario.Email = dto.Email;
        //     usuario.Senha = dto.Senha;
        //     usuario.Ativo = dto.Ativo;

        //     return NoContent();
        // }
    }
}
