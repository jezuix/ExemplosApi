using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.API.Models;
using Teste.API.RequestModel;

namespace Teste.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        #region Não Abra ...
        #region Não Abra ...
        #region Não Abra ...
        #region Não Abra ...
        #region Não Abra ...
        #region Não Abra ...
        #region Não Abra ...

        private readonly string badRequest = "Poxa Vida.... Quebrou ...";
        private readonly string usuarioNaoExiste = "Infelizmente este cara pra quem tu quer dar dinheiro não existe, mas se fizer questão te passo minha conta pra depositar";
        private readonly string badRequestExtreme = "Quebrou aqui ??? Caraca ai tu foi genial ahua zueira :)";
        private readonly string ok = "Num é que funfou O_O";
        private readonly string ufaaa = "Ufaaa essa foi quase ....";
        private readonly string deletado = "Agora deletou pra sempre ... ou até você recompilar!";

        #endregion
        #endregion
        #endregion
        #endregion
        #endregion
        #endregion
        #endregion

        private readonly ApiContext _context;

        public TesteController(ApiContext context)
        {
            _context = context;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> EsseMetodoGetNaoServePraNada([FromServices] ApiContext apiContext)
        {
            try
            {
                var usuarios = await apiContext.Usuarios.Include(c => c.Carteira).ToListAsync();

                if (usuarios.Any())
                {
                    var index = new Random().Next(0, usuarios.Count() - 1);
                    var nome = usuarios[index].Nome;
                    var idUsuario = usuarios[index].Id;
                    var idCarteira = usuarios[index].IdCarteira;

                    return Ok($"Eu avisei (No código ... olha o nome do método)" +
                        $"{Environment.NewLine}Então aqui vai um nome aleatório da \"BASE\": {nome}" +
                        $"{Environment.NewLine}Código Usuário: {idUsuario}" +
                        $"{Environment.NewLine}Código Carteira: {idCarteira}");
                }
            }
            catch
            {
                BadRequest(badRequestExtreme);
            }

            return Ok("Cade os usuários da Base ??? Sumiu !!! Oh GODDDD");
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.Include(c => c.Carteira).ToListAsync();
                var retorno = usuarios.Select(x => new
                {
                    CodUsuario = x.Id,
                    NomeUsuario = x.Nome,
                    CodCarteira = x.IdCarteira,
                    InvestimentoCarteira = decimal.Round(x.Carteira?.ValorInvestido ?? 0, 2)
                });

                return Ok(retorno);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpGet]
        [Route("GetCarteirasPraDeletar")]
        public async Task<IActionResult> GetCarteirasPraDeletar()
        {
            try
            {
                var usuarios = await _context.Carteiras.ToListAsync();
                var retorno = usuarios.Select(x => new
                {
                    IdCarteira = x.Id,
                    TemCerteza = true
                });

                return Ok(retorno);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpGet]
        [Route("GetUsuariosPraDeletar")]
        public async Task<IActionResult> GetUsuariosPraDeletar()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                var retorno = usuarios.Select(x => x.Id);

                return Ok(retorno);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpGet]
        [Route("GetCarteira")]
        public async Task<IActionResult> GetCarteira(Guid carteiraId)
        {
            try
            {
                var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == carteiraId);

                if (carteira != null)
                    return Ok(new
                    {
                        CodUsuario = carteira.Usuario.Id,
                        NomeUsuario = carteira.Usuario.Nome,
                        CodCarteira = carteira.Id,
                        InvestimentoCarteira = decimal.Round(carteira.ValorInvestido, 2)
                    });

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpGet]
        [Route("GetCarteiraPorUsuario")]
        public async Task<IActionResult> GetCarteiraPorUsuario(Guid usuarioId)
        {
            try
            {
                var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                if (carteira != null)
                    return Ok(new
                    {
                        CodUsuario = carteira.Usuario.Id,
                        NomeUsuario = carteira.Usuario.Nome,
                        CodCarteira = carteira.Id,
                        InvestimentoCarteira = decimal.Round(carteira.ValorInvestido, 2)
                    });

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpGet]
        [Route("GetUsuario")]
        public async Task<IActionResult> GetUsuario(Guid usuarioId)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);

                if (usuario != null)
                    return Ok(new
                    {
                        CodUsuario = usuario.Id,
                        NomeUsuario = usuario.Nome
                    });

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public IActionResult OutroMetodoQueNaoFazNadaMasSeraQueVoceViu()
        {
            var listaInteiro = new List<int>();
            for (int i = 0; i < new Random().Next(1, 100); i++)
            {
                listaInteiro.Add(new Random().Next());
            }

            return Ok($"Lista de números aleatórios:{Environment.NewLine}" +
                $"{string.Join('|', listaInteiro)}");
        }

        [HttpPost]
        [Route("InsereUsuario")]
        public async Task<IActionResult> InsereUsuario([FromBody] InsereUsuarioRequest insereUsuarioRequest)
        {
            try
            {
                if (insereUsuarioRequest?.Nome?.Length > 0)
                {
                    var usuario = new Usuario(insereUsuarioRequest.Nome);
                    await _context.AddAsync(usuario);
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return BadRequest(usuarioNaoExiste);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpPost]
        [Route("InsereCarteira")]
        public async Task<IActionResult> InsereCarteira([FromBody] InsereCarteiraRequest insereCarteiraRequest)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == insereCarteiraRequest.IdUsuario);
                if (usuario != null)
                {
                    var carteira = new Carteira(usuario.Id, insereCarteiraRequest.ValorInvestido);
                    await _context.AddAsync(carteira);
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return BadRequest(usuarioNaoExiste);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpPost]
        [Route("AtualizaCarteira")]
        public async Task<IActionResult> AtualizaCarteira(Guid carteiraId, decimal novoValor)
        {
            try
            {
                var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == carteiraId);
                if (carteira != null)
                {
                    carteira.ValorInvestido = novoValor;
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpPost]
        [Route("SomaValor")]
        public async Task<IActionResult> SomaValor([FromBody] AtualizaCarteiraRequest atualizaCarteiraRequest)
        {
            try
            {
                var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == atualizaCarteiraRequest.IdCarteira);
                if (carteira != null)
                {
                    carteira.ValorInvestido += atualizaCarteiraRequest.ValorInvestido;
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        public IActionResult NemAdiantaFalarQueNaoFazNada()
        {
            return Ok("To sem criatividade!");
        }

        [HttpPut]
        [Route("AtualizaCarteiraFromBody")]
        public async Task<IActionResult> AtualizaCarteiraFromBody([FromBody] AtualizaCarteiraRequest carteira)
        {
            try
            {
                var carteiraSelecionada = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == carteira.IdCarteira);
                if (carteiraSelecionada != null)
                {
                    carteiraSelecionada.ValorInvestido = carteira.ValorInvestido;
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpPut]
        [Route("AtualizaRangeCarteirasFromBody")]
        public async Task<IActionResult> AtualizaRangeCarteirasFromBody([FromBody] List<AtualizaCarteiraRequest> carteiras)
        {
            try
            {
                if (carteiras?.Any() ?? false)
                {
                    foreach (var carteira in carteiras)
                    {
                        var carteiraSelecionada = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == carteira.IdCarteira);
                        if (carteiraSelecionada != null)
                        {
                            carteiraSelecionada.ValorInvestido = carteira.ValorInvestido;
                            await _context.SaveChangesAsync();
                        }
                    }

                    return Ok(ok);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpPut]
        [Route("AtualizaNome")]
        public async Task<IActionResult> AtualizaNomeFromBody([FromQuery] Guid usuarioId, string nome)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
                if (usuario != null)
                {
                    usuario.Nome = nome;
                    await _context.SaveChangesAsync();

                    return Ok(ok);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public IActionResult EsseVaiApagarTudo()
        {
            return Ok("Base Limpa :D Só Que Não");
        }

        [HttpDelete]
        [Route("DeletaUsuario")]
        public async Task<IActionResult> DeletaUsuario(Guid usuarioId, bool temCerteza = false)
        {
            try
            {
                if (temCerteza)
                {
                    var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);

                    if (usuario != null)
                    {
                        var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == usuario.IdCarteira);
                        if (carteira != null)
                            _context.Remove(carteira);

                        _context.Remove(usuario);

                        await _context.SaveChangesAsync();

                        return Ok(deletado);
                    }
                }

                return Ok(ufaaa);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpDelete]
        [Route("DeletaCarteira")]
        public async Task<IActionResult> DeletaCarteira(Guid carteiraId, bool temCerteza = false)
        {
            try
            {
                if (temCerteza)
                {
                    var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == carteiraId);

                    if (carteira != null)
                    {
                        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == carteira.IdUsuario);
                        if (usuario != null)
                        {
                            usuario.Carteira = null;
                            usuario.IdCarteira = null;
                        }

                        _context.Remove(carteira);

                        await _context.SaveChangesAsync();

                        return Ok(deletado);
                    }
                }

                return Ok(ufaaa);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpDelete]
        [Route("DeletaCarteiraFromBody")]
        public async Task<IActionResult> DeletaCarteiraFromBody([FromBody] DeletaCarteiraRequest deletaCarteiraRequest)
        {
            try
            {
                if (deletaCarteiraRequest.TemCerteza)
                {
                    var carteira = await _context.Carteiras.FirstOrDefaultAsync(x => x.Id == deletaCarteiraRequest.IdCarteira);

                    if (carteira != null)
                    {
                        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == carteira.IdUsuario);
                        if (usuario != null)
                        {
                            usuario.Carteira = null;
                            usuario.IdCarteira = null;
                        }

                        _context.Remove(carteira);

                        await _context.SaveChangesAsync();

                        return Ok(deletado);
                    }
                }

                return Ok(ufaaa);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpDelete]
        [Route("DeletaRangeCarteirasFromBody")]
        public async Task<IActionResult> DeletaRangeCarteirasFromBody([FromBody] List<DeletaCarteiraRequest> carteiras)
        {
            try
            {
                if (carteiras?.Any() ?? false)
                {
                    var carteirasDeletadas = (await _context.Carteiras.ToListAsync()).Where(c => carteiras.Any(x => x.TemCerteza && x.IdCarteira == c.Id));
                    if (carteirasDeletadas.Any())
                    {
                        (await _context.Usuarios.ToListAsync()).Where(x => carteirasDeletadas.Any(c => c.Id == x.IdCarteira)).ToList().ForEach(x => { x.IdCarteira = null; x.Carteira = null; });
                        _context.RemoveRange(carteirasDeletadas);

                        await _context.SaveChangesAsync();

                        return Ok(deletado);
                    }
                }

                return Ok(ufaaa);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        [HttpDelete]
        [Route("DeletaRangeUsuariosFromBody")]
        public async Task<IActionResult> DeletaRangeUsuariosFromBody([FromBody] List<Guid> usuariosId)
        {
            try
            {
                if (usuariosId?.Any() ?? false)
                {
                    var usuariosDeletados = (await _context.Usuarios.ToListAsync()).Where(c => usuariosId.Any(x => x == c.Id));
                    if (usuariosDeletados.Any())
                    {
                        var carteirasDeletadas = (await _context.Carteiras.ToListAsync()).Where(x => usuariosId.Any(u => u == x.Id));

                        _context.RemoveRange(carteirasDeletadas);
                        _context.RemoveRange(usuariosDeletados);

                        await _context.SaveChangesAsync();

                        return Ok(deletado);
                    }
                }

                return Ok(ufaaa);
            }
            catch
            {
                return BadRequest(badRequest);
            }
        }

        #endregion
    }
}
