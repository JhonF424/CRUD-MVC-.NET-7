using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppCrud.Models;
using AppCrud.Repository.Contract;

namespace AppCrud.Controllers;

public class HomeController : Controller
{
    private readonly IGenericRepository<Departamento> _departamentoRepository;
    private readonly IGenericRepository<Empleado> _empleadoRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IGenericRepository<Departamento> departamentoRepository, IGenericRepository<Empleado> empleadoRepository)
    {
        _logger = logger;
        _departamentoRepository = departamentoRepository;
        _empleadoRepository = empleadoRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ListaDepartamentos()
    {
        List<Departamento> _lista = await _departamentoRepository.Lista();
        return StatusCode(StatusCodes.Status200OK, _lista);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListaEmpleados()
    {
        List<Empleado> _lista = await _empleadoRepository.Lista();
        return StatusCode(StatusCodes.Status200OK, _lista);
    }
    
    [HttpPost]
    public async Task<IActionResult> CrearEmpleado([FromBody] Empleado model)
    {
        bool _resultado = await _empleadoRepository.Guardar(model);

        if (_resultado)
            return StatusCode(StatusCodes.Status200OK, new { value = _resultado, msg = "OK" });
        else
            return StatusCode(StatusCodes.Status500InternalServerError, new { value = _resultado, msg = "Server Error" });
    }
    
    [HttpPut]
    public async Task<IActionResult> EditarEmpleado([FromBody] Empleado model)
    {
        bool _resultado = await _empleadoRepository.Editar(model);

        if (_resultado)
            return StatusCode(StatusCodes.Status200OK, new { value = _resultado, msg = "OK" });
        else
            return StatusCode(StatusCodes.Status500InternalServerError, new { value = _resultado, msg = "Server Error" });
    }
    
    [HttpDelete]
    public async Task<IActionResult> EliminarEmpleado(int idEmpleado)
    {
        bool _resultado = await _empleadoRepository.Eliminar(idEmpleado);

        if (_resultado)
            return StatusCode(StatusCodes.Status200OK, new { value = _resultado, msg = "OK" });
        else
            return StatusCode(StatusCodes.Status500InternalServerError, new { value = _resultado, msg = "Server Error" });
    }
    
    
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}