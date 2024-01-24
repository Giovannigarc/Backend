using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var usuarios = await _usuarioRepository.GetAll();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter usuários: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return NotFound($"Usuário com Id {id} não encontrado");

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter usuário: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Usuario usuario)
    {
        try
        {
            var createdUsuarioId = await _usuarioRepository.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = createdUsuarioId }, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar usuário: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
    {
        try
        {
            var existingUsuario = await _usuarioRepository.GetById(id);

            if (existingUsuario == null)
                return NotFound($"Usuário com Id {id} não encontrado");

            usuario.Id = id;
            await _usuarioRepository.Update(usuario);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao atualizar usuário: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingUsuario = await _usuarioRepository.GetById(id);

            if (existingUsuario == null)
                return NotFound($"Usuário com Id {id} não encontrado");

            await _usuarioRepository.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao excluir usuário: {ex.Message}");
        }
    }
}
