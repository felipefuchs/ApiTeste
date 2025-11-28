using Microsoft.AspNetCore.Mvc;
using SellApi.Models;

namespace SellApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        // armazenamento em memória para demonstração
        private static readonly List<Usuario> _usuarios = new List<Usuario>();
        private static int _nextId = 1;

        /// <summary>
        /// GET api/usuarios
        /// Recupera todos os usuários.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAll()
        {
            return Ok(_usuarios);
        }

        /// <summary>
        /// GET api/usuarios/{id}
        /// Recupera usuário pelo id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
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
        ///   "password": "senhaSegura123",
        ///   "active": true
        /// }
        /// </summary>
        [HttpPost]
        public ActionResult<Usuario> Create([FromBody] UsuarioCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new Usuario
            {
                Id = _nextId++,
                Nome = dto.Nome,
                Password = dto.Password,
                Active = dto.Active
            };

            _usuarios.Add(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// PUT api/usuarios/{id}
        /// Atualiza usuário existente.
        /// Exemplo de payload:
        /// {
        ///   "nome": "Nome atualizado",
        ///   "password": "novaSenha123",
        ///   "active": false
        /// }
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] UsuarioUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();

            usuario.Nome = dto.Nome;
            usuario.Password = dto.Password;
            usuario.Active = dto.Active;

            return NoContent();
        }
    }
}
